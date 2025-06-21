
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    public ColorSelector colorSelect;
    public ShipContent shipContent;
    public ShipPanel shipPanel;
    public RotateTarget rotateShip;
    void Start()
    {
        if(colorSelect==null|| shipPanel==null|| shipContent == null|| rotateShip==null)
        {
            Debug.LogError("Did not Set Main's parameter in GameObject of "+this.gameObject.name);
            return;
        }
        colorSelect.AddColorChangeFun(ColorChangeFun);
        shipPanel.AddChangeShipFun(ChangeShipFun);
        shipPanel.AddChangeSkinFun(ChangeSkinFun);
        if (shipContent.currShip)
        {
            rotateShip.rotateTarget = shipContent.currShip.transform;
        }
    }
    private void ChangeShipFun(ShipType shipType)
    {
        Ship ship = shipContent.ShowShipType(shipType);
        rotateShip.rotateTarget = ship.transform;
    }
    private void ChangeSkinFun(ShipSkinType shipSkinType)
    {
        shipContent.UpdateShipSkinType(shipSkinType);
    }
    private void ColorChangeFun(Color color,bool isBaseTexture)
    {

        shipContent.UpdateShipColor(color, isBaseTexture);
    }



}
