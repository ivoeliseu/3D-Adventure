using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class EnemieShoot : EnemieBase
    {
        public GunBase gunbase;


        protected override void Init()
        {
            base.Init();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                gunbase.StartShoot();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                gunbase.StopShoot();
            }
        }

    }
}
