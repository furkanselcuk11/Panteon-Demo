using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float turnSpeed = 100f;    // Obejelerin dönme hizi
    [SerializeField] bool turnDirection;  // Obejelerin dönme yönü
    void Start()
    {

    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (turnDirection == false)
        {   // Eger dönme yönü false ise saga dön
            transform.Rotate(0f, turnSpeed * Time.fixedDeltaTime, 0f);
        }
        else
        {   // Eger dönme yönü false ise sola dön
            transform.Rotate(0f, -turnSpeed * Time.fixedDeltaTime, 0f);
        }
    }
    
}
