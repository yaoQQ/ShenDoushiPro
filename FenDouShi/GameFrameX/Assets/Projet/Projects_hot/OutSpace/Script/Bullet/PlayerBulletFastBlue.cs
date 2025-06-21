using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerBulletFastBlue : Bullet
{
   // private int playerbulletLayer;


    public override void Active()
    {
        base.Active();
        timeLife = 3;
    }
  
}

