using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f; //Tempo at� o proj�til encerrar
    public int damageAmount = 1; //Dano causado
    public float speed = 50f; //Velocidade do proj�til

    public List<string> tagsToHit;


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
        //Se colidir com algum objeto com a tag demarcada na lista, executa o c�digo
        foreach(var t in tagsToHit)
        {
            if (collision.transform.tag == t)
            {
                //Se colidir com algum objeto com interface de dano, aplica dano
                var damageable = collision.transform.GetComponent<DamageInterface>();

                //transfere o valor do dano do proj�til para o objeto.
                if (damageable != null)
                {
                    //Calcula o recuo do objeto baseado de onde sofreu o dano da bala.
                    Vector3 dir = collision.transform.position - transform.position;
            
                    //Normaliza n�meros quebrados (ex: 0.00025 vira 0.1)
                    dir = -dir.normalized;

                    //N�o mexe na dire��o Y do objeto acertado pela bala
                    dir.y = 0;

                    //Quantidade de dano da bala
                    damageable.Damage(damageAmount, dir);
                }
                //Se colidir, destr�i a bala
                Destroy(gameObject);

                //Se identificar que colidiu com um alvo da tag, para esse trecho do c�digo
                break;
            }
        }
    }
}
