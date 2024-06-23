using Animation;
using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    public class EnemieBase : MonoBehaviour, DamageInterface
    {
        [Header("Enemy Stats")]
        public float startLife = 10;
        public float timeToDie = 2f;
        public float damageToPlayer = 1f;
        public bool lookAtPlayer = false;

        [SerializeField] private float _currentLife;

        public Collider enemieCollider;
        public ParticleSystem particles;
        public FlashColor flashColor;
        public UIEnemyUpdate uiUpdate;

        [Header("Spawn Animation")]
        public float startAnimationDuration = .2f;
        public Ease StartAnimationEase = Ease.OutBack;
        public bool spawnAnimation = true;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        private Player _player;

        private void Awake()
        {
            //Ao começar, pega o componente Colisor do inimigo
            enemieCollider = GetComponent<Collider>();
            Init();
            if (gameObject.GetComponent<MovimentHelper>() != null) PlayAnimationByTrigger(AnimationType.RUN);
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }
        //Reinicia a vida do inimigo para a base
        private void ResetLife()
        {
            _currentLife = startLife;
        }
        //Realiza operações quando iniciar
        protected virtual void Init()
        {
            ResetLife();
            SpawnAnimation();
        }
        //Caso morra, ocorre algumas solicitações e após, em OnKill, destrói o inimigo
        protected virtual void Kill()
        {
            //Quando o inimigo morrer, SE O INIMIGO TIVER UM COLISOR, desativa o colisor para que execute a animação de morte sem dar dano ao jogador
            if (enemieCollider != null) enemieCollider.enabled = false;
            OnKill();
        }
        protected virtual void OnKill()
        {
            //Destruirá o objeto após alguns segundos, para executar animação de morte
            Destroy(gameObject, timeToDie);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float damage)
        {
            //Se tiver o script de piscar o objeto, irá piscar.
            if (flashColor != null) flashColor.Flash();
            if (particles != null) particles.Emit(15);

            _currentLife -= damage;
            uiUpdate.UpdateValue(startLife, _currentLife);

            if (_currentLife <= 0)
            {
                Kill();
            }
        }

        //DEBUG
        public virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.L)) 
            {
                OnDamage(5f);
            }

            if (lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }

        private void SpawnAnimation()
        {
            if (spawnAnimation)
            {
                //Normalmente iria escalar a animação para 0, mas o FROM ao final inverte,
                //fazendo com que ele inicie em 0 e vá para a escala que está naturalmente.

                transform.DOScale(0, startAnimationDuration).SetEase(StartAnimationEase).From() ;
            }
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        public void Damage(float damage)
        {
            Debug.Log("Damage");
            OnDamage(damage);
        }

        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Identifica se foi um objeto com o script PLAYER que bateu nele.
            Player p = collision.transform.GetComponent<Player>();

            //Se for o player que colidiu, causa dano.
            if(p != null)
            {
                p.healthBase.Damage(damageToPlayer);
            }
        }
    }
}
