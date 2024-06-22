using Animation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;
using static UnityEngine.ParticleSystem;

public class HealthBase : MonoBehaviour
{
    public float startLife = 10f;
    public bool destroynOnKill = false;
    [SerializeField] private float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public void Awake()
    {
        Init();
    }
    public void Init()
    {
        ResetLife();
    }
    public void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Kill()
    {
        if (destroynOnKill)
        {
            //Destruirá o objeto após alguns segundos, para executar animação de morte
            Destroy(gameObject, 3f);

            OnKill?.Invoke(this);
        }
    }

    public void Damage()
    {
        Damage(5);
    }
    public void Damage(float damage)
    {
        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            Kill();
        }

        OnDamage?.Invoke(this);
    }
}
