using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Test to Create All ships SkinType and random  color
/// </summary>
public class CloneTest : MonoBehaviour
{
    public ShipContent cloneTarget;
    private int index = 1;
    private List<ShipContent> shipContentList= new List<ShipContent>();

    private bool isCameraMove = true;
    public float cameraMoveSpeed = 0.5f;
    private void Awake()
    {
        if (cloneTarget) {
            shipContentList.Add(cloneTarget);
            ShowAllSkinTypeAndRandomColor();
        }

    }
    private void Update()
    {
        if (isCameraMove)
        {
            Camera.main.transform.position = Camera.main.transform.position + Vector3.forward*Time.deltaTime * cameraMoveSpeed;
        }
    }
    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 300, 50), "click to ShowAllSkinTypeAndRandomColor_" + index))
        {
            ShowAllSkinTypeAndRandomColor();
        }
        if (GUI.Button(new Rect(0,50, 200, 50), "isCameraMove=" + isCameraMove))
        {
            isCameraMove = !isCameraMove;
        }
    }
    private void ShowAllSkinTypeAndRandomColor()
    {
        for(var i= ShipSkinType.SelfTextureAndNotMatallic; i< ShipSkinType.None; i++)
        {
            createShipContent(i);
        }
    }
    private void createShipContent(ShipSkinType skinType)
    {
        GameObject obj = GameObject.Instantiate(cloneTarget.gameObject);
        obj.name = "ship_" + skinType;
        ShipContent shipContent = obj.GetComponent<ShipContent>();
        obj.transform.localScale = Vector3.one;
        obj.transform.position = cloneTarget.transform.position+ new Vector3(0, 0,index * 100);
        shipContentList.Add(shipContent);
        shipContent.UpdateAllShipSkinType(skinType);
        Color baseColor = RandomColor();
        Color trimColor = RandomColor();
        shipContent.UpdateAllShipColor(baseColor, true);
        shipContent.UpdateAllShipColor(trimColor, false);

        obj.name = "ship_"+index+"_" + skinType;
        index++;
    }
    private Color RandomColor()
    {
        float a = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        float c = Random.Range(0f, 1f);
        return new Color(a, b, c);
    }
}
