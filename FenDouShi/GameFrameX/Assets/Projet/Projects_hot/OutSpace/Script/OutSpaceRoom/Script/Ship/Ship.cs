using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Ship : MonoBehaviour
{
    public ShipType shipType = ShipType.None;
    public ShipSkinType shipSkinType = ShipSkinType.None;//just look At editor
    public Texture selfTexture;//store current ship mainTexture,//just look At editor

    private List<Material> mats;
    //The ship's Shader Head name
    private const string  ShaderHeadName = "ShipRaceBoats";

    private void Awake()
    {
        if (!isMatsProperly())
        {
            mats = GetShipMats();
            if (mats == null)
            {
                Debug.LogError(this.gameObject.name + " chid's node did not have Materials or The Shader HeadName is Not BoatAttack!");
                return;
            }
        }
        if (isMatsProperly() && selfTexture == null)
        {
            selfTexture = mats[0].mainTexture;
        }
    }
    private bool isMatsProperly()
    {
        if (mats == null || mats.Count <= 0)
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// Update Ship Color
    /// </summary>
    /// <param name="color"> set color</param>
    /// <param name="primary">true is base Color or false is trim color</param>
    public void UpdateShipColor(Color color, bool primary)
    {
        if (!isMatsProperly())
        {
            return;
        }
        for (int i = 0; i < mats.Count; i++)
        {
            mats[i].SetColor(primary ? "_Color1" : "_Color2", color);
        }

    }

    /// <summary>
    /// Update ship,s Skin by ShipSkinTextureConfig config
    /// </summary>
    /// <param name="skinType">skin config from ShipContent</param>
    public void UpdateShipSkinType(ShipContent.ShipSkinTextureConfig skinType)
    {
        if (isMatsProperly() && skinType != null)
        {
            shipSkinType = skinType.currShipMatType;
            if (skinType.currShipMatType == ShipSkinType.SelfTextureAndNotMatallic)
            {
                Debug.Log("skinType.albedoAOTex=" + skinType.albedoAOTex);
                Debug.Log("skinType.metallicTex=" + skinType.metallicTex);
            }
            for (int i = 0; i < mats.Count; i++)
            {
                mats[i].SetTexture("_MainTex", skinType.albedoAOTex?skinType.albedoAOTex:null);
                mats[i].SetTexture("_MRLL", skinType.metallicTex?skinType.metallicTex:null);
                //keep the ship selfTexture
                if (skinType.currShipMatType== ShipSkinType.SelfTextureAndNotMatallic|| skinType.currShipMatType == ShipSkinType.SelfTextureAndScattleMatallic|| skinType.currShipMatType == ShipSkinType.SelfTextureAndSmmothMatallic)
                {
                    mats[i].SetTexture("_MainTex", selfTexture);
                }
            }
           
        }
    }
    private List<Material> GetShipMats()
    {
        MeshRenderer[] renderers = this.gameObject.GetComponentsInChildren<MeshRenderer>(true);
        if (renderers == null || renderers.Length <= 0)
        {
            return null;
        }
        List<Material> mats = new List<Material>();
        for (int i = 0; i < renderers.Length; i++)
        {
            string shaderName = renderers[i].material.shader.name;
            //the Material is not Ship Material
            if (shaderName.IndexOf(ShaderHeadName) == -1)
            {
                Debug.Log(renderers[i].gameObject.name + "  shader name=" + shaderName);
                continue;
            }

            //the Material is  Ship Material
            mats.Add(renderers[i].material);

        }
        return mats;
    }
}
