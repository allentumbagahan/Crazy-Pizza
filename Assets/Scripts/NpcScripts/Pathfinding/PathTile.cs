using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class PathTile
{
    public GameObject pathMap;
    public PathTile startPath;
    public PathTile parentPath;
    public Vector3Int pathPosition;
    public Vector3Int targetPos;
    public HashSet<PathTile> paths;
    public Dictionary<Vector3Int, PathTile> allPathsVisited;
    public List<PathTile> QueuePathsToGetNodes;
    [SerializeField] protected List<PositionAndDirection> ResultPath;
    public bool IsPath;
    [SerializeField] private static HashSet<Vector3Int> checkedPositions = new HashSet<Vector3Int>();
    public PositionAndDirection.Directions FromParentToThisDirection;
    PathIndicator pathIndicator;
    Tilemap tilemap;

    public PathTile(PathTile startPath, PathTile parent, Vector3Int pathPosition, Vector3Int targetPos, GameObject pathMap)
    {
        if (startPath != null)
        {
            this.startPath = startPath;
            this.pathMap = this.startPath.pathMap;
            allPathsVisited = this.startPath.allPathsVisited;

        }
        else
        {
            this.startPath = this;
            this.pathMap = pathMap;
            allPathsVisited = new Dictionary<Vector3Int, PathTile>();
            QueuePathsToGetNodes = new List<PathTile>();
            ResultPath = new List<PositionAndDirection>();
            Debug.Log(pathMap + "path");
        }
        pathIndicator = this.startPath.pathMap.GetComponent<PathIndicator>();
        tilemap = this.startPath.pathMap.GetComponent<Tilemap>();
        bool isNearX = Math.Abs(pathPosition.x - targetPos.x) == 1 && Math.Abs(pathPosition.y - targetPos.y) == 0;
        bool isNearY = Math.Abs(pathPosition.y - targetPos.y) == 1 && Math.Abs(pathPosition.x - targetPos.x) == 0;
        bool isNearPos = isNearX || isNearY;
        this.paths = new HashSet<PathTile>();
        this.IsPath = false;

        this.parentPath = parent;
        this.pathPosition = pathPosition;
        this.targetPos = targetPos;



        if (!checkedPositions.Add(pathPosition))
        {
            return; // Already checked this position
        }

        if (pathPosition == targetPos)
        {
            Debug.Log("test target reached");
            this.startPath.IsPath = true;
            setAsPath();
        } 
        if(pathIndicator.block == tilemap.GetTile(targetPos) && isNearPos)
        {
            this.startPath.IsPath = true;
            setAsPath();
        }
    }

    public void setAsPath()
    {
        PositionAndDirection PosAndDirection = new PositionAndDirection(this.pathPosition, this.FromParentToThisDirection);
        this.IsPath = true;
        this.startPath.ResultPath.Insert(0, PosAndDirection);
        if(this.IsPath) this.startPath.pathMap.GetComponent<Tilemap>().SetTile(pathPosition, this.startPath.pathMap.GetComponent<PathIndicator>().testPath);
        clearTempPath();
        if (parentPath != null) parentPath.setAsPath();
    }

    private void clearTempPath()
    {
        PathTile truePath = null;
        foreach (PathTile path in paths)
        {
            if (path.IsPath)
            {
                truePath = path;
                break;
            }
        }
        paths.Clear();
        if (truePath != null) paths.Add(truePath);
    }

    private void GetNodesPath()
    {

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x != 0 && y != 0) continue;
                if (x == 0 && y == 0) continue;
                Vector3Int positionTemp = new Vector3Int(this.pathPosition.x + x, this.pathPosition.y + y, 0);
                
                if (Vector3Int.Distance(positionTemp, this.parentPath?.pathPosition ?? Vector3Int.zero) < float.Epsilon) continue;
                if(this.parentPath != null)
                {
                    if(positionTemp == this.parentPath.pathPosition) continue;
                }
                Debug.Log(positionTemp + " pos tem and pos " + this.pathPosition + " existing? " + (allPathsVisited.ContainsKey(positionTemp)));

                if (pathIndicator.movable == tilemap.GetTile(positionTemp))
                {
                    try
                    {
                        if(!allPathsVisited.ContainsKey(positionTemp))
                        {
                            PathTile childPath = new PathTile(this.startPath, this, positionTemp, this.targetPos, this.startPath.pathMap);
                            if(x == -1) childPath.FromParentToThisDirection = PositionAndDirection.Directions.Left;
                            else if(x == +1) childPath.FromParentToThisDirection = PositionAndDirection.Directions.Right;
                            else if(y == -1) childPath.FromParentToThisDirection = PositionAndDirection.Directions.Down;
                            else if(y == +1) childPath.FromParentToThisDirection = PositionAndDirection.Directions.Top;
                            this.paths.Add(childPath);
                            allPathsVisited[positionTemp] = childPath;
                            this.startPath.QueuePathsToGetNodes.Add(childPath);
                        }
                    }
                    catch (System.Exception err)
                    {
                        Debug.Log(err);
                    }
                }
            }
        }


        Debug.Log(paths.Count + "count " + this.pathPosition);
    }
    public void StartPathFinding()
    {
        this.GetNodesPath();    
        while (!this.startPath.IsPath)
        {    
            List<PathTile> queueTemp = this.startPath.QueuePathsToGetNodes;
            if(queueTemp.Count >= 1)
            {
                queueTemp[0].GetNodesPath();
                this.startPath.QueuePathsToGetNodes.RemoveAt(0);
            }
        }
    }
    public List<PositionAndDirection> GetResults()
    {
        return ResultPath;
    }
}
