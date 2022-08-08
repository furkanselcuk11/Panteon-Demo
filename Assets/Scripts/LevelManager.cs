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
        GameManager.gamemanagerInstance.startGame = false;  // Sonraki levele geciste startGame false olur
        scoreType.gameLevel++;  // Bir sonraki icin arttırır
        if (scoreType.gameLevel == SceneManager.sceneCountInBuildSettings)  // Son seviye kaçsa (index degerine göre ) son seviye gelince ilk levele geri döner
        {
            SceneManager.LoadScene(1);  // Oyunun ilk sahnesinin index degerini çalistirir
            scoreType.gameLevel = 1;    // oyunlevelini 1 olarak degistirir
        }
        else
        {
            SceneManager.LoadScene(scoreType.gameLevel);   // Aktif leveli acar
            //Bir sonraki levele geçer
        }
        SaveManager.savemanagerInstance.SaveGame(); // Verileri kaydet
    }
}