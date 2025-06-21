using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 球体引力
/// </summary>
public class GravityAttractor : MonoBehaviour
{
    public float gravity = 10f;//设置重力的大小
    private MeshRenderer render;
   // [HideInInspector]
    public Vector3 targetDir = Vector3.zero;

    public bool isGrassPlanet = false;

    [SerializeField]
    public float planetRudia = 1;
    void Awake()
    {
        render = this.transform.GetComponent<MeshRenderer>();
        planetRudia = this.transform.localScale.x / 2;
    }
 

    /// <summary>
    /// 模拟重力
    /// </summary>
    /// <param name="body">被施加力的对象</param>
    public void Attractor(BaseItem body)
    {

        targetDir = (transform.position-body.transform.position).normalized;//朝向星球的引力方向
        Vector3 bodyDown = -body.transform.up;//物体向上

        body.transform.rotation = Quaternion.FromToRotation(bodyDown, targetDir) * body.transform.rotation;//从自身到目标方向X身体当前的旋转=使目标Up方向朝星球
        body.rigidBody.AddForce(targetDir * gravity);//模拟重力，向下推
    }
    public void enterPlanet()
    {

        ThirdPersonController.Instance.planetAttract = this;
    }
    public void ClickPlanet()
    {
      
        //Shader shader = Shader.Find("MyShader/TraceOutLine");
        //Material mat2 = new Material(shader);
      
        ////render.materials[1] = mat2;
        ////render.materials[1].shader = shader;
        //Material[] materials = new Material[] {
        //    render.materials[0],
        //    mat2,
        //};
        //render.materials = materials;
        //Debug.Log("ClickPlanet()");
      
    }
    public void CancelClickPlanet()
    {
        //Material[] materials = new Material[] {
        //    render.materials[0],
        //};
        //render.materials = materials;
        //Debug.Log("@@@@@@@cancelClickPlanet()");
    }
}
