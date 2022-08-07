using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ScoreSO scoreType = null;    // Scriptable Objects eriþir 
    void Start()
    {
      
    }
   
    public void NextLevel()
    {
        // Sonraki Level gecer
        GameManager.gamemanagerInstance.startGame = false;
        scoreType.gameLevel++;  // Bir sonraki icin arttırır
        if (scoreType.gameLevel == SceneManager.sceneCountInBuildSettings)  // Son seviye kaçsa (index deðerine göre ) son seviye gelince ilk levele geri döner
        {
            SceneManager.LoadScene(1);  // Oyunun ilk sahnesinin index degerini çalistirir
            scoreType.gameLevel = 1;    // oyunlevelini 1 oalrak ekler
        }
        else
        {
            SceneManager.LoadScene(scoreType.gameLevel);   // Currentevel+1 diye deðiþtir
            //Bir sonraki levele geçer
        }
        SaveManager.savemanagerInstance.SaveGame();
    }
}