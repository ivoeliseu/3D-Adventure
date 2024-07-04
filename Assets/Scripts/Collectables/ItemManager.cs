using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }

    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itemSetups;

        public SOInt coins;
        public string compareTag = "Coins";
        //public SOHudUpdate hudManager;

        private void Start()
        {
            Reset();
        }

        private void LoadItemsFromSave()
        {
            AddByType(ItemType.COIN, (int) SaveManager.Instance.Setup.coins);
            AddByType(ItemType.LIFE_PACK, (int) SaveManager.Instance.Setup.health);

        }

        private void Reset()
        {
            foreach (var i in itemSetups)
            {
                i.soInt.value = 0;
            }
        }

        //Essa função retorna o tipo do item.
        public ItemSetup GetItemsByType(ItemType itemType)
        {
            return itemSetups.Find(i => i.itemType == itemType);
        }

        //Sistema responsável por adicionar quantia ao coletar o item.
        //ACHA na lista itemSetups: o tipo de item se é igual ao tipo de item coletado. Adiciona no serializavel soInt o valor de amount
        public void AddByType(ItemType itemType, int amount = 1)
        {
            if (amount < 0) return; //Essa checagem faz com que um valor negativo não subtraia ao tentar adicionar
            itemSetups.Find(i => i.itemType == itemType).soInt.value += amount;
        }

        public void RemoveByType(ItemType itemType, int amount = 1)
        {
            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soInt.value -= amount;

            if (item.soInt.value < 0) item.soInt.value = 0; //Se ficar menos que 0, retorna o valor para 0
        }


        [NaughtyAttributes.Button]
        private void AddCoin()
        {
            AddByType(ItemType.COIN);
        }

        [NaughtyAttributes.Button]
        private void AddLifePack()
        {
            AddByType(ItemType.LIFE_PACK);
        }

    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
        public bool consumable = false;
    }
}
