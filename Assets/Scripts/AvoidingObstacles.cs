using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class AvoidingObstacles : MonoBehaviour
{
    [SerializeField] private float defaultSwipe = 4f;    // Player default kaydirma mesafesi
    [SerializeField] private float speed = 20f;
    [SerializeField] private Vector2 speedBetween;
    bool isLeftHit;
    bool isRightHit;
    bool isBothHit;
    bool isRandomDecided;

    int randomDirectionX;

    private Animator anim;
    private void Start()
    {
        anim = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        speed = Random.Range(speedBetween.x, speedBetween.y);
    }
    private void Update()
    {
        if (AIManager.aimanagerInstance.isMove)
        {
            anim.SetBool("Running", true);    // Koþma animasyonu çalýþýr
        }
        else
        {
            anim.SetBool("Running", false);    // Bekleme animasyonu çalýþýr
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.gamemanagerInstance.startGame)
        {
            All_Raycast();
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -defaultSwipe, defaultSwipe), transform.position.y, transform.position.z);
            AIManager.aimanagerInstance.isMove = true;
        }        
    }

    private void All_Raycast()
    {
        Left_Raycast();
        Right_Raycast();
        Forward_Raycast();
    }

    private void Left_Raycast()
    {
        for (int i = -1; i < 2; i += 2)
        {
            Ray leftRays = new Ray(this.transform.position + (new Vector3(0, 0, (transform.localScale.z / 2))) * i, Ray_Direction(-90, Vector3.up));
            RaycastHit hit;
            if (Physics.Raycast(leftRays, out hit, 1f))
            {
                transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
            }
        }
    }

    private void Right_Raycast()
    {
        for (int i = -1; i < 2; i += 2)
        {
            Ray rightRays = new Ray(this.transform.position + (new Vector3(0, 0, (transform.localScale.z / 2))) * i, Ray_Direction(90, Vector3.up));
            RaycastHit hit;
            if (Physics.Raycast(rightRays, out hit, 1f))
            {
                transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
            }
        }
    }

    private void Forward_Raycast()
    {
        var destination = Vector3.zero;

        Ray leftRay = new Ray(this.transform.position - new Vector3((transform.localScale.x / 2), 0, 0), Ray_Direction(0, Vector3.up));
        Ray rightRay = new Ray(this.transform.position + new Vector3((transform.localScale.x / 2), 0, 0), Ray_Direction(0, Vector3.up));
        RaycastHit hit;

        if (Physics.Raycast(leftRay, out hit, 1f))
        {
            isLeftHit = true;
        }
        else
        {
            isLeftHit = false;
        }

        if (Physics.Raycast(rightRay, out hit, 1f))
        {
            isRightHit = true;
        }
        else
        {
            isRightHit = false;
        }


        if (isLeftHit && isRightHit)
        {
            isBothHit = true;
            isLeftHit = false;
            isRightHit = false;            
        }
        else
        {
            isBothHit = false;
        }

        if (isLeftHit)
        {
            isRandomDecided = false;
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        }
        else if (isRightHit)
        {
            isRandomDecided = false;
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
        }
        else if (isBothHit)
        {
            if (isRandomDecided == false)
            {
                randomDirectionX = Random.Range(0, 2);
                isRandomDecided = true;
            }

            if (randomDirectionX == 0)
            {
                transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
            }

        }
        else
        {
            destination += 10f * Ray_Direction(0, Vector3.up) * Time.deltaTime;
        }

        transform.position += destination * speed * Time.deltaTime;
    }


    //Raycast direction 
    private Vector3 Ray_Direction(float angle, Vector3 direction)
    {
        var rotaitonAxis = Quaternion.AngleAxis(angle, direction);
        var ray_direction = rotaitonAxis * Vector3.forward;

        return ray_direction;
    }
    
}