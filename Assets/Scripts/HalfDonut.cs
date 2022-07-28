using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonut : MonoBehaviour
{
    //[SerializeField] private GameObject movingStick;

    [SerializeField] private float speed = 5f;

    private Vector3 startpos1;
    private Vector3 startpos2;
    void Start()
    {
        startpos1 = transform.localPosition;
        startpos2 = new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
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
    private void OnTriggerEnter(Collider other)
    {
        // Temas eden karakterler oyuna tekrar baslar
    }
}
