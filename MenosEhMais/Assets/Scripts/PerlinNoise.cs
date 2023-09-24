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
    public int depth = 20;
    public float offsetX = 100f;
    public float offsetZ = 100f;
    public Vector2 tileCoord;

    private Renderer rend;

    public void StartScript(float offsetX, float offsetZ, float scale, Vector2 tilePos)
    {
        Terrain terrain = GetComponent<Terrain>();
        rend = GetComponent<Renderer>();   

        tileCoord = tilePos;
        this.offsetX = offsetX + tileCoord.x * scale;
        this.offsetZ = offsetZ + tileCoord.y * scale;

        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        //rend.material.mainTexture = GenerateTexture();
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
        if(x == 0 && y == 0)
        {
            Debug.Log("Ponto 0,0 é " + xCoord + " " + yCoord);
        }
        else if(x == 255 && y == 255)
        {
            Debug.Log("Ponto 255,255 é " + xCoord + " " + yCoord);
        }

        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        return new Color(sample, sample, sample);
    }

    TerrainData GenerateTerrain (TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    float CalculateHeight (int x, int y)
    {
        float xCoord = (float)x / (width-1) * scale + offsetX;
        float yCoord = (float)y / (height-1) * scale + offsetZ;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}

/*using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public int width = 256; //x-axis of the terrain
    public int height = 256; //z-axis

    public int depth = 20; //y-axis

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    private void Start()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }

    private void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }
}*/
