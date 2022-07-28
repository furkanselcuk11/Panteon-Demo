using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] float turnSpeed = 100f;    // Obejelerin d�nme hizi
    public bool turnDirection;  // Obejelerin d�nme y�n�
    void Start()
    {
        
    }    
    void Update()
    {
               
    }
    private void FixedUpdate()
    {
        if (turnDirection == false)
        {   // Eger d�nme y�n� false ise sola d�n
            transform.Rotate(0f, 0f, turnSpeed * Time.fixedDeltaTime);            
        }
        else
        {   // Eger d�nme y�n� false ise saga d�n
            transform.Rotate(0f, 0f, -turnSpeed * Time.fixedDeltaTime);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        // �zerinde bulunan karakterler donme hizindan etkilenir ve tersi hareket eder
        if (collision.gameObject.CompareTag("Player"))
        {
            //float moveX = collision.gameObject.transform.position.x; // �zerinde bulunan karakterin x pozisyonunu al�r                 
            //moveX = Mathf.Clamp(moveX - 1 * (turnSpeed / 10) * Time.deltaTime, -3.5f, 3.5f);
            //collision.gameObject.transform.position = new Vector3(moveX, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);            
        }
    }    
}
