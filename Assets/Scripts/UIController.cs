using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController uicontrollerInstance;

    [Header("Game UI Controller")]
    [SerializeField] private GameObject GameStartPanel;
    [SerializeField] private GameObject GameRunTimePanel;
    [SerializeField] private GameObject GameFinishPanel;
    public TextMeshProUGUI rankingText;
    public TextMeshProUGUI finishRankText;
    
   
    private void Awake()
    {
        if (uicontrollerInstance == null)
        {
            uicontrollerInstance = this;
        }
    }
    void Start()
    {
        
    }
    
    void Update()
    {
        UpdatePanel();
    }
    public void UpdatePanel()
    {
        if (GameManager.gamemanagerInstance.startGame & !GameManager.gamemanagerInstance.isFinish)
        {
            GameStartPanel.SetActive(false);
            GameRunTimePanel.SetActive(true);
            GameFinishPanel.SetActive(false);
        }
        else
        {
            GameStartPanel.SetActive(false);
            GameRunTimePanel.SetActive(false);
            GameFinishPanel.SetActive(true);
        }
        if (!GameManager.gamemanagerInstance.startGame & !GameManager.gamemanagerInstance.isFinish)
        {
            GameStartPanel.SetActive(true);
            GameRunTimePanel.SetActive(false);
            GameFinishPanel.SetActive(false);
        }
    }
}
