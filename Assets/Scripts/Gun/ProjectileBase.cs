using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f; //Tempo até o projétil encerrar
    public int damageAmount = 1; //Dano causado
    public float speed = 50f; //Velocidade do projétil


    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        //Move o projétil para frente, baseado na velocidade do projétil (estabilizado por deltaTime).
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Se colidir com algum objeto com interface de dano, aplica dano
        var damageable = collision.transform.GetComponent<DamageInterface>();

        //transfere o valor do dano do projétil para o objeto.
        if (damageable != null)
        {
            damageable.Damage(damageAmount);

            //Se colidir, destrói a bala
            Destroy(gameObject);
        }
    }
}
