using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum States
    {
        STATE1,
        STATE2,
        STATE3
    }

    public Dictionary<States, StateBase> dictionaryState;

    private StateBase _currentState;

    private void Awake()
    {
        dictionaryState = new Dictionary<States, StateBase>();
        dictionaryState.Add(States.STATE1, new StateBase());    
        dictionaryState.Add(States.STATE2, new StateBase());    
        dictionaryState.Add(States.STATE3, new StateBase());

        SwitchState(States.STATE1);
    }

    private void SwitchState(States state)
    {
        if (_currentState != null) _currentState.OnStateExit();
        _currentState = dictionaryState[state];
        _currentState.OnStateEnter();
    }

    private void Update()
    {
        if (_currentState != null) _currentState.OnStateStay();
    }
}
