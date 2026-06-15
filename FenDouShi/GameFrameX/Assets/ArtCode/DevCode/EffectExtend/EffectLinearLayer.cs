using Sirenix.OdinInspector;
using UnityEngine;

[ExecuteInEditMode]
public class EffectLinearLayer : MonoBehaviour
{
    public GameObject controlA = null;
    public GameObject controlB = null;
    public GameObject layerNode = null;
    public Vector2 scale = Vector2.one;
    public bool debugMode = true;
    public bool debugWire = false;

    [Button("Create Control Node")]
    public void CreateControlNode()
    {
        controlA = new GameObject("ControlA");
        controlA.transform.SetParent(transform);
        controlA.transform.localPosition = new Vector3(-0.5f, 0, 0);

        controlB = new GameObject("ControlB");
        controlB.transform.SetParent(transform);
        controlB.transform.localPosition = new Vector3(0.5f, 0, 0);

        layerNode = new GameObject("LayerNode");
        layerNode.transform.SetParent(transform);
        layerNode.transform.localPosition = controlB.transform.localPosition + (controlA.transform.localPosition - controlB.transform.localPosition)*0.5f;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(controlA == null || controlB == null || layerNode == null)
        {
            return;
        }

        var dir = controlA.transform.position - controlB.transform.position;
        layerNode.transform.rotation = Quaternion.LookRotation(dir);

        layerNode.transform.position = controlB.transform.position + dir * 0.5f;

        layerNode.transform.localScale =   new Vector3(scale.x, scale.y, dir.magnitude / this.transform.lossyScale.z) ;

        controlA.transform.rotation = Quaternion.LookRotation(dir);
        controlB.transform.rotation = Quaternion.LookRotation(dir);

        
        

    }


    private void OnDrawGizmos()
    {
        if (!debugMode )
        {
            return;
        }

        if (controlA != null) {

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(controlA.transform.position, 0.1f);
        }

        if (controlB != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(controlB.transform.position, 0.1f);
        }

        if (controlA != null && controlB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(controlA.transform.position, controlB.transform.position);

            if(layerNode != null && debugWire)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireMesh(CreateCubeMesh(), layerNode.transform.position, layerNode.transform.rotation, layerNode.transform.localScale);
            }
        }
    }

    Mesh CreateCubeMesh()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = {
        new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.5f, -0.5f, -0.5f),
        new Vector3(0.5f, 0.5f, -0.5f), new Vector3(-0.5f, 0.5f, -0.5f),
        new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0.5f, -0.5f, 0.5f),
        new Vector3(0.5f, 0.5f, 0.5f), new Vector3(-0.5f, 0.5f, 0.5f)
    };
        int[] triangles = {
        0,1,2, 0,2,3, 4,6,5, 4,7,6, // 前后
        0,4,5, 0,5,1, 2,6,7, 2,7,3, // 左右
        0,3,7, 0,7,4, 1,5,6, 1,6,2  // 上下
    };
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }
}
