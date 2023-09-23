using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform tilesParent;
    [SerializeField] private GameObject terrainTile;
    [SerializeField] private GameObject blabla;
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
        if(centerDistZ <= -3.5)
        {
            direction.y = -1;
        }

        if(direction != Vector2.zero)
        {
            //Debug.Log("TA GERANDO :" + direction);
            GenerateTiles(currentTile, direction);
        }
    }

    void GenerateTiles(GameObject currentTile, Vector2 direction)
    {
        float currentTilePosX = currentTile.GetComponent<Transform>().position.x;
        float currentTilePosZ = currentTile.GetComponent<Transform>().position.z;
        float halfScale = scale / 2;
        Vector2 currentTileCoord = currentTile.GetComponent<PerlinNoise>().tileCoord;

        if(direction.x != 0 && direction.y != 0)
        {
            //Diagonal
            //GameObject bb = Instantiate(blabla, new Vector3(currentTilePosX + direction.x * halfScale, 0, currentTilePosZ + direction.y * halfScale), new Quaternion(0,0,0,1));
            //bb.GetComponent<Renderer>().material.mainTexture = Texture2D.redTexture;

            Collider[] hitColliders = Physics.OverlapSphere(new Vector3(currentTilePosX + direction.x * halfScale, 0, currentTilePosZ + direction.y * halfScale), 5f, mask);
            if(hitColliders != null)
            {
                foreach(var hitCollider in hitColliders)
                {
                    Debug.Log(hitCollider);
                }
                //GameObject Tile = Instantiate(terrainTile, new Vector3(currentTilePosX + direction.x * halfScale, 0, currentTilePosZ + direction.y * halfScale), new Quaternion(0,0,0,1), tilesParent);
                //Tile.GetComponent<PerlinNoise>().GenerateTerrain(offSetX, offSetZ, scale, new Vector2(currentTileCoord.x + direction.x, currentTileCoord.y + direction.y));
            }

            //X axis
            //GameObject cc = Instantiate(blabla, new Vector3(currentTilePosX + direction.x * halfScale, 0, currentTilePosZ), new Quaternion(0,0,0,1));
            //cc.GetComponent<Renderer>().material.mainTexture = Texture2D.blackTexture;

            hitColliders = Physics.OverlapSphere(new Vector3(currentTilePosX + direction.x * halfScale, 0, currentTilePosZ), 5f, mask);
            if(hitColliders != null)
            {
                foreach(var hitCollider in hitColliders)
                {
                    Debug.Log(hitCollider);
                }
                //GameObject Tile = Instantiate(terrainTile, new Vector3(currentTilePosX + direction.x * halfScale, 0, currentTilePosZ), new Quaternion(0,0,0,1), tilesParent);
                //Tile.GetComponent<PerlinNoise>().GenerateTerrain(offSetX, offSetZ, scale, new Vector2(currentTileCoord.x + direction.x, currentTileCoord.y));
            }

            //Z axis
            //GameObject dd = Instantiate(blabla, new Vector3(currentTilePosX, 0, currentTilePosZ + direction.y * halfScale), new Quaternion(0,0,0,1));
            //dd.GetComponent<Renderer>().material.mainTexture = Texture2D.whiteTexture;

            hitColliders = Physics.OverlapSphere(new Vector3(currentTilePosX, 0, currentTilePosZ + direction.y * halfScale), 5f, mask);
            if(hitColliders != null)
            {
                foreach(var hitCollider in hitColliders)
                {
                    Debug.Log(hitCollider);
                }
                //GameObject Tile = Instantiate(terrainTile, new Vector3(currentTilePosX, 0, currentTilePosZ + direction.y * halfScale), new Quaternion(0,0,0,1), tilesParent);
                //Tile.GetComponent<PerlinNoise>().GenerateTerrain(offSetX, offSetZ, scale, new Vector2(currentTileCoord.x, currentTileCoord.y + direction.y));
            }
        }
        else //if(direction != Vector2.zero)
        {
            GameObject bb = Instantiate(blabla, new Vector3(currentTilePosX + direction.x * halfScale, 0, currentTilePosZ + direction.y * halfScale), new Quaternion(0,0,0,1));
            bb.GetComponent<Renderer>().material.mainTexture = Texture2D.grayTexture;
            Destroy(bb, 0.5f);
            //X or Z axis
            Collider[] hitColliders = Physics.OverlapSphere(new Vector3(currentTilePosX + direction.x * halfScale, 0, currentTilePosZ + direction.y * halfScale), 1f, mask);
            if(hitColliders != null)
            {
                foreach(var hitCollider in hitColliders)
                {
                    Debug.Log("é isso aqui: " + hitCollider.gameObject);
                }
                //GameObject Tile = Instantiate(terrainTile, new Vector3(currentTilePosX + direction.x * halfScale, 0, currentTilePosZ + direction.y * halfScale), new Quaternion(0,0,0,1), tilesParent);
                //Tile.GetComponent<PerlinNoise>().GenerateTerrain(offSetX, offSetZ, scale, new Vector2(currentTileCoord.x + direction.x, currentTileCoord.y + direction.y));
            }
        }
    }
}
