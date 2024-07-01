using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    public int coinValue = 1;
    protected override void Collect()
    {
        base.Collect();
        //itens (namespace). ItemType (Lista).COIN (o que coletou)
        ItemManager.Instance.AddByType(ItemType.COIN, coinValue);
        //Debug.Log("Coletou moeda");
    }
}
