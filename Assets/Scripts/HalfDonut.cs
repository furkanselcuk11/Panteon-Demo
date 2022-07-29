using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonut : MonoBehaviour
{   

    [SerializeField] private float speed = 5f;
    [SerializeField] private float timer=5f;
    [SerializeField] private bool isMove;

    private Vector3 startpos1;
    private Vector3 startpos2;
    void Start()
    {
        startpos1 = transform.localPosition;
        startpos2 = new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        isMove = true;
        StartCoroutine(nameof(MoveDonut));
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (isMove)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startpos1, Time.fixedDeltaTime * speed);
            if (transform.localPosition == startpos1)
            {
                startpos1 = startpos2;
                if (startpos1 == startpos2)
                {
                    startpos2 = transform.localPosition;
                }
            }

        }
        else
        {
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
