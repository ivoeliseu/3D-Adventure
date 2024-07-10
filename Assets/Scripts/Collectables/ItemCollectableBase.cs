using UnityEngine;

namespace Items
{

    public class ItemCollectableBase : MonoBehaviour
    {
        public SFXType sfxType;
        public ItemType itemType;

        public string compareTag = "Player";
        public ParticleSystem itemParticleSystem;
        public float timeToHide = 3;
        public GameObject graphicItem;

        public Collider itemCollider;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake()
        {
            
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sfxType);
        }

        protected virtual void Collect()
        {
            //Desativa o collider do item
            if (itemCollider != null) itemCollider.enabled = false;
            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke("HideObject", timeToHide);
            OnCollect();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect() 
        {
            PlaySFX();
            if (itemParticleSystem != null) itemParticleSystem.Play();
            if (audioSource != null) audioSource.Play();
            ItemManager.Instance.AddByType(itemType);
        }
    }
}
