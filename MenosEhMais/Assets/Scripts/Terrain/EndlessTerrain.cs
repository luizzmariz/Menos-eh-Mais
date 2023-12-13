using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EndlessTerrain : MonoBehaviour {

	const float scale = 2.5f;

	const float viewerMoveThresholdForChunkUpdate = 25f;
	const float sqrViewerMoveThresholdForChunkUpdate = viewerMoveThresholdForChunkUpdate * viewerMoveThresholdForChunkUpdate;

	public LODInfo[] detailLevels;
	public static float maxViewDst;

	public Transform viewer;
	public Material mapMaterial;

	public static Vector2 viewerPosition;
	Vector2 viewerPositionOld;
	static MapGenerator mapGenerator;
	int chunkSize;
	int chunksVisibleInViewDst;

	Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
	static List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

	void Start() {
		mapGenerator = FindObjectOfType<MapGenerator> ();

		maxViewDst = detailLevels [detailLevels.Length - 1].visibleDstThreshold;
		chunkSize = MapGenerator.mapChunkSize - 1;
		chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);

		UpdateVisibleChunks ();
	}

	void Update() {
		viewerPosition = new Vector2 (viewer.position.x, viewer.position.z) / scale;

		if ((viewerPositionOld - viewerPosition).sqrMagnitude > sqrViewerMoveThresholdForChunkUpdate) {
			viewerPositionOld = viewerPosition;
			UpdateVisibleChunks ();
		}
	}
		
	void UpdateVisibleChunks() {

		for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++) {
			terrainChunksVisibleLastUpdate [i].SetVisible (false, false);
		}
		terrainChunksVisibleLastUpdate.Clear ();
			
		int currentChunkCoordX = Mathf.RoundToInt (viewerPosition.x / chunkSize);
		int currentChunkCoordY = Mathf.RoundToInt (viewerPosition.y / chunkSize);

		for (int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++) {
			for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++) {
				Vector2 viewedChunkCoord = new Vector2 (currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

				if (terrainChunkDictionary.ContainsKey (viewedChunkCoord)) {
					terrainChunkDictionary [viewedChunkCoord].UpdateTerrainChunk ();
				} else {
					terrainChunkDictionary.Add (viewedChunkCoord, new TerrainChunk (viewedChunkCoord, chunkSize, detailLevels, transform, mapMaterial));
				}

			}
		}
	}

	public class TerrainChunk {

		GameObject meshObject;
		Vector2 position;
		Vector3 positionV3;
		Bounds bounds;

		MeshRenderer meshRenderer;
		MeshFilter meshFilter;
		MeshCollider meshCollider;

		LODInfo[] detailLevels;
		LODMesh[] lodMeshes;
		LODMesh collisionLODMesh;

		MapData mapData;
		bool mapDataReceived;
		int previousLODIndex = -1;

		Transform parent;
		Material material;

		//[Range(1.35f,1.55f)]
		public float highPointQuote = 1.47f;
		bool hasCave;
		float caveSeed;
		CaveEntrance caveEntrance;
		Dungeon dungeon;

		//Prof. Isaac, se vc chegou até aqui olhando meu código saiba que está é uma mensagem de amor pra ti, ficou faltando uma lista de recursos(gameobjects) recolhidos utilizada para checar se deveria spawnar ou não o item na caverna. Dito isso me empenhei nos ultimos minutos como um bom vagabundo empenhado, foi foda tmj! 

		bool objectIsCreated;

		public TerrainChunk(Vector2 coord, int size, LODInfo[] detailLevels, Transform parent, Material material) {
			this.detailLevels = detailLevels;
			this.parent = parent;
			this.material = material;

			hasCave = false;

			position = coord * size;
			bounds = new Bounds(position,Vector2.one * size);
			positionV3 = new Vector3(position.x,0,position.y);

			objectIsCreated = false;

			lodMeshes = new LODMesh[detailLevels.Length];
			for (int i = 0; i < detailLevels.Length; i++) {
				lodMeshes[i] = new LODMesh(detailLevels[i].lod, UpdateTerrainChunk);
				if (detailLevels[i].useForCollider) {
					collisionLODMesh = lodMeshes[i];
				}
			}

			mapGenerator.RequestMapData(position,OnMapDataReceived);
		}

		void OnMapDataReceived(MapData mapData) {
			this.mapData = mapData;
			mapDataReceived = true;

			int width = mapData.heightMap.GetLength (0);
			int height = mapData.heightMap.GetLength (1);

			float seedGenerator = 0;

			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					seedGenerator += mapData.heightMap[x,y];
					if(mapData.heightMap[x,y] > highPointQuote && !hasCave)
					{
						hasCave = true;
						//caveSeed = mapData.heightMap[x,y];
					}
				}
			}

			caveSeed = (int)Mathf.Abs(seedGenerator - 39000f);

			UpdateTerrainChunk ();
		}

	

		public void UpdateTerrainChunk() {
			if (mapDataReceived) {
				float viewerDstFromNearestEdge = Mathf.Sqrt (bounds.SqrDistance (viewerPosition));
				bool visible = viewerDstFromNearestEdge <= maxViewDst;
				bool renderDungeon = viewerDstFromNearestEdge <= maxViewDst / 2;

				SetVisible (visible, renderDungeon);

				if (visible) {
					int lodIndex = 0;

					for (int i = 0; i < detailLevels.Length - 1; i++) {
						if (viewerDstFromNearestEdge > detailLevels [i].visibleDstThreshold) {
							lodIndex = i + 1;
						} else {
							break;
						}
					}

					if (lodIndex != previousLODIndex || objectIsCreated) {
						LODMesh lodMesh = lodMeshes [lodIndex];
						if (lodMesh.hasMesh) {
							previousLODIndex = lodIndex;
							meshFilter.mesh = lodMesh.mesh; /////
						} else if (!lodMesh.hasRequestedMesh) {
							lodMesh.RequestMesh (mapData);
						}
					}

					if (lodIndex == 0) {
						if (collisionLODMesh.hasMesh) {
							meshCollider.sharedMesh = collisionLODMesh.mesh; /////
						} else if (!collisionLODMesh.hasRequestedMesh) {
							collisionLODMesh.RequestMesh (mapData);
						}
					}

					terrainChunksVisibleLastUpdate.Add (this);
				}
			}
		}

		public void SetVisible(bool visible, bool renderDungeon) {
			if(visible && !objectIsCreated)
			{
				meshObject = new GameObject("Terrain Chunk " + position);
				meshRenderer = meshObject.AddComponent<MeshRenderer>();
				meshFilter = meshObject.AddComponent<MeshFilter>();
				meshCollider = meshObject.AddComponent<MeshCollider>();
				meshRenderer.material = material;

				meshObject.transform.position = positionV3 * scale;
				meshObject.transform.parent = parent;
				meshObject.transform.localScale = Vector3.one * scale;

				Texture2D texture = TextureGenerator.TextureFromColourMap (mapData.colourMap, MapGenerator.mapChunkSize, MapGenerator.mapChunkSize);
				meshRenderer.material.mainTexture = texture;

				int layer = LayerMask.NameToLayer("TerrainTest");
				meshObject.layer = layer;

				if(hasCave && renderDungeon)
				{
					dungeon = new Dungeon(position, meshObject.GetComponent<Transform>(), caveSeed);
					caveEntrance = new CaveEntrance(position, meshObject.GetComponent<Transform>(), dungeon.resources[0].transform.position);
				}

				objectIsCreated = true;
			}
			else if(!visible && objectIsCreated)
			{
				Destroy(meshObject);
				objectIsCreated = false;
			}
		}
	}

	public class CaveEntrance {

		GameObject meshObject;
		// CylinderGenerator cylinderGenerator;// = new CylinderGenerator();
		// MeshRenderer meshRenderer;
		// MeshFilter meshFilter;
		// MeshCollider meshCollider;
		public CaveEntrance(Vector2 position, Transform parent, Vector3 portalPosition) {
			Vector3 positionV3 = new Vector3(position.x,90,position.y);

			// meshObject = new GameObject("Dungeon Portal");
			// meshRenderer = meshObject.AddComponent<MeshRenderer>();
			// meshFilter = meshObject.AddComponent<MeshFilter>();
			// meshCollider = meshObject.AddComponent<MeshCollider>();
			// meshObject.AddComponent<PortalInteractable>();
			// // meshRenderer.material = material;

			// cylinderGenerator = meshObject.AddComponent<CylinderGenerator>();
			// Mesh cylinderMesh = cylinderGenerator.GenerateCylinder();
			// meshFilter.mesh = cylinderMesh;
			// meshCollider.sharedMesh = cylinderMesh;
			// meshCollider.convex = true;
			// meshCollider.isTrigger = true;

			// meshObject.transform.position = positionV3 * scale;
			// meshObject.transform.rotation = new Quaternion(-0.707106829f,0,0,0.707106829f);
			// meshObject.transform.parent = parent;
			// meshObject.transform.localScale = Vector3.one * scale;

			meshObject = (GameObject)Instantiate(Resources.Load("Prefabs/DungeonPortal"), positionV3 * scale, new Quaternion(0,0,0,0), parent);
			meshObject.GetComponent<PortalInteractable>().teleportPosition = new Vector3(portalPosition.x, -40, portalPosition.z);
		}
	}

	public class Dungeon {

		GameObject meshObject;
		bool hasResources;
		public List<GameObject> resources;

		public Dungeon(Vector2 position, Transform parent, float _caveSeed) {
			Vector3 positionV3 = new Vector3(position.x,-50,position.y);
			hasResources = false;

			meshObject = (GameObject)Instantiate(Resources.Load("Prefabs/CaveGenerator"), positionV3 * scale, new Quaternion(0,0,0,0), parent);

			meshObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
			meshObject.GetComponent<CaveMapGenerator>().seed = _caveSeed.ToString();
			meshObject.GetComponent<CaveMapGenerator>().CreateDungeon();

			if(!hasResources)
			{
				resources = meshObject.GetComponent<DungeonResources>().GetResources(positionV3);
				hasResources = true;
			}
			else
			{
				meshObject.GetComponent<DungeonResources>().SetResources(resources);
			}
			
		}
	}

	class LODMesh {

		public Mesh mesh;
		public bool hasRequestedMesh;
		public bool hasMesh;
		int lod;
		System.Action updateCallback;

		public LODMesh(int lod, System.Action updateCallback) {
			this.lod = lod;
			this.updateCallback = updateCallback;
		}

		void OnMeshDataReceived(MeshData meshData) {
			mesh = meshData.CreateMesh ();
			hasMesh = true;

			updateCallback ();
		}

		public void RequestMesh(MapData mapData) {
			hasRequestedMesh = true;
			mapGenerator.RequestMeshData (mapData, lod, OnMeshDataReceived);
		}

	}

	[System.Serializable]
	public struct LODInfo {
		public int lod;
		public float visibleDstThreshold;
		public bool useForCollider;
	}

}