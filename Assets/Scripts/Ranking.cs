using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ranking : MonoBehaviour
{
    [SerializeField] private GameObject aiOpponentsParent;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform finishPoint;
    [SerializeField] private List<GameObject> rankingList=new List<GameObject>();
    public int indexNo = 0;
    void Start()
    {
        ListAdd();
        ListUpdate();                
    }    
    void Update()
    {
        if (!GameManager.gamemanagerInstance.isFinish)
        {
            ListUpdate();
        }
        else
        {
            //UIController.uicontrollerInstance.finishRankText.SetText(indexNo.ToString());
        }
    }
    private void ListAdd()
    {
        rankingList.Add(player);
        for (int i = 0; i < aiOpponentsParent.transform.childCount; i++)
        {
            rankingList.Add(aiOpponentsParent.transform.GetChild(i).transform.gameObject);
        }
    }
    private void ListUpdate()
    {
        // Finish mesafesine göre listesiyi sýrala
        rankingList = rankingList.OrderBy((d) => (d.transform.position - finishPoint.position).sqrMagnitude).ToList();        
        foreach (var item in rankingList)
        {
            if (item.transform.name == player.transform.name)
            {
                indexNo = rankingList.IndexOf(item) + 1;                
            }
        }
        UIController.uicontrollerInstance.rankingText.text=indexNo+" / 11";
    }
}
