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
    [SerializeField] private Vector3 targetPosition; //Calculating
    [SerializeField] private float targetDistance; //Calculating
    public delegate void MoveFunction(string Direction);
    private MoveFunction move;
    private Tilemap pathTileMap;
    Vector3 direction;

    public global::System.Boolean TargetReached { get => targetReached; set => targetReached = value; }

    //Entry
    public void MoveTo(GameObject targetObject)
    {
        InitializeField();
        Debug.Log("Cashier " + targetObject + " pathTileMap " + pathTileMap);
        Vector3Int targetCellPosTemp = pathTileMap.WorldToCell(targetObject.transform.position);
        Vector3Int thisCellPosTemp = pathTileMap.WorldToCell(gameObject.transform.position);
        Move(thisCellPosTemp, targetCellPosTemp);
        path = null;
    }
    void Move(Vector3Int thisPosition, Vector3Int targetPosition)
    {
        //find path
        path = new PathTile(null, null, thisPosition, targetPosition, pathMap); //new Vector3Int(0, -5,0)
        path.StartPathFinding();
        ResultPath = path.GetResultsInShorcut();
        //path.DisposePathFinding();
    }
    private void Start()
    {
        InitializeField();
    }
    private void InitializeField()
    {
        pathTileMap = pathMap.GetComponent<Tilemap>();
        direction = new Vector3(0,0,0);
    }
    private void Update() {
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
                transform.position = targetPosition;
                ResultPath.Remove(currentTargetPath);
            }
        }
        if(ResultPath.Count == 0 && !targetReached)
        {
            path = null;
            targetReached = true;
        }
    }
    public void SetMoveFunc(MoveFunction moveFunc)
    {
        move = new MoveFunction(moveFunc);
    }
    

}
