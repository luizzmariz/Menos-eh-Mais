using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform tilesParent;
    [SerializeField] private GameObject terrainTile;
    [SerializeField] LayerMask mask;
    [SerializeField] private float offSetX;
    [SerializeField] private float offSetZ;
    [SerializeField] private float scale;

    void Start()
    {
        offSetX = Random.Range(0f, 99999f);
        offSetZ = Random.Range(0f, 99999f);

        player = GameObject.Find("Player").transform;

        GameObject initialTile = Instantiate(terrainTile, Vector3.zero, new Quaternion(0,0,0,1), tilesParent);
        initialTile.GetComponent<PerlinNoise>().GenerateTerrain(offSetX, offSetZ, scale, Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(player.position, Vector3.down);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, 5f, mask))
        {
            //Vector2 currentTileCoord = hitInfo.collider.GetComponent<PerlinNoise>().tileCoord;
            CheckPosition(hitInfo.collider.gameObject);
        } 
        
    }

    void CheckPosition(GameObject currentTile)
    {
        float centerDistX = player.position.x - currentTile.GetComponent<Transform>().position.x;
        float centerDistZ = player.position.z - currentTile.GetComponent<Transform>().position.z;
        
        Vector2 direction = Vector2.zero;

        if(centerDistX >= 3.5)
        {
            direction.x = 1;
        }
        if(centerDistX <= -3.5)
        {
            direction.x = -1;
        }
        if(centerDistZ >= 3.5)
        {
            direction.y = 1;
        }
        if(centerDistZ >= 3.5)
        {
            direction.y = -1;
        }
        //GenerateTiles();
    }

    void GenerateTiles(Vector2 baseTileCoord, Vector2 direction)
    {
        GameObject initialTile = Instantiate(terrainTile, Vector3.zero, new Quaternion(0,0,0,1), tilesParent);
    }
}
