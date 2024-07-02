using DG.Tweening;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItemCoin : ChestItemBase
{
    public int coinNumber = 5;
    public GameObject coinObject;

    private List<GameObject> _items = new List<GameObject>();
    public float tweenEndTime = .5f;

    public Vector2 randomRange = new Vector2(-2f, 2f);
    public override void ShowItem()
    {
        base.ShowItem();
        CreateItens(); 
    }

    [NaughtyAttributes.Button]
    //Instancia as moedas que aparecem ao abrir o ba�
    private void CreateItens()
    {
        for(int i = 0; i < coinNumber; i++)
        {
            var item = Instantiate(coinObject);
            item.transform.position = transform.position + Vector3.forward * Random.Range(randomRange.x, randomRange.y) + Vector3.right * Random.Range(randomRange.x, randomRange.y);
            item.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
            _items.Add(item);
        }
    }

    [NaughtyAttributes.Button]
    //Faz a anima��o das moedas sendo coletadas ap�s estarem instanciadas ao abrir o ba�
    public override void Collect()
    {
        base.Collect();
        foreach(var i in _items)
        {
            i.transform.DOMoveY(2f, tweenEndTime).SetRelative();
            i.transform.DOScale(0, tweenEndTime / 2).SetDelay(tweenEndTime / 2);
            ItemManager.Instance.AddByType(ItemType.COIN);
        }
    }
}
