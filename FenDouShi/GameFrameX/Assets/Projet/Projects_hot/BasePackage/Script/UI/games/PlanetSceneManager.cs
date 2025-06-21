using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isPressUp = false;
    void Start()
    {

    }
    SelectPlanet selectPlanet = null;
    private void Update()
    {



        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {//画出射线
            Debug.DrawLine(ray.origin, hit.point);

            if (hit.collider.tag != "Planet")
            {
                if (selectPlanet != null)
                {
                    selectPlanet.MouseLeave();
                    selectPlanet = null;
                }
                isPressUp = false;
                return;
            }
            selectPlanet = hit.collider.gameObject.GetComponent<SelectPlanet>();

            ////通过标签来判断
            if (selectPlanet != null)
            {
                selectPlanet.MouseIn();
                //根据需求来实现所需要的功能
                if (!isPressUp&&Input.GetMouseButtonUp(0))
                {

                    selectPlanet.ShowScene();
                    isPressUp = true;
                }
            }

        }

    }





}
