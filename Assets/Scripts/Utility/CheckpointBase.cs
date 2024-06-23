using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 1;

    private bool checkpointActive = false;

    private string _checkpointKey = "CheckpointKey";
    private void OnTriggerEnter(Collider other)
    {
        if (!checkpointActive && other.transform.tag == "Player")
        {
            CheckCheckpoints();
        }
        
    }

    private void CheckCheckpoints()
    {
        SaveCheckPoint();
        TurnOn();
    }

    [NaughtyAttributes.Button]
    private void TurnOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    [NaughtyAttributes.Button]
    private void TurnOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.grey);
    }

    private void SaveCheckPoint()
    {
        /*if (PlayerPrefs.GetInt(_checkpointKey, 0) > key)
        PlayerPrefs.SetInt(_checkpointKey, key);*/

        CheckpointManager.Instance.SaveCheckPoint(key);

        checkpointActive = true;
    }
}
