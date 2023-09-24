using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [Header("Texture")]
    private int width = 256;
    private int height = 256; 

    [Header("Perlin Noise")]
    public float scale;
    //public float terrainScale;
    public float offsetX;
    public float offsetZ;
    public Vector2 tileCoord;
    private Renderer rend;

    public void StartScript(float offsetX, float offsetZ, float scale, Vector2 tilePos)
    {
        rend = GetComponent<Renderer>();   

        tileCoord = tilePos;
        this.scale = scale;
        this.offsetX = offsetX + tileCoord.x * scale;
        this.offsetZ = offsetZ + tileCoord.y * scale;
        // this.offsetX = offsetX - tileCoord.x;
        // this.offsetZ = offsetZ - tileCoord.y;

        rend.material.mainTexture = GenerateTexture();
    }

    // void Update()
    // {
    //     rend.material.mainTexture = GenerateTexture();
    // }
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
        // float xCoord = (float)x / (width-1) + offsetX;
        // float yCoord = (float)y / (height-1) + offsetZ;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        Color cor;
        if(x == 0 && y <30)
        {
            if(y == 0) Debug.Log("Ponto 0,0 é " + xCoord + " " + yCoord);
            cor = new Color(sample, 0, 0);
        }
        else if(x == (height-1) && y <30)
        {
            if(y == 0) Debug.Log("Ponto " + (height-1) + ",0 é " + xCoord + " " + yCoord);
            cor = new Color(0, sample, 0);
        }
        else
        {
            cor = new Color(sample, sample, sample);
        }
        return cor;
    }
}
