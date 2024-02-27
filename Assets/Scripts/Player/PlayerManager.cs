using StateMachine;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("Player Controller")]
    public GameObject player;
    public float movementSpeed = .01f;

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

    private void Update()
    {
        PlayerMovement();
    }

    #region INPUTS
    private void PlayerMovement()
    {
        stateMachine.SwitchState(PlayerStates.RUNNING);
        //Variáveis capturam o movimento
        var xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
        var yMovement = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;
        //Pega o Movimento tanto para esquerda e quanto para cima e para baixo.
        player.transform.Translate( xMovement, 0, yMovement);
        stateMachine.SwitchState(PlayerStates.IDLE);
    }

    #endregion
}
