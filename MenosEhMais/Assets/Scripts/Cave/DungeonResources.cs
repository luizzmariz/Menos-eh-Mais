using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonResources : MonoBehaviour
{
    DungeonGrid grid;
    public List<GridNode> avaiableNodes;

    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<DungeonGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> GetResources(Vector3 position)
    {
        //StartCoroutine(WaitGrid());

        List<GameObject> resources = new List<GameObject>();
        List<GridNode> resourceNodes = new List<GridNode>();
        Vector3 checkVector = new Vector3(100,100,100);

        if(avaiableNodes.Count < 1)
        {
            Debug.Log("se fudeu");
        }

        foreach(GridNode node in avaiableNodes)
        {
            if((node.worldPosition.x - position.x) < checkVector.x || (node.worldPosition.z - position.z) < checkVector.z)
            {
                checkVector = node.worldPosition;
            }

            if(node.gridY % 5 == 0 && node.gridX % 7 == 0 && (node.gridX + node.gridY) % 2 == 1)
            {
                resourceNodes.Add(node);
            }
        }

        GameObject portalExit = (GameObject)Instantiate(Resources.Load("Prefabs/DungeonPortal"), checkVector, new Quaternion(0,0,0,0), this.gameObject.transform);
        portalExit.transform.localScale = new Vector3(1, 10, 1);
        portalExit.GetComponent<PortalInteractable>().teleportPosition = new Vector3(position.x, 90, position.z);
        resources.Add(portalExit);

        foreach(GridNode node in resourceNodes)
        {
            if(node.worldPosition == checkVector)
            {
                continue;
            }
            GameObject item = (GameObject)Instantiate(Resources.Load("Prefabs/Cube"), new Vector3(node.worldPosition.x , node.worldPosition.y - 1, node.worldPosition.z), new Quaternion(0,0,0,0), this.gameObject.transform);
            resources.Add(item);
        }

        return resources;
    }

    public void SetResources(List<GameObject> resources)
    {
        
    }

    // IEnumerator WaitGrid()
    // {
    //     yield return new WaitUntil(() => avaiableNodes.Count > 0);
    // }

}
