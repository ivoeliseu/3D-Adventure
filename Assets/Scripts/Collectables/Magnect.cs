using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magnect : MonoBehaviour
{
    public float dist = .2f;
    public float coinSpeed = 3f;
    void Update()
    {
        if(Vector3.Distance(transform.position, Player.Instance.transform.position) > dist)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, Time.deltaTime * coinSpeed);
            coinSpeed++;
        }
    }
}
