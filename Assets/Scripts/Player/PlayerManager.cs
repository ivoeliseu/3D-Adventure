using StateMachine;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    #region STATE MACHINE
    public enum PlayerStates
    {
        IDLE,
        RUNNING,
        JUMPING,
        DEAD
    }

    public StateMachine<PlayerStates> stateMachine;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<PlayerStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(PlayerStates.IDLE, new StateBase());
        stateMachine.RegisterStates(PlayerStates.RUNNING, new StateBase());
        stateMachine.RegisterStates(PlayerStates.JUMPING, new StateBase());
        stateMachine.RegisterStates(PlayerStates.DEAD, new StateBase());

        stateMachine.SwitchState(PlayerStates.IDLE);
    }
    #endregion

    
}
