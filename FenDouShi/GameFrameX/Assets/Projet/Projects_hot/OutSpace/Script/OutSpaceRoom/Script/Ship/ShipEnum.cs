using UnityEngine;
using System.Collections;

//ship's skin
public enum ShipSkinType
{
    SelfTextureAndNotMatallic = 0,//The ship's own skin and It's mat did not have Matallic
    CommonSmooth =1,//Communal smooth skin
    CommonScatter=2,//Communal  Scatter skin
  
    SelfTextureAndSmmothMatallic =3,//The ship's own skin and It's mat  have SmmothMatallic
    SelfTextureAndScattleMatallic=4,//The ship's own skin and It's mat  have ScattleMatalli

    None
}
public enum ShipType
{
    ScatterShip,
    BezierShip,
    AircraftCarrierShip,
    ShipA0401,
    ShipA0501,
    ShipA0601,
    ShipA0602,



    CashShip,
    UVAShip,

    None
}
public class ShipEnum 
{

 
}
