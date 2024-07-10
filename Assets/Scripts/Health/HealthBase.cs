using Animation;
using Cloth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

public class HealthBase : MonoBehaviour, DamageInterface //Nas aulas estava como Idamageble
{
    public float startLife = 10f;
    public bool destroynOnKill = false;
    public float timeToDestroy = 3f;
    [SerializeField] public float currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public UIFillUpdate healthUpdate;
    public float damageMultiplier = 1;

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
        currentLife = startLife;
        healthUpdate.UpdateValue(currentLife);
    }

    protected virtual void Kill()
    {
        if (destroynOnKill)
        {
            //Destruirá o objeto após alguns segundos, para executar animação de morte
            Destroy(gameObject, timeToDestroy);
        }
        
        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }
    public void Damage(float damage)
    {
        currentLife -= damage * damageMultiplier;

        if (currentLife <= 0)
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
        if (healthUpdate == null) return;
        healthUpdate.UpdateValue((float) currentLife / startLife);
    }

    public void ChangeDamageMultiplier(float damage, float duration)
    {
        StartCoroutine(ChangeDamageMultiplierCoroutine(damageMultiplier, duration));
    }

    IEnumerator ChangeDamageMultiplierCoroutine(float damageMultiplier, float duration)
    {
        //This diz que é o damage do scritp mesmo.
        this.damageMultiplier = damageMultiplier;
        yield return new WaitForSeconds(duration);
        this.damageMultiplier = 1;
    }
}
