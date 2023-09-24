// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TerrainManager2 : MonoBehaviour
// {
//     [Header("Terrain")]
//     [SerializeField] private Transform player;
//     [SerializeField] private Transform tilesParent;
//     [SerializeField] private GameObject terrainTile;
//     [SerializeField] LayerMask mask;
//     [SerializeField] private float offSetX;
//     [SerializeField] private float offSetZ;
//     [SerializeField] private float perlinScale;
//     [SerializeField] private int depth;
//     private float terrainProportion = 20;

//     [SerializeField] private GameObject blabla;


//     void Start()
//     {
//         //offSetX = Random.Range(0f, 99999f);
//         //offSetZ = Random.Range(0f, 99999f);
//         offSetX = 100;
//         offSetZ = 100;

//         player = GameObject.Find("Player").transform;

//         GameObject initialTile = Instantiate(terrainTile, new Vector3(-5f, 0f, -5f), new Quaternion(0,0,0,1), tilesParent);
//         //initialTile.transform.GetChild(0).GetComponent<PerlinNoise>().StartScript(offSetX, offSetZ, perlinScale, depth, Vector2.zero);
//         initialTile.GetComponent<PerlinNoise>().StartScript(offSetX, offSetZ, perlinScale, depth, Vector2.zero);
//     }

//     void Update()
//     {
//         Ray ray = new Ray(player.position, Vector3.down);
//         RaycastHit hitInfo;

//         if(Physics.Raycast(ray, out hitInfo, 5f, mask))
//         {
//             //CheckPosition(hitInfo.collider.gameObject.GetComponent<Transform>().parent.gameObject);
//             CheckPosition(hitInfo.collider.gameObject);
//         } 
//     }

//     void CheckPosition(GameObject currentTile)
//     {
//         //float centerDistX = player.position.x - currentTile.GetComponent<Transform>().position.x;
//         //float centerDistZ = player.position.z - currentTile.GetComponent<Transform>().position.z;
//         float centerDistX = player.position.x - currentTile.GetComponent<Transform>().position.x -5;
//         float centerDistZ = player.position.z - currentTile.GetComponent<Transform>().position.z -5;
        
//         Vector2 direction = Vector2.zero;

//         if(centerDistX >= 3.5)
//         {
//             direction.x = 1;
//         }
//         if(centerDistX <= -3.5)
//         {
//             direction.x = -1;
//         }
//         if(centerDistZ >= 3.5)
//         {
//             direction.y = 1;
//         }
//         if(centerDistZ <= -3.5)
//         {
//             direction.y = -1;
//         }

//         if(direction != Vector2.zero)
//         {
//             //Debug.Log("o direction é " + direction);
//             GenerateTiles(currentTile, direction);
//         }
//     }

//     void GenerateTiles(GameObject currentTile, Vector2 direction)
//     {
//         float currentTilePosX = currentTile.GetComponent<Transform>().position.x -5;
//         float currentTilePosZ = currentTile.GetComponent<Transform>().position.z -5;
//         float halfScale = terrainProportion / 2;
//         //Vector2 currentTileCoord = currentTile.transform.GetChild(0).GetComponent<PerlinNoise>().tileCoord;
//         Vector2 currentTileCoord = currentTile.GetComponent<PerlinNoise>().tileCoord;

//         if(direction.x != 0 && direction.y != 0)
//         {
//             /*GameObject aa = Instantiate(blabla, new Vector3(currentTilePosX + direction.x * halfScale +10, 0, currentTilePosZ + direction.y * halfScale +10), new Quaternion(0,0,0,1));
//             aa.GetComponent<Renderer>().material.mainTexture = Texture2D.redTexture;
//             Destroy(aa, 0.5f);*/

//             //Diagonal
//             Collider[] hitColliders = Physics.OverlapSphere(new Vector3(currentTilePosX + direction.x * halfScale +10, 0, currentTilePosZ + direction.y * halfScale +10), 3f, mask);
//             if(hitColliders.Length == 0)
//             {
//                 GameObject Tile = Instantiate(terrainTile, new Vector3(currentTilePosX + direction.x * halfScale +5, 0, currentTilePosZ + direction.y * halfScale +5), new Quaternion(0,0,0,1), tilesParent);
//                 //Tile.transform.GetChild(0).GetComponent<PerlinNoise>().StartScript(offSetX, offSetZ, perlinScale, depth, new Vector2(currentTileCoord.x + direction.x, currentTileCoord.y + direction.y));
//             }

//             /*GameObject bb = Instantiate(blabla, new Vector3(currentTilePosX + direction.x * halfScale +10, 0, currentTilePosZ +10), new Quaternion(0,0,0,1));
//             bb.GetComponent<Renderer>().material.mainTexture = Texture2D.blackTexture;
//             Destroy(bb, 0.5f);*/

//             //X axis
//             Collider[] hitColliders2 = Physics.OverlapSphere(new Vector3(currentTilePosX + direction.x * halfScale +10, 0, currentTilePosZ +10), 3f, mask);
//             if(hitColliders2.Length == 0)
//             {
//                 GameObject Tile = Instantiate(terrainTile, new Vector3(currentTilePosX + direction.x * halfScale +5, 0, currentTilePosZ +5), new Quaternion(0,0,0,1), tilesParent);
//                 //Tile.transform.GetChild(0).GetComponent<PerlinNoise>().StartScript(offSetX, offSetZ, perlinScale, depth, new Vector2(currentTileCoord.x + direction.x, currentTileCoord.y));
//             }

//             /*GameObject cc = Instantiate(blabla, new Vector3(currentTilePosX +10, 0, currentTilePosZ + direction.y * halfScale +10), new Quaternion(0,0,0,1));
//             cc.GetComponent<Renderer>().material.mainTexture = Texture2D.whiteTexture;
//             Destroy(cc, 0.5f);*/

//             //Z axis
//             Collider[] hitColliders3 = Physics.OverlapSphere(new Vector3(currentTilePosX +10, 0, currentTilePosZ + direction.y * halfScale +10), 3f, mask);
//             if(hitColliders3.Length == 0)
//             {
//                 GameObject Tile = Instantiate(terrainTile, new Vector3(currentTilePosX +5, 0, currentTilePosZ + direction.y * halfScale +5), new Quaternion(0,0,0,1), tilesParent);
//                 //Tile.transform.GetChild(0).GetComponent<PerlinNoise>().StartScript(offSetX, offSetZ, perlinScale, depth, new Vector2(currentTileCoord.x, currentTileCoord.y + direction.y));
//             }
//         }
//         else
//         {
//             /*GameObject aa = Instantiate(blabla, new Vector3(currentTilePosX + direction.x * halfScale +10, 0, currentTilePosZ + direction.y * halfScale +10), new Quaternion(0,0,0,1));
//             aa.GetComponent<Renderer>().material.mainTexture = Texture2D.redTexture;
//             Destroy(aa, 0.5f);*/

//             //X or Z axis
//             Collider[] hitColliders = Physics.OverlapSphere(new Vector3(currentTilePosX + direction.x * halfScale +10, 0, currentTilePosZ + direction.y * halfScale +10), 3f, mask);
//             if(hitColliders.Length == 0)
//             {
//                 GameObject Tile = Instantiate(terrainTile, new Vector3(currentTilePosX + direction.x * halfScale +5, 0, currentTilePosZ + direction.y * halfScale +5), new Quaternion(0,0,0,1), tilesParent);
//                 //Tile.GetComponent<PerlinNoise>().StartScript(offSetX, offSetZ, perlinScale, depth, new Vector2(currentTileCoord.x + direction.x, currentTileCoord.y + direction.y));
//             }
//         }
//     }
    
// }
