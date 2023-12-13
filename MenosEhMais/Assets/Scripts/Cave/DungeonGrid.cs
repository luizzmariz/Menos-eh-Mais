using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonGrid : MonoBehaviour {

	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	public float littleDownValue; //must be 2.5;
	GridNode[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	public void CreateGrid() {
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);

		grid = new GridNode[gridSizeX,gridSizeY];
		Vector3 worldBottomLeft = transform.position -Vector3.up * littleDownValue - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;

		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {
				bool walkable = true;
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				if(x == 0 || y == 0 || x == gridSizeX - 1 || y == gridSizeY - 1)
				{
					walkable = false;
				}
				else
				{
					walkable = !(Physics.CheckSphere(worldPoint,nodeRadius,unwalkableMask));
				}
				grid[x,y] = new GridNode(walkable,worldPoint, x,y);
			}
		}

		CheckNeighbours();
	}

	public void CheckNeighbours() {
		List<int> neighborhoods = new List<int>();
		//int[] neighborhoods = new int[0];
		int neighborhood = 1;

		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {
				if(grid[x,y].walkable == false || grid[x,y].neighborhood != 0)
					continue;
				neighborhoods.Add(GetNeighbours(grid[x,y], neighborhood));
				neighborhood++;
			}
		}

		int largestNeighborhood = 0;
		int neighborhoodMembers = 0;

		for(int x = 0; x < neighborhoods.Count; x++)
		{
			if(neighborhoods[x] > neighborhoodMembers)
			{
				neighborhoodMembers = neighborhoods[x];
				largestNeighborhood = (x+1);
			}
			//Debug.Log("Neighborhood " + x+1 + " has " + neighborhoods[x] + " members");
		}

		List<GridNode> avaiableNodes = new List<GridNode>();

		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {
				if(grid[x,y].neighborhood == largestNeighborhood)
				{
					avaiableNodes.Add(grid[x,y]);
					continue;
				}
				grid[x,y].walkable = false;
			}
		}

		this.gameObject.GetComponent<DungeonResources>().avaiableNodes = avaiableNodes;
	}

	public int GetNeighbours(GridNode node, int _neighborhood) {
		//List<GridNode> neighbours = new List<GridNode>();
		int neighbours = 1;

		node.neighborhood = _neighborhood;

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				// if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
				// 	neighbours.Add(grid[checkX,checkY]);
				// }

				if(grid[checkX,checkY].walkable && grid[checkX,checkY].neighborhood == 0)
				{
					neighbours += GetNeighbours(grid[checkX,checkY], _neighborhood);
				}
			}
		}

		return neighbours;
	}

	public List<GridNode> path;
	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,gridWorldSize.y,1));

		if (grid != null) {
			foreach (GridNode n in grid) {
				Gizmos.color = (n.walkable)?Color.white:Color.red;
				if (path != null)
					if (path.Contains(n))
						Gizmos.color = Color.black;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
			}
		}
	}
}