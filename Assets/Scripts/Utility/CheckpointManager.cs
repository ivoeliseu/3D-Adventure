using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckPoint = 0;

    public List<CheckpointBase> checkpoints;

    public bool HasCheckpoint()
    {
        //Se retornar 0, não tem. Se for maior, sim
        return lastCheckPoint > 0;
    }
    public void SaveCheckPoint(int i)
    {
        if(i > lastCheckPoint)
        {
            lastCheckPoint = i;
            SaveManager.Instance.SaveCheckpoint();
        }
    }

    public Vector3 GetLastCheckpointPosition()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckPoint);
        return checkpoint.transform.position;
    }
}
