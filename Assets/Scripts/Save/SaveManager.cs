using System.IO; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;
using System;
using Cloth;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    private string _path = Application.streamingAssetsPath + "save.txt";

    public int lastLevel;

    public Action<SaveSetup> FileLoaded;

    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        Invoke(nameof(Load), .1f);
    }
    public void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Ivo";
        _saveSetup.coins = 0;
        _saveSetup.health = 0;
        _saveSetup.checkpoint = 0;
        _saveSetup.playerLife = 10;
        _saveSetup.lastCloth = "";
    }

    #region SAVE
    [NaughtyAttributes.Button]
    private void Save()
    {
        //Classe da Unity que auxilia transformar string para Json
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveLastLevel (int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
    }

    [NaughtyAttributes.Button]
    public void SaveItems()
    {
        _saveSetup.coins = (float) Items.ItemManager.Instance.GetItemsByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.health = (float) Items.ItemManager.Instance.GetItemsByType(Items.ItemType.LIFE_PACK).soInt.value;
        Save();
    }
    public void SaveCheckpoint()
    {
        _saveSetup.checkpoint = CheckpointManager.Instance.lastCheckPoint;
        Save();
    }
    #endregion

    private void SaveFile(string json)
    {
        
        string fileLoaded = "";

        //if (File.Exists(path)) fileLoaded = File.ReadAllText(path);

        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    public void Load()
    {
        string fileLoaded = "";
        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;
            FileLoaded.Invoke(_saveSetup);
        }
        else
        {
            CreateNewSave();
            Save();
        }
    }

    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);

    }
    public void SaveName(string name)
    {
        _saveSetup.playerName = name;
        Save();
    }

    public void SavePlayerLife()
    {
        _saveSetup.playerLife = Player.Instance.healthBase.currentLife;
    }
    public void SaveCloth(string cloth)
    {
        _saveSetup.lastCloth = cloth;
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public float coins;
    public float health;
    public int checkpoint;
    public float playerLife;
    public string lastCloth;

    public string playerName;
}
