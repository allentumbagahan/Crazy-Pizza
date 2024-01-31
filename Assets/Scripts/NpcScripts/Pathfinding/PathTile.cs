using System.Collections.Generic;
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
    public bool IsPath;
    [SerializeField] private static HashSet<Vector3Int> checkedPositions = new HashSet<Vector3Int>();

    public PathTile(PathTile startPath, PathTile parent, Vector3Int pathPosition, Vector3Int targetPos, GameObject pathMap)
    {
        this.paths = new HashSet<PathTile>();
        this.IsPath = false;
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
            Debug.Log(pathMap + "path");
        }

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
        else
        {
            

        }
    }

    public void setAsPath()
    {
        this.IsPath = true;
        Debug.Log("TruePath " + this.pathPosition);
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
        var pathIndicator = this.startPath.pathMap.GetComponent<PathIndicator>();
        var tilemap = this.startPath.pathMap.GetComponent<Tilemap>();

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
            queueTemp[0].GetNodesPath();
            this.startPath.QueuePathsToGetNodes.RemoveAt(0);
        }
    }
}
