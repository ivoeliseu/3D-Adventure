using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityBase : MonoBehaviour
{
    protected Player player;
    protected Inputs inputs;

    private void OnValidate()
    {
        if (player == null ) player = GetComponent<Player>();
    }

    private void Start()
    {
        inputs = new Inputs();
        inputs.Enable();

        Init();
        OnValidate();
        RegisterListener();
    }

    private void OnEnable()
    {
        if(inputs != null)
            inputs.Enable();
    }

    protected virtual void Init()
    {

    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    protected virtual void RegisterListener()
    {

    }

    protected virtual void RemoveListeners()
    {

    }
}
