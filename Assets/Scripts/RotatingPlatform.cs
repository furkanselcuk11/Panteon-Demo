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
}
