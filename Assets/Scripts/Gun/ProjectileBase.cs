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

    }
}
