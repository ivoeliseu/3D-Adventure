using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemieShoot : EnemieBase
    {
        public GunBase gunbase;


        protected override void Init()
        {
            base.Init();

            gunbase.StartShoot();
        }
    }
}
