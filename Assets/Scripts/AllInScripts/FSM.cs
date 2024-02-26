using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class FSM : MonoBehaviour
{
    public enum ExampleEnum
    {
        STATE_ONE, 
        STATE_TWO, 
        STATE_THREE
    }

    //Essa variável puxa o script stateMachine
    public StateMachine<ExampleEnum> stateMachine;

    //
    private void Start()
    {
        stateMachine = new StateMachine<ExampleEnum>();
        stateMachine.Init();
        stateMachine.RegisterStates(ExampleEnum.STATE_ONE, new StateBase());
    }
}
