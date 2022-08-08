using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gamemanagerInstance;

    public bool startGame; // Oyun basladimi
    public bool isFinish; // Level bittimi
    private void Awake()
    {
        if (gamemanagerInstance == null)
        {
            gamemanagerInstance = this;
        }
    }
    void Start()
    {
        startGame = false;  // Oyun ilk basladiginda startGame false olur
        isFinish = false;   // Oyun ilk basladiginda isFinish false olur
        AudioController.audioControllerInstance.Play("BgSound");    // Arkaplan sesi calar
    }
    
    void Update()
    {
        
    }
    public void StartTheGame()
    {
        startGame = true;
        isFinish = false;
    }
}
