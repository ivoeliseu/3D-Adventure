using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;


namespace Boss
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;

        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss = (BossBase)objs[0];
        }
    }

    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            Debug.Log("Boss: " + boss);
            boss.StartInitAnimation();
        }
    }

    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            Debug.Log("Boss Walking: " + boss);
            boss.GoToRandomPoint(OnArrive);
        }

        private void OnArrive()
        {
            boss.SwitchState(BossAction.ATTACK);
        }

        public override void OnStateExit()
        {
            Debug.Log("Exit WALK");
            base.OnStateExit();
            boss.StopAllCoroutines();
        }
    }
    public class BossStateAttack : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            Debug.Log("Boss Attacking: " + boss);
            boss.StartAttack(EndAttacks);
        }

        private void EndAttacks()
        {
            boss.SwitchState(BossAction.WALK);
        }

        public override void OnStateExit()
        {
            Debug.Log("Exit ATTACK");
            base.OnStateExit();
            boss.StopAllCoroutines();
        }
    }
    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            Debug.Log("Boss DEAD: " + boss);
            boss.transform.localScale = Vector3.one * .2f;
        }
        
    }


}
