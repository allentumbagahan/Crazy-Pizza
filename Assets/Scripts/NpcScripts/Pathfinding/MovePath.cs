using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovePath : MonoBehaviour
{
    [SerializeField] private GameObject pathMap;
    [SerializeField] private PathTile path; 

    void Start()
    {
        path = new PathTile(null, null, new Vector3Int(-10,-10,0), new Vector3Int(9, -2,0), pathMap); //new Vector3Int(0, -5,0)
        path.StartPathFinding();
        Debug.Log("test path created" + path.allPathsVisited.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
