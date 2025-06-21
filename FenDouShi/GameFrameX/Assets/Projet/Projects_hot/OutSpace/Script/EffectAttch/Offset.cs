using UnityEngine;
using System.Collections;

public class Offset : MonoBehaviour {

	public Renderer rend;
	public float OffsetX = 0.0f;
	public float OffsetY = 0.0f;

	void Start() {
		rend = GetComponent<Renderer>();
	}
	void Update() {
		float uvX = Time.time * OffsetX;
		float uvY = Time.time * OffsetY;
		rend.material.SetTextureOffset("_MainTex", new Vector2(uvX, uvY));
	}
}