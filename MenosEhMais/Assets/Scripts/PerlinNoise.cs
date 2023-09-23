using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [Header("Texture")]
    private int width = 256;
    private int height = 256; 

    [Header("Perlin Noise")]
    private float scale = 20f;
    private float offsetX = 100f;
    private float offsetZ = 100f;
    public Vector2 tileCoord;

    private Renderer rend;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GenerateTerrain(float offsetX, float offsetZ, float scale, Vector2 tilePos)
    {
        rend = GetComponent<Renderer>();   

        tileCoord = tilePos;
        this.offsetX = offsetX + tileCoord.x * scale;
        this.offsetZ = offsetZ + tileCoord.y * scale;
        
        rend.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / (width-1) * scale + offsetX;
        float yCoord = (float)y / (height-1) * scale + offsetZ;
        /*if(x == 0 && y == 0)
        {
            Debug.Log("Ponto 0,0 é " + xCoord + " " + yCoord);
        }
        else if(x == 255 && y == 255)
        {
            Debug.Log("Ponto 255,255 é " + xCoord + " " + yCoord);
        }*/

        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        return new Color(sample, sample, sample);
    }
}
