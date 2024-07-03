using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructablesItemsBase : MonoBehaviour
{
    public HealthBase healthBase;

    public float shakeDuration = .1f;
    public int shakeForce = 1;

    public int dropCoinsAmount = 10;
    public GameObject coinPrefab;
    public Transform dropPosition;
    public ParticleSystem destructableObjectParticle;

    private void OnValidate()
    {
        if (healthBase != null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += OnDamage;
    }

    private void OnDamage(HealthBase h)
    {
        //Faz o objeto balançar quando tomar dano
        gameObject.transform.DOShakeScale(shakeDuration, Vector3.up/2, shakeForce);
        DropCoins();
        destructableObjectParticle.Play();
}

private void DropCoins()
    {
        //Instancia a moeda na posição de dropPosition
        var i = Instantiate(coinPrefab);
        i.transform.position = dropPosition.position;

        //Faz a animação da moeda "crescendo" ao sair da árvore;
        i.transform.DOScale(0, .5f).SetEase(Ease.OutBack).From() ;
    }

    [NaughtyAttributes.Button]
    private void DropGroupOfCoins()
    {
        StartCoroutine(DropGroupOfCoinsCoroutine());
    }

    IEnumerator DropGroupOfCoinsCoroutine()
    {
        for (int i = 0; i < dropCoinsAmount; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(.1f);
        }
    }
}
