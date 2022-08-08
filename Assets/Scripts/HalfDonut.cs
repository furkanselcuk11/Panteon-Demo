using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonut : MonoBehaviour
{   

    [SerializeField] private float speed = 5f;  // Hareket hizi
    [SerializeField] private float timer=5f;    // Ne kadar surede calisacagi
    [SerializeField] private bool isMove;   // hareket ediyor mu

    private Vector3 startpos1;
    private Vector3 startpos2;
    void Start()
    {
        startpos1 = transform.localPosition; // HalfDonut objesinin ilk konumu
        startpos2 = new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z); // HalfDonut objesinin ilk konumunun tersi
        isMove = true;  // hareket et
        StartCoroutine(nameof(MoveDonut)); //MoveDonut fonk. calis
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (isMove)
        {
            // Eger ismove true ise HalfDonut objesi startpos1 ve startpos2 pozisyonlari arasinda hareket eder
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startpos1, Time.fixedDeltaTime * speed); // Speed degerinde pozisyonu degisir
            if (transform.localPosition == startpos1)
            {
                // Pozisyon her degistiginde startpos1 ve startpos1 degerleri yer degistirir dongu halinde devam etmesi icin
                startpos1 = startpos2;
                if (startpos1 == startpos2)
                {
                    startpos2 = transform.localPosition;
                }
            }

        }
        else
        {
            //Eger ismove false ilk konuma döner
            Vector3 targetPos = new Vector3(0.16f, 0f, 0f);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, Time.fixedDeltaTime * speed);
        }
    }
    IEnumerator MoveDonut()
    {        
        while (true)
        {
            // Random zaman araliginda hareket et
            float randomTimer = Random.Range(0f, timer);
            yield return new WaitForSeconds(randomTimer);
            isMove = !isMove;
        }
    }
}
