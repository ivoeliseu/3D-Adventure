using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items; //Usa o name Space

public class PlayerMagnectTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        ItemCollectableBase i = other.transform.GetComponent<ItemCollectableBase>();
        if (i != null)
        {
            i.gameObject.AddComponent<Magnect>();
        }
    }
}
