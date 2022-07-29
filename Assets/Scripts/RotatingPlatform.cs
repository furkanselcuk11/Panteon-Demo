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
}
