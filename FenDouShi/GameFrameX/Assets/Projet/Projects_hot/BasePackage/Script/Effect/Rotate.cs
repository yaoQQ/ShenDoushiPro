using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	/// <summary>The speed of the rotation in degrees per second.</summary>
	public Vector3 AngularVelocity = Vector3.up;

	/// <summary>The rotation space.</summary>
	public Space RelativeTo;

	public float rotateSpeed = 1f;

	public Vector3 faward = Vector3.zero;
	public float moveSpeed = 1f;
	public float motionTime = 10f;

	public Transform moveTarget;

	public bool isAotoRotate = false;
	void Update()
	{
		if (!isAotoRotate)
		{
			if (motionTime > 0)
				motionTime -= Time.deltaTime;
			if (motionTime < 0)
			{
				return;
			}
			if (moveTarget)
			{
				Vector3 tagetPos = Vector3.Lerp(this.transform.position, moveTarget.position + Vector3.forward, Time.deltaTime * moveSpeed);
				this.transform.position = tagetPos;
				return;
			}
			transform.Translate(faward * Time.deltaTime * moveSpeed);
		}
		transform.Rotate(AngularVelocity * Time.deltaTime* rotateSpeed, RelativeTo);
		
	}
}
