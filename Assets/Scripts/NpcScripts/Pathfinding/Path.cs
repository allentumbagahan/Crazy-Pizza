using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Path
{
    public GameObject pathMap;
    public Path startPath;
    public Path parentPath;
    public Vector3Int pathPosition;
    public Vector3Int targetPos;
    public List<Path> paths;
    public bool IsPath;

    public Path(Path startPath, Path parent, Vector3Int pathPosition, Vector3Int targetPos, GameObject pathMap)
    {
        this.paths =new List<Path>();
        this.IsPath = false;
        if(startPath != null) 
        {
            this.startPath = startPath;
            this.pathMap = this.startPath.pathMap;
        }
        else
        {
            this.startPath = this;
            this.pathMap = pathMap;
            Debug.Log(pathMap + "path");
            
        } 
        this.parentPath = parent;
        this.pathPosition = pathPosition;
        this.targetPos = targetPos;
        if(pathPosition == targetPos)
        {
           this.startPath.IsPath = true;
            setAsPath();
        }
        else
        {
            Debug.Log(this.startPath + "");
            if(!this.startPath.IsPath)
            {
                GetNodesPath();
            }
        }
    }

    public void setAsPath()
    {
        this.startPath.pathMap.GetComponent<Tilemap>().SetTile(pathPosition, this.startPath.pathMap.GetComponent<PathIndicator>().testPath);
        this.IsPath = true;
        if(parentPath != null) parentPath.setAsPath();
        clearTempPath();
    }
    private void clearTempPath()
    {
        Path truePath = null;
        foreach (Path path in paths)
        {
            if(path.IsPath) {
                truePath = path;
                break;
            }
        }
        paths.Clear();
        if(truePath != null) paths.Add(truePath);
    }

    private void GetNodesPath()
    {
        for (int x = -1; x <= 1; x++)
        {    
            for (int y = -1; y <= 1; y++)
            {
                if(x != 0 && y != 0) continue;
                if(x == 0 && y == 0) continue;
                Vector3Int positionTemp = new Vector3Int(this.pathPosition.x + x, this.pathPosition.y + y, 0);
                if(this.parentPath != null)
                {
                    if(positionTemp == this.parentPath.pathPosition) continue;
                }
                Debug.Log(this.startPath.pathMap + " aa" + this.startPath + " bb" + this.targetPos + " cc" + positionTemp + " dd" + paths);
                if(this.startPath.pathMap.GetComponent<PathIndicator>().movable == this.startPath.pathMap.GetComponent<Tilemap>().GetTile(positionTemp))
                {
                    try
                    {
                        this.paths.Add(new Path(this.startPath, this, positionTemp, this.targetPos, this.startPath.pathMap));
                    }
                    catch (System.Exception err)
                    {
                        Debug.Log(err);
                    }
                    
                }
            }
        }
    }
}
