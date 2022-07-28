using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] float turnSpeed = 100f;    // Obejelerin dönme hizi
    public bool turnDirection;  // Obejelerin dönme yönü
    void Start()
    {
        
    }    
    void Update()
    {
               
    }
    private void FixedUpdate()
    {
        if (turnDirection == false)
        {   // Eger dönme yönü false ise sola dön
            transform.Rotate(0f, 0f, turnSpeed * Time.fixedDeltaTime);            
        }
        else
        {   // Eger dönme yönü false ise saga dön
            transform.Rotate(0f, 0f, -turnSpeed * Time.fixedDeltaTime);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        // Üzerinde bulunan karakterler donme hizindan etkilenir ve tersi hareket eder
        if (collision.gameObject.CompareTag("Player"))
        {
            //float moveX = collision.gameObject.transform.position.x; // Üzerinde bulunan karakterin x pozisyonunu alýr                 
            //moveX = Mathf.Clamp(moveX - 1 * (turnSpeed / 10) * Time.deltaTime, -3.5f, 3.5f);
            //collision.gameObject.transform.position = new Vector3(moveX, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);            
        }
    }    
}
