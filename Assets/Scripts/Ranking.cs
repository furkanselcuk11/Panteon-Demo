using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ranking : MonoBehaviour
{
    [SerializeField] private GameObject aiOpponentsParent;  // AI rakiplerin tutuldugu ana parent
    [SerializeField] private GameObject player; // oyuncu   
    [SerializeField] private Transform finishPoint; // Finish noktasi
    [SerializeField] private List<GameObject> rankingList=new List<GameObject>();   // Siralama listesi
    public int indexNo = 0;
    void Start()
    {
        ListAdd();  // Siralama Listesine ekleme
        ListUpdate(); // Siralama Listesini guncelle               
    }    
    void Update()
    {
        if (!GameManager.gamemanagerInstance.isFinish)
        {
            ListUpdate();   // eger finish aktif degilse Siralama Listesini guncelle
        }
    }
    private void ListAdd()
    {
        rankingList.Add(player);    // oyuncuyu Siralama Listesine ekle
        for (int i = 0; i < aiOpponentsParent.transform.childCount; i++)
        {
            // aiOpponentsParent altindaki AI rakipleri Siralama Listesine ekle
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
        UIController.uicontrollerInstance.rankingText.text=indexNo+" / 11"; // Oyuncun siralamadaki yerini goster
    }
}
