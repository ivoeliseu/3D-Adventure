using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBase : MonoBehaviour
{
    public Animator animator;
    public string triggerOpen = "Open";
    public string playerTag = "Player";
    public KeyCode keyCode = KeyCode.Z;

    private bool _chestOpened = false;

    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;

    private float _startScale;
    
    [Space]
    public ChestItemBase chestItem;
    public float timeToShowItem = 1f;
    

    private void Start()
    {
        _startScale = notification.transform.localScale.x;
    }
    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if (_chestOpened) return;
        animator.SetTrigger(triggerOpen);
        Invoke(nameof(ShowItem), timeToShowItem);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), timeToShowItem);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == playerTag)
        {
            ShowNotification();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == playerTag)
        {
            HideNotification();
        }
    }
    [NaughtyAttributes.Button]
    public void ShowNotification()
    {
        if (_chestOpened) return;
        notification.SetActive(true);

        //Animação: Primeiro irá zerar e depois irá aumentar ao tamanho certo.
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(_startScale, tweenDuration);
    }
    [NaughtyAttributes.Button]
    public void HideNotification()
    {
        notification.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode) && notification.activeSelf)
        {
            OpenChest();
            _chestOpened = true;
            HideNotification();
        }
    }
}
