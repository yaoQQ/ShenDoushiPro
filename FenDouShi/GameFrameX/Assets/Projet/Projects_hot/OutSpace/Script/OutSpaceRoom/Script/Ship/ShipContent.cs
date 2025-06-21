using UnityEngine;
using System.Collections.Generic;
using System;

public class ShipContent : MonoBehaviour
{
    [System.Serializable]
    public class ShipSkinTextureConfig
    {
        public ShipSkinType currShipMatType = ShipSkinType.None;
        //To hide  the albedoAOTex in Editor By  ShipSkinType's (int)Value
        //did not need the texture.Hide in Editor when ShipSkinType==SelfTextureAndNotMatallic, SelfTextureAndSmmothMatallic,SelfTextureAndScattleMatallic,
        [ConditionalHide2("currShipMatType", new int[] { (int)ShipSkinType.SelfTextureAndNotMatallic,(int)ShipSkinType.SelfTextureAndScattleMatallic, (int)ShipSkinType.SelfTextureAndSmmothMatallic })]
        public Texture albedoAOTex;
        //did not need the texture.Hide in Editor when ShipSkinType==SelfTextureAndNotMatallic
        [ConditionalHide2("currShipMatType", (int)ShipSkinType.SelfTextureAndNotMatallic)]
        public Texture metallicTex;
    }

    [SerializeField]
    //Save data for spaceship skin conversion
    public List<ShipSkinTextureConfig> shipMatConfigList=new List<ShipSkinTextureConfig>();
    private Dictionary<ShipType, Ship> shipDic = new Dictionary<ShipType, Ship>();

    public Ship currShip;

    public MeshRenderer baseColor;
    public MeshRenderer trimColor;
    private void Awake()
    {
       Ship[] ships= this.transform.GetComponentsInChildren<Ship>(true);
        for(int i=0;i< ships.Length; i++)
        {
            shipDic[ships[i].shipType] = ships[i];
         //   Debug.LogFormat("Add ShipType[{0}] ={1}",i,ships[i].shipType);
            if(ships[i].shipType== ShipType.None)
            {
                Debug.LogError(this.gameObject.name+" ShipType did not set!");
            }
            if (ships[i].gameObject.activeSelf)//init get the active ship
            {
                currShip = ships[i];
            }
        }
        
    }
    public void UpdateShipColor(Color color, bool isBaseTexture)
    {
        if (currShip)
        {
            currShip.UpdateShipColor(color, isBaseTexture);
        }
    }
    public void UpdateShipSkinType(ShipSkinType shipSkinType)
    {
        if (currShip)
        {
            ShipSkinTextureConfig skin = GetShipSkinType(shipSkinType);
            currShip.UpdateShipSkinType(skin);
        }
    }
    public void UpdateAllShipSkinType(ShipSkinType shipSkinType)
    {
        ShipSkinTextureConfig skin = GetShipSkinType(shipSkinType);
        foreach (var pair in shipDic)
        {
            Ship ship = pair.Value;
            ship.UpdateShipSkinType(skin);
        }
    }
    public void UpdateAllShipColor(Color color, bool isBaseTexture)
    {
        foreach (var pair in shipDic)
        {
            Ship ship = pair.Value;
            ship.UpdateShipColor(color, isBaseTexture);
        }
        DisplayShipColor(color, isBaseTexture);
    }
    private void DisplayShipColor(Color color, bool isBaseTexture)
    {
        if (isBaseTexture && baseColor)
        {
            Material colorMat = baseColor.material;
            if (colorMat)
            {
                colorMat.SetColor("_Color", color);
            }
        }
        else if (!isBaseTexture && trimColor)
        {
            Material colorMat = trimColor.material;
            if (colorMat)
            {
                colorMat.SetColor("_Color", color);
            }
        }
    }

    public Ship ShowShipType(ShipType shipType)
    {
        if (!shipDic.ContainsKey(shipType))
        {
            Debug.LogError("ShipContent Don't have shipType=" + shipType);
            return null;
        }
        Reset();
        currShip = shipDic[shipType];
        currShip.gameObject.SetActive(true);
        return currShip;
    }
    

    private ShipSkinTextureConfig GetShipSkinType(ShipSkinType matType)
    {
        for(int i=0;i< shipMatConfigList.Count; i++)
        {
            if(shipMatConfigList[i].currShipMatType== matType)
            {
                return shipMatConfigList[i];
            }
        }
        return null;
    }
    private void Reset()
    {
        foreach(var pair in shipDic)
        {
            pair.Value.gameObject.SetActive(false);
        }
    }

    
}
