using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemieMoviment : EnemieBase
    {
        [Header("Waypoints")]
        public GameObject[] waypoints;
        public float minDistance = 1f;
        public float speed = 1f;

        private int _index = 0;

        public override void Update()
        {
            base.Update();
            //Se detectar que chegou até o objeto do index, vai para o próximo index. Se chegar ao final da lista, reseta.
            if (Vector3.Distance(transform.position, waypoints[_index].transform.position) < minDistance)
            {
                _index++;
                if(_index >= waypoints.Length )
                {
                    _index = 0;
                }
            }

            //Move o inimigo até o atual index.
            transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].transform.position, Time.deltaTime * speed);
            transform.LookAt(waypoints[_index].transform.position);
        }

    }

}
