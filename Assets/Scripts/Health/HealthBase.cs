using Animation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;
using static UnityEngine.ParticleSystem;

public class HealthBase : MonoBehaviour, DamageInterface //Nas aulas estava como Idamageble
{
    public float startLife = 10f;
    public bool destroynOnKill = false;
    [SerializeField] private float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public UIFillUpdate healthUpdate;

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
        healthUpdate.UpdateValue(_currentLife);
    }

    protected virtual void Kill()
    {
        if (destroynOnKill)
        {
            //Destruirá o objeto após alguns segundos, para executar animação de morte
            Destroy(gameObject, 3f);
        }
        
        OnKill?.Invoke(this);
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

        UpdateUI();
        OnDamage?.Invoke(this);
    }

    //Interface de dano
    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        healthUpdate.UpdateValue((float) _currentLife / startLife);
    }
}
