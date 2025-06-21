using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ¾àÀë±È½ÏÆ÷
/// </summary>
public class DistanceComparer : IComparer<EnemyShipBase>
{
	private Transform trans;
	private bool invert;

	public DistanceComparer(Transform t, bool invert)
	{
		this.trans = t;
		this.invert = invert;
	}

	public int Compare(EnemyShipBase x, EnemyShipBase y)
	{
		if (this.trans && x && y)
		{
			float xd = (x.transform.position - this.trans.position).sqrMagnitude;
			float yd = (y.transform.position - this.trans.position).sqrMagnitude;

			return invert ? -xd.CompareTo(yd) : xd.CompareTo(yd);
		}
		return 0;
	}
}