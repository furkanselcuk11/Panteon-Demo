using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{
    public MeshRenderer meshRenderer; // Boyayacagimiz obje
    public Texture2D brush; // Fýrça texture
    public Vector2Int textureArea; //x:1024, y:1024
    Texture2D texture;
    public Camera cam;
    private bool isPainted;
    
    [SerializeField] private int max;
    [SerializeField] private Image fillAmountMask;

    void Start()
    {
        texture = new Texture2D(textureArea.x, textureArea.y, TextureFormat.ARGB32, false);
        meshRenderer.material.mainTexture = texture;
        max = (textureArea.x * textureArea.y)-2576;
        isPainted = false;
    }
        
    void Update()
    {
        if (GameManager.gamemanagerInstance.isFinish)
        {
            meshRenderer.gameObject.SetActive(true);
        }
        if (Input.GetMouseButton(0) && GameManager.gamemanagerInstance.isFinish &&!isPainted)// Sol tuþa basýlý tuttukça boyayacak
        {            
            RaycastHit hitInfo;
            // cam, kullandýðýmýz kamera(Camera classý)
            if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition),out hitInfo))
            {
                Paint(hitInfo.textureCoord);
            }
        }
    }
    private void Paint(Vector2 coordinate)
    {
        coordinate.x *= texture.width;  // 0-1 degerini tam nokta piksellere cevirdik
        coordinate.y *= texture.height; // Yani 0-1024 yaptýk
        Color32[] textureC32 = texture.GetPixels32();
        Color32[] brushC32 = brush.GetPixels32();        

        // Fýrçacmým ortasýný kordinatlarý
        Vector2Int halfBrush = new Vector2Int(brush.width / 2, brush.height / 2);

        for (int x = 0; x < brush.width; x++)
        {
            int xPos = x - halfBrush.x + (int)coordinate.x;
            if (xPos < 0 || xPos >= texture.width)
                continue;
            for (int y = 0; y < brush.height; y++)
            {
                int yPos = y - halfBrush.y + (int)coordinate.y;
                if (yPos < 0 || yPos >= texture.height)
                    continue;
                if (brushC32[x+(y*brush.width)].a > 0f)
                {
                    int tPos =
                        xPos +// X (U) pozisyonu
                        (texture.width * yPos);// Y (V) pozisyonu
                    if (brushC32[x + (y * brush.width)].r > textureC32[tPos].r)
                        textureC32[tPos] = brushC32[x + (y * brush.width)];                   
                }
            }
        }
        texture.SetPixels32(textureC32);        
        texture.Apply(); // Deðiþiklikleri uygulamasý için

        int painted = 0;
        foreach (var item in textureC32)
        {
            if (item.g == 0)
            {
                painted++;                
            }
        }
        GetCurrentFiil(painted);
    }    
    void GetCurrentFiil(int painted)
    {
        float fillAmount = (float)painted / (float)max;
        fillAmountMask.fillAmount = fillAmount;
        if (fillAmountMask.fillAmount == 1f)
        {
            paintedFinish();
        }
    }
    void paintedFinish()
    {
        isPainted = true;
        Debug.Log("Boyandý");
        AudioController.audioControllerInstance.Play("FinishSound");
        UIController.uicontrollerInstance.paintSuccessful.SetActive(true);
        UIController.uicontrollerInstance.nextLevel.gameObject.SetActive(true);
    }
}
