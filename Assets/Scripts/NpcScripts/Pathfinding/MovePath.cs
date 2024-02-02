using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovePath : MonoBehaviour
{
    [SerializeField] private GameObject pathMap;
    [SerializeField] private List<PositionAndDirection> ResultPath;
    [SerializeField] private PathTile path;
    [SerializeField] private bool targetReached;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject targetTemp; //For Testing
    [SerializeField] private Vector3 targetPosition; //Calculating
    [SerializeField] private float targetDistance; //Calculating
    private Tilemap pathTileMap;

    //Entry
    public void MoveTo(GameObject targetObject)
    {
        Vector3Int targetCellPosTemp = pathTileMap.WorldToCell(targetObject.transform.position);
        Vector3Int thisCellPosTemp = pathTileMap.WorldToCell(gameObject.transform.position);
        Move(thisCellPosTemp, targetCellPosTemp);
    }
    void Move(Vector3Int thisPosition, Vector3Int targetPosition)
    {
        //find path
        path = new PathTile(null, null, thisPosition, targetPosition, pathMap); //new Vector3Int(0, -5,0)
        path.StartPathFinding();
        ResultPath = path.GetResults();
    }
    private void Start()
    {
        pathTileMap = pathMap.GetComponent<Tilemap>();
    }
    private void Update() {
        if(targetTemp != null)
        {
            MoveTo(targetTemp);
            targetTemp = null;
        }
        //move to each path
        if(ResultPath.Count > 0)
        {
            while(true)
            {
                if(ResultPath.Count > 1)
                {
                    if(ResultPath[0].Direction !=  ResultPath[1].Direction) break;
                    ResultPath.RemoveAt(0);
                }
                else
                {
                    break;
                }

            }
            if(ResultPath.Count > 0)
            {
                targetReached = false;
                targetPosition = pathTileMap.CellToWorld(ResultPath[0].Position);
                targetPosition.x += 0.815f;
                targetPosition.y += 0.815f;
                Vector3 direction = (targetPosition - transform.position);
                transform.position += (direction*moveSpeed);
                targetDistance = Vector3.Distance(transform.position, targetPosition);
                if(targetDistance <= 0.1f)
                {
                    targetReached = true;
                    ResultPath.RemoveAt(0);
                }
            }
        }
    }

}
