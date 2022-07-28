using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float turnSpeed = 100f;    // Obejelerin d�nme hizi
    [SerializeField] bool turnDirection;  // Obejelerin d�nme y�n�
    void Start()
    {

    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (turnDirection == false)
        {   // Eger d�nme y�n� false ise saga d�n
            transform.Rotate(0f, turnSpeed * Time.fixedDeltaTime, 0f);
        }
        else
        {   // Eger d�nme y�n� false ise sola d�n
            transform.Rotate(0f, -turnSpeed * Time.fixedDeltaTime, 0f);
        }
    }
    
}
