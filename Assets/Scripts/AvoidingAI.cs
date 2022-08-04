using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidingAI : MonoBehaviour
{
    [SerializeField] private float targetVelocity=10f;
    [SerializeField] private int numberOfRays = 17;
    [SerializeField] private float angle = 90;
    [SerializeField] private float rayRange = 2;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    private void FixedUpdate()
    {        
        Move();        
    }
    private void Move()
    {        
        var deltaPosition = Vector3.zero;
        for (int i = 0; i < numberOfRays; i++)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis((i / ((float)numberOfRays - 1)) * angle * 2 - angle, this.transform.up);
            var direction = rotation * rotationMod * Vector3.forward;

            var ray = new Ray(this.transform.position, direction);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, rayRange))
            {
                deltaPosition -= (1.0f / numberOfRays) * targetVelocity * direction;
            }
            else
            {
                deltaPosition += (1.0f / numberOfRays) * targetVelocity * direction;
            }
            this.transform.position += deltaPosition * Time.fixedDeltaTime;
        }
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis((i / ((float)numberOfRays-1)) * angle * 2 - angle, this.transform.up);
            var direction = rotation * rotationMod * Vector3.forward;
            Gizmos.DrawRay(this.transform.position, direction);
        }
    }
}
