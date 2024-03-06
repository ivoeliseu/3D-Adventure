using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f; //Tempo at� o proj�til encerrar
    public int damageAmount = 1; //Dano causado
    public float speed = 50f; //Velocidade do proj�til


    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        //Move o proj�til para frente, baseado na velocidade do proj�til (estabilizado por deltaTime).
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
