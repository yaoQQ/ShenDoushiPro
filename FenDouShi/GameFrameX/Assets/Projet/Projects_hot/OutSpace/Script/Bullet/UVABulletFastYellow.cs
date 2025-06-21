using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UVABulletFastYellow : Bullet
{
    public override void Active()
    {
        base.Active();
        timeLife = 3;
    }

}

