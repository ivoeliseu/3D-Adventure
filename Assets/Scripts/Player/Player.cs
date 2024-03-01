using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Controller")]
    public CharacterController characterController;
    public float movementSpeed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15;
    public KeyCode jumpButton = KeyCode.Space;

    [Header("Run Setup")]
    public KeyCode runButton = KeyCode.LeftShift;
    public float runSpeed = 1.5f;

    [Header("Animator")]
    public Animator animator;

    private float _vSpeed = 0f;
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
}
