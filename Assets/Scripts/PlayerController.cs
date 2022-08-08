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
    [SerializeField] private ParticleSystem konfetiFX;  // Finih alani konfeti efekti
    private bool isRotatingPlatform;    // RotatingPlatform uzerinde karakter varmı
    private bool isRight;   //RotatingPlatform donus yonu
    public bool isMove; // Hareker ediyor mu

    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isRotatingPlatform = false;
    }
    private void FixedUpdate()
    {
        if (GameManager.gamemanagerInstance.startGame & !GameManager.gamemanagerInstance.isFinish && isMove)
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

        if (isRotatingPlatform)
        {
            // RotatingPlatform donus yonune karaktere zıt kuvvet uygular
            RotatingPlatformMove(isRight);            
        }
    }
    void MoveInput()
    {
        #region Mobile Controller 4 Direction

        float moveX = transform.position.x; // Player objesinin x pozisyonun de?erini al?r      
        float moveZ = transform.position.z; // Player objesinin z pozisyonun de?erini al?r           

        if (Input.GetKey(KeyCode.LeftArrow) || MobileInput.instance.swipeLeft)
        {   // Eðer klavyede sol ok tuþuna basýldýysa yada "MobileInput" scriptinin swipeLeft deðeri True ise  Sola hareket gider
            moveX = Mathf.Clamp(moveX - 1 * horizontalspeed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);    // Pozisyon sýnýrlandýrýlmasý koyulacaksa defaultSwipe eklenir
            // Player objesinin x (sol) pozisyonundaki gideceði min-max sýnýrý belirler
        }
        else if (Input.GetKey(KeyCode.RightArrow) || MobileInput.instance.swipeRight)
        {   // Eðer klavyede sað ok tuþuna basýldýysa yada "MobileInput" scriptinin swipeRight deðeri True ise Saða hareket gider   
            moveX = Mathf.Clamp(moveX + 1 * horizontalspeed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);    // Pozisyon sýnýrlandýrýlmasý koyulacaksa defaultSwipe eklenir
            // Player objesinin x (sað) pozisyonundaki gideceði min-max sýnýrý belirler
        }
        transform.position = new Vector3(moveX, transform.position.y, moveZ);
        // Player objesinin pozisyonu moveX deðerine göre x ekseninde, moveZ deðerine göre z ekseninde hareket eder ve y ekseninde sabit kalýr  

        #endregion
    }
    void RotatingPlatformMove(bool value)
    {
        float moveX = transform.position.x; // Player objesinin x pozisyonun de?erini al?r      
        float moveZ = transform.position.z; // Player objesinin z pozisyonun de?erini al?r  
        if (!value)
        {
            moveX = Mathf.Clamp(moveX - 1 * (horizontalspeed/2) * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);    
        }
        else
        {
            moveX = Mathf.Clamp(moveX + 1 * (horizontalspeed / 2) * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);    
        }
        transform.position = new Vector3(moveX, transform.position.y, moveZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            // Eğer karakter Finish cizgisini gecmisse karakter hareket etmez 
            StartCoroutine(nameof(PlayerStop));
            konfetiFX.transform.position = this.transform.position;
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        if (other.gameObject.CompareTag("Fail"))
        {
            // Eğer karakter düşmğş ve Fail temas etmisse tekrar oyna
            StartCoroutine(nameof(RestartPosition));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("StaticObstacle"))
        {
            // Eger StaticObstacle objesine temas etmisse tekrar oyna
            StartCoroutine(nameof(RestartPosition));
        }
        if (collision.gameObject.CompareTag("HorizontalObstacle"))
        {
            // Eger HorizontalObstacle objesine temas etmisse tekrar oyna
            StartCoroutine(nameof(RestartPosition));
        }
        if (collision.gameObject.CompareTag("MovingStick"))
        {
            // Eger MovingStick objesine temas etmisse tekrar oyna
            StartCoroutine(nameof(RestartPosition));
        }
        if (collision.gameObject.CompareTag("RotatingStick"))
        {
            // Eger RotatingStick objesine temas etmisse cubuk kuvvet uygular
            rb.AddForce(collision.gameObject.transform.right * 300f * Time.fixedDeltaTime, ForceMode.Impulse);
            StartCoroutine(nameof(RestartPosition));
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("RotatingPlatform"))
        {
            // RotatingPlatform objesine temas etmisse
            isRotatingPlatform = true;
            isRight = collision.gameObject.GetComponent<RotatingPlatform>().turnDirection;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("RotatingPlatform"))
        {
            // RotatingPlatform objesinden çıkmışsa
            isRotatingPlatform = false;
        }
    }
    IEnumerator RestartPosition()
    {
        AudioController.audioControllerInstance.Play("DeathSound");
        isMove = false;
        yield return new WaitForSeconds(0.5f);
        transform.position = Vector3.zero;
    }
    IEnumerator PlayerStop()
    {
        konfetiFX.Play();   // Finish cizgisi gecildiginde Konfeti patlar
        AudioController.audioControllerInstance.Play("FinishSound");
        yield return new WaitForSeconds(0.3f);   // 0.3 sure sonra calis     
        anim.SetTrigger("Victory");   // Victory animasyonu çalışır
        isMove = false; // hareket etme false olur
        GameManager.gamemanagerInstance.isFinish = true; // isFinish aktif olur
        UIController.uicontrollerInstance.finishRankText.text = FindObjectOfType<Ranking>().indexNo.ToString(); // Yarisdaki siralamasi yazilir
        yield return new WaitForSeconds(0.1f);  // 0.1 sure sonra calis 
        anim.SetBool("Running", false);   // Koþma animasyonu durur ve default olarak bekleme animsayonu çalýþýr
        GetComponent<PlayerController>().enabled = false;
        GetComponent<MobileInput>().enabled = false;
    }
}