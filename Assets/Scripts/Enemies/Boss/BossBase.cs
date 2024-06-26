using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using DG.Tweening;
using System;
using Unity.VisualScripting;

namespace Boss
{
    //Boss States
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }


    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startingAnimationEase = Ease.OutBack;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = 1f;

        [Header("Coroutine")]
        public float speed = 15f;
        public List<Transform> waypoints;

        public HealthBase healthBase;

        private StateMachine<BossAction> stateMachine;

        private void OnValidate()
        {
            if (healthBase == null) healthBase = GetComponent<HealthBase>();
        }

        private void Awake()
        {
            Init();
            healthBase.OnKill += OnBossKill;
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
        }

        #region BOSS
        private void OnBossKill(HealthBase h)
        {
            SwitchState(BossAction.DEATH);
        }
        #endregion

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallBack)
        {
            int attacks = 0;
            while (attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            endCallBack?.Invoke();
        }
        #endregion
        #region BOSS MOVIMENT
        public void GoToRandomPoint(Action onArrive)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }
        IEnumerator GoToPointCoroutine(Transform t, Action onArrive)
        {
            while(Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Player")
            {
                SwitchState(BossAction.WALK);
            }
        }

        #endregion
        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startingAnimationEase).From();
        }
        #endregion
        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }

        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }

        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }
        #endregion
        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }
        #endregion
    }
}
