using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise2 : MonoBehaviour
{
    [Header("Terrain")]
    public float depth;
    public float terrainSize;
    private Terrain terrain;
    //private terrainData terrainData
    private int width = 256;
    private int height = 256; 
    
    [Header("Perlin Noise")]
    public float scale;
    public float offsetX;
    public float offsetZ;
    public Vector2 tileCoord;

    public void StartScript(float offsetX, float offsetZ, float terrainSize, float scale, int depth, Vector2 tilePos)
    {
        terrain = this.GetComponent<Terrain>();

        tileCoord = tilePos;
        this.scale = scale;
        this.terrainSize = terrainSize;
        this.depth = depth;

        // this.offsetX = offsetX - tileCoord.x * scale;
        // this.offsetZ = offsetZ - tileCoord.y * scale;

        // this.offsetX = offsetX - tileCoord.x;
        // this.offsetZ = offsetZ - tileCoord.y;

        this.offsetX = offsetX - tileCoord.x * terrainSize;
        this.offsetZ = offsetZ - tileCoord.y * terrainSize;
        
        terrain.terrainData = new TerrainData(); 
        // terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    void Update()
    {
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain (TerrainData terrainData)
    {
        
        //terrainData.heightmapResolution = width + 1;
        terrainData.heightmapResolution = width;
        terrainData.size = new Vector3(terrainSize, depth, terrainSize);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width+1, height+1];
        for(int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    float CalculateHeight (int x, int y)
    {
        // float xCoord = (float)x / (width-1) * scale + offsetX;
        // float yCoord = (float)y / (height-1) * scale + offsetZ;
        float xCoord = (float)x / (width-1) + offsetX;
        float yCoord = (float)y / (height-1) + offsetZ;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
