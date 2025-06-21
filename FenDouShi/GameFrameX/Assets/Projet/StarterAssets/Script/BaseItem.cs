using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody))]
public class BaseItem :MonoBehaviour
{
    public Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = this.transform.GetComponent<Rigidbody>();
    }
    public void ItemDistroy()
    {

    }
   
}