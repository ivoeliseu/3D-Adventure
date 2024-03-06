using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GunShootLimit : GunBase
{
    public List<UIGunUpdate> uiGunUpdate;

    public float maxShoot = 5f;
    public float timeToRecharge = 1f;

    private float _currentShoot;
    private bool _reload = false;

    private void Awake()
    {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (_reload) yield break;

        while (true)
        {
            if(_currentShoot < maxShoot)
            {
                Shoot();
                _currentShoot++;
                CheckReload();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void CheckReload()
    {
        if(_currentShoot >= maxShoot)
        {
            StopShoot();
            StartReload();
        }
    }

    private void StartReload()
    {
        _reload = true;
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        float time = 0;
        while (time < timeToRecharge)
        {
            time += Time.deltaTime;
            Debug.Log("Reloading");
            uiGunUpdate.ForEach(i => i.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShoot = 0;
        _reload = false;
    }

    private void UpdateUI()
    {
        uiGunUpdate.ForEach(i => i.UpdateValue(maxShoot, _currentShoot));
    }
    private void GetAllUIs()
    {
        uiGunUpdate = GameObject.FindObjectsOfType<UIGunUpdate>().ToList();
    }

}
