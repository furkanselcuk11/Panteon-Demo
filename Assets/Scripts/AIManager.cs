using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager aimanagerInstance;
    Rigidbody rb;
    private Vector3 startPosition;
    
    public bool isMove;
    public bool isFinish;


    private void Awake()
    {
        if (aimanagerInstance == null)
        {
            aimanagerInstance = this;
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = this.transform.position;
        isMove = false;
        isFinish = false;
    }
    
    void Update()
    {
        
    }
    IEnumerator RestartPosition()
    {
        yield return new WaitForSeconds(0.5f);
        this.transform.position = this.startPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            // Eğer AI Finish cizgisini gecmisse AI hareket etmez
            this.GetComponent<AvoidingObstacles>().StartCoroutine("EnemyStop");
        }
        if (other.gameObject.CompareTag("Fail"))
        {
            // Eğer AI dusmus ve Fail temas etmisse tekrar oyna
            StartCoroutine(nameof(RestartPosition));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("StaticObstacle"))
        {
            // Eger StaticObstacle objesine temas etmisse tekrar oyna
            StartCoroutine(nameof(RestartPosition));
        }
        if (collision.gameObject.CompareTag("HorizontalObstacle"))
        {
            // Eger HorizontalObstacle objesine temas etmisse tekrar oyna
            StartCoroutine(nameof(RestartPosition));
        }
        if (collision.gameObject.CompareTag("MovingStick"))
        {
            // Eger MovingStick objesine temas etmisse tekrar oyna
            StartCoroutine(nameof(RestartPosition));
        }
        if (collision.gameObject.CompareTag("RotatingStick"))
        {
            // Eger RotatingStick objesine temas etmisse cubuk kuvvet uygular
            rb.AddForce(collision.gameObject.transform.right * 300f * Time.fixedDeltaTime, ForceMode.Impulse);
            StartCoroutine(nameof(RestartPosition));
        }
    }
    
}
