using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovePath : MonoBehaviour
{
    [SerializeField] private GameObject pathMap;
    [SerializeField] private List<PositionAndDirection> ResultPath;
    [SerializeField] private PositionAndDirection currentTargetPath;
    [SerializeField] private PathTile path;
    [SerializeField] private bool targetReached;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject targetTemp; //For Testing
    [SerializeField] private Vector3 targetPosition; //Calculating
    [SerializeField] private float targetDistance; //Calculating
    [SerializeField] private AnimationManager animationManager; //Calculating
    public delegate void MoveFunction(string Direction);
    private MoveFunction move;
    private Tilemap pathTileMap;
    Vector3 direction;

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
        ResultPath = path.GetResultsInShorcut();
    }
    private void Start()
    {
        pathTileMap = pathMap.GetComponent<Tilemap>();
         direction = new Vector3(0,0,0);
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
            
            if(currentTargetPath != ResultPath[0] || currentTargetPath == null)
            {
                currentTargetPath = ResultPath[0];
                targetPosition = pathTileMap.CellToWorld(currentTargetPath.Position);
                targetPosition.x += 0.815f;
                targetPosition.y += 0.815f;
                direction = (targetPosition - transform.position);
                if(move != null) move.Invoke(currentTargetPath.Direction);
            }   
            targetReached = false;
            transform.position += (direction.normalized*moveSpeed);
            if((transform.position - targetPosition).sqrMagnitude <moveSpeed*moveSpeed)
            {
                targetReached = true;
                transform.position = targetPosition;
                ResultPath.Remove(currentTargetPath);
            }
        }
    }
    public void SetMoveFunc(MoveFunction moveFunc)
    {
        move = new MoveFunction(moveFunc);
    }
    

}
