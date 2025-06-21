using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelComparer : IComparer<GameGunData>
{

	public int Compare(GameGunData x, GameGunData y)
	{
		if (x!=null&&y!=null)
		{
		

			return (x.level> y.level)?1:0;
		}
		return 0;
	}
}
