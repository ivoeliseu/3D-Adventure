using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemLayoutManager : Singleton<ItemLayoutManager>
    {
        public ItemLayout prefabLayout; //O Prefab com o icon e textmesh
        public Transform container; //O próprio Horizontal Grupo

        public List<ItemLayout> itemLayouts;

        private void Start()
        {
            CreateItens();
        }

        private void CreateItens()
        {
            foreach(var setup in ItemManager.Instance.itemSetups) 
            {
                var item = Instantiate(prefabLayout, container);
                item.Load(setup);
                itemLayouts.Add(item);
            }
        }
    }
}
