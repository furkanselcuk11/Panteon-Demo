using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    [Header("Player Controller")]
    [SerializeField] private float speed = 5f;    // Player hareket hizi
    [SerializeField] private float horizontalspeed = 5f; // Player yön hareket hizi
    [SerializeField] private float defaultSwipe = 4f;    // Player default kaydirma mesafesi

    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (GameManager.gamemanagerInstance.startGame & !GameManager.gamemanagerInstance.isFinish)
        {
            // Eðer StartGame true ve isFinish false ise hareket et
            transform.Translate(0, 0, speed * Time.fixedDeltaTime); // Karakter speed deðeri hýzýdna ileri hareket eder
            anim.SetBool("Running", true);    // Koþma animasyonu çalýþýr
            MoveInput();    // Player hareket kontrolü çalýþtýr
        }
        else
        {
            // Eðer StartGame False ise  hareket etmez
            anim.SetBool("Running", false);   // Koþma animasyonu durur ve default olarak bekleme animsayonu çalýþýr
        }
    }
    void MoveInput()
    {
        #region Mobile Controller 4 Direction

        float moveX = transform.position.x; // Player objesinin x pozisyonun de?erini al?r      
        float moveZ = transform.position.z; // Player objesinin z pozisyonun de?erini al?r           

        if (Input.GetKey(KeyCode.LeftArrow) || MobileInput.instance.swipeLeft)
        {   // Eðer klavyede sol ok tuþuna basýldýysa yada "MobileInput" scriptinin swipeLeft deðeri True ise  Sola hareket gider
            moveX = Mathf.Clamp(moveX - 1 * horizontalspeed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);    // Pozisyon sýnýrlandýrýlmasý koyulacaksa
            // Player objesinin x (sol) pozisyonundaki gideceði min-max sýnýrý belirler
        }
        else if (Input.GetKey(KeyCode.RightArrow) || MobileInput.instance.swipeRight)
        {   // Eðer klavyede sað ok tuþuna basýldýysa yada "MobileInput" scriptinin swipeRight deðeri True ise Saða hareket gider   
            moveX = Mathf.Clamp(moveX + 1 * horizontalspeed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);    // Pozisyon sýnýrlandýrýlmasý koyulacaksa
            // Player objesinin x (sað) pozisyonundaki gideceði min-max sýnýrý belirler
        }
        else
        {
            rb.velocity = Vector3.zero; // E?er hareket edilmediyse Player objesi sabit kals?n
        }

        transform.position = new Vector3(moveX, transform.position.y, moveZ);
        // Player objesinin pozisyonu moveX deðerine göre x ekseninde, moveZ deðerine göre z ekseninde hareket eder ve y ekseninde sabit kalýr  

        #endregion
    }
}