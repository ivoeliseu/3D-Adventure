using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootAngle : GunShootLimit
{
    public int amountPerShoot = 4;
    public float angle = 15f;

    public override void Shoot()
    {
        //multiplicador que irá verificar se o número é par
        int mult = 0;

        for (int i = 0; i < amountPerShoot; i++)
        {
            //Verifica se o número é par.
            if (i%2 == 0)
            {
                mult++;
            }

            var projectile = Instantiate(prefabProjectile, positionToShoot);

            projectile.transform.localPosition = Vector3.zero;
            projectile.transform.rotation = positionToShoot.rotation;

            //Vector3.zaro + Vector3.UP (eixo Y, (0/1/0)), se for par, multiplica pelo angulo, se não, multipla - o angulo, multiplado pelo multiplicador
            projectile.transform.localEulerAngles = Vector3.zero + Vector3.up * (i % 2 == 0 ? angle : -angle) * mult;
            projectile.speed = speed;
            projectile.transform.parent = null;
        }
    }
}
