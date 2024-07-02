using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public enum ClothType
    {
        SPEED,
        FORCE,
        NORMAL,
        FASHION
    }

    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;

        public ClothSetup GetSetupByType(ClothType clothType)
        {
            return clothSetups.Find(i => i.clothType == clothType);
        }
    }

    [System.Serializable]
    public class  ClothSetup
    {
        public ClothType clothType;
        public Texture2D texture;
    }
}