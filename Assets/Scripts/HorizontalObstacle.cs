using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacle : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Vector3 startpos1;
    private Vector3 startpos2;
    void Start()
    {
        startpos1 = transform.position; // HorizontalObstacle objesinin ilk konumu
        startpos2 = new Vector3(-transform.position.x, transform.position.y, transform.position.z); // HorizontalObstacle objesinin ilk konumunun tersi
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        // HorizontalObstacle objesi startpos1 ve startpos2 pozisyonlari arasinda hareket eder
        transform.position = Vector3.MoveTowards(transform.position, startpos1, Time.fixedDeltaTime * speed);   // Speed degerinde pozisyonu degisir
        if (transform.position == startpos1)
        {
            // Pozisyon her degistiginde startpos1 ve startpos1 degerleri yer degistirir dongu halinde devam etmesi icin
            startpos1 = startpos2;
            if (startpos1 == startpos2)
            {
                startpos2 = transform.position;
            }
        }
    }
    
}
