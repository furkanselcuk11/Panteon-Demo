using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target; // Takip edilecek obje
    [SerializeField] private Vector3 _offset;   // Kamera ile takip edilecek obje arasýndaki mesafe
    [SerializeField] private float _chaseSpeed = 5; // Takip etme hýzý

    private void LateUpdate()
    {
        if (GameManager.gamemanagerInstance.isFinish)
        {
            Vector3 desPos = new Vector3(0f, _target.position.y, _target.position.z) + _offset;  // Kamera ile takip edilen obje arasýndaki mesafe
            transform.position = Vector3.Lerp(transform.position, desPos, _chaseSpeed);   // Kamera pozisyonu yumuþak geçiþ ile aradaki mesafe kadar uzaktan takip eder
        }
        else
        {
            Vector3 desPos = _target.position + _offset;  // Kamera ile takip edilen obje arasýndaki mesafe
            transform.position = Vector3.Lerp(transform.position, desPos, _chaseSpeed);   // Kamera pozisyonu yumuþak geçiþ ile aradaki mesafe kadar uzaktan takip eder
        }        
    }
}