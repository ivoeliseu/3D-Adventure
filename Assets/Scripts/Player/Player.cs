using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cloth;

public class Player : Singleton<Player>
{
    [Header("Player Controller")]
    public CharacterController characterController;
    public float movementSpeed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15;
    public KeyCode jumpButton = KeyCode.Space;

    private float _vSpeed = 0f;

    [Header("Run Setup")]
    public KeyCode runButton = KeyCode.LeftShift;
    public float runSpeed = 1.5f;

    [Header("Animator")]
    public Animator animator;

    [Header("Flash")]
    public List <FlashColor> flashColors;

    [Header("Life")]
    public List<Collider> colliders;
    public HealthBase healthBase;

    [Space]
    [SerializeField] private ClothChanger clothChanger;

    private bool _alive = true;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    public override void Awake()
    {
        base.Awake();
        OnValidate();
        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }


    private void Update()
    {
        PlayerMovement();
    }

    #region INPUTS
    private void PlayerMovement()
    {
        //Controla a rotação ao virar
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        //Controla a velocidade ao se movimentar para a frente
        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * movementSpeed;

        //Aqui será realizada a chegagem de pulo. >> PRECISA SER ANTES DA ATUALIZAÇÃO DA GRAVIDADE <<
        Jump();        

        //Controla a gravidade
        _vSpeed -= gravity * Time.deltaTime;
        speedVector.y = _vSpeed;

        //CONTROLE DE CORRER
        //Variável isWalking bool. Se a variavel "inputAxisVertical" for diferente de zero, o valor será verdadeiro, indicando que que está em movimento.
        bool isWalking = inputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(runButton))
            {
                speedVector *= runSpeed;
                animator.speed = runSpeed;
            }
            else
            {
                animator.speed = 1f; // 1 = Velocidade da animação padrão.
            }
        }

        //Move o personagem conforme velocidade.
        characterController.Move(speedVector * Time.deltaTime);

        //Controla a animação de correr do player
        //Se a variavel "inputAxisVertical" for diferente de zero, o valor será verdadeiro, ativando o trigger Run, caso não for, será falso.
        animator.SetBool("Run", isWalking);
        
    }

    private void Jump()
    {
        //Controla o pulo. Se estiver no chão, personagem poderá pular;
        if (characterController.isGrounded)
        {
            _vSpeed = 0;
            if (Input.GetKeyDown(jumpButton))
            {
                _vSpeed = jumpSpeed;
            }
        }
    }
    #endregion

    #region PLAYER LIFE
    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
        ShakeCamera.Instance.Shake();

    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }

    private void OnKill(HealthBase h)
    {
        //Checa se player está vivo para tocar animação de morte
        if (_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), 3f);
        }
        
    }

    private void Revive()
    {
        _alive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Respawn");
        Respawn();
        Invoke(nameof(TurnOnColliders), .1f);
    }

    //Essa função existe para não bugar com o colisor e dar tempo de respawnar no local correto
    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
    }

    [NaughtyAttributes.Button]
    public void Respawn()
    {

        if (CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetLastCheckpointPosition();
        }
        
    }
    #endregion

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = movementSpeed;
        movementSpeed = localSpeed;
        yield return new WaitForSeconds(duration);
        movementSpeed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        clothChanger.ResetTexture();
    }
}
