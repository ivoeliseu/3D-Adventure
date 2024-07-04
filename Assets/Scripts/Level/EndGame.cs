using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public List <GameObject> endGameObjects;
    public int currentLevel = 1;
    [SerializeField]private bool _endGame = false;

    private void Awake()
    {
        endGameObjects.ForEach(i => i.SetActive(false));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_endGame == true && other.transform.tag == "Player")
        {
            ShowEndGame();
        }
    }
    private void ShowEndGame()
    {
        _endGame = true;
        //endGameObjects.ForEach(i => i.SetActive(true));

        foreach(var i in endGameObjects) 
        {
            i.SetActive(true);
            i.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
            SaveManager.Instance.SaveLastLevel(currentLevel);
            SaveManager.Instance.SaveItems();
        }
    }
}
