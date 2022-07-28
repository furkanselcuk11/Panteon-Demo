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
        startpos1 = transform.position;
        startpos2 = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, startpos1, Time.fixedDeltaTime * speed);
        if (transform.position == startpos1)
        {
            startpos1 = startpos2;
            if (startpos1 == startpos2)
            {
                startpos2 = transform.position;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Temas eden karakterler oyuna tekrar baslar
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Temas eden karakterler oyuna tekrar baslar
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Tekrar başla");
        }
    }
}
