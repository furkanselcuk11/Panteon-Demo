using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painterr : MonoBehaviour
{
    public MeshRenderer meshRenderer; // Boyayacagimiz obje
    public Texture2D brush; // Fýrça texture
    public Vector2Int textureArea; //x:1024, y:1024
    Texture2D texture;
    public Camera cam;
    private Color _color = Color.red;

    void Start()
    {
        texture = new Texture2D(textureArea.x, textureArea.y, TextureFormat.ARGB32, false);
        meshRenderer.material.mainTexture = texture;
    }
        
    void Update()
    {
        if (Input.GetMouseButton(0))// Sol tuþa basýlý tuttukça boyayacak
        {
            RaycastHit hitInfo;
            // cam, kullandýðýmýz kamera(Camera classý)
            if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition),out hitInfo))
            {
                Debug.Log(hitInfo.textureCoord);
                Paint(hitInfo.textureCoord);
            }
        }
    }
    private void Paint(Vector2 coordinate)
    {
        coordinate.x *= texture.width;  // 0-1 egerini tam nokra piksellere cevirdik
        coordinate.y *= texture.height; // Yeni 0-1024 yaptýk
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
    }
}
