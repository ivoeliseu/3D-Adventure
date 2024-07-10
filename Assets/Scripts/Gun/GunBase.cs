using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [Header("Projectile")]
    public ProjectileBase prefabProjectile;
    public float speed = 50f;

    public Transform positionToShoot; //Local onde o projétil da arma é instanciado.
    public float timeBetweenShoot = .3f;

    private Coroutine _currentCoroutine;

    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public virtual void Shoot()
    {
        SFXPool.Instance.Play(SFXType.TYPE_03);
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;
    }

    public void StartShoot()
    {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }
}
