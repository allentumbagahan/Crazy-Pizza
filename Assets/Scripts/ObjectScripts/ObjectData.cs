using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectData : MonoBehaviour
{
    [Header("Setup")]
    public string objectName;
    //calculate
    [Header("Calculate")]
    public Vector3 worldPosition;
    public Vector3Int cellPosition; //base on furniture and decor tilemap
    [Header("Cell Positions To World")]
    public Vector3 cellPositionToWorld;
    public Vector3 cellPosToWorldLeft;
    public Vector3 cellPosToWorldRight;
    public Vector3 cellPosToWorldUp;
    public Vector3 cellPosToWorldDown;
    public Tilemap furnitureTilemap;
    public enum ObjectType
    {
        Furniture,
        FurnitureCustomerInteraction,
        Item,
        CustomerExit
    }
    public enum PositionToIn
    {
        Up,
        Down,
        Right,
        Left,
        Center
    }
    [Header("Set or Attach")]
    public List<PositionToIn> positionToIn = new List<PositionToIn>();
    public ObjectType objectType = ObjectType.Furniture;
    void Start()
    {
        //find and add this object to objectLister
        GameObject objCentral = GameObject.FindWithTag("ObjectLister");
        ObjectLister objLister = objCentral.GetComponent<ObjectLister>();
        objLister.AddObject(gameObject);

        //intialize the world position and other data
        furnitureTilemap = GameObject.Find("Furniture-Decor").GetComponent<Tilemap>();
        worldPosition = transform.position;
        cellPosition = furnitureTilemap.WorldToCell(worldPosition);
        cellPositionToWorld = findCellPosToWorld(0,0);
        cellPosToWorldLeft = findCellPosToWorld(-1, 0);
        cellPosToWorldRight = findCellPosToWorld(1, 0);
        cellPosToWorldUp = findCellPosToWorld(0, 1);
        cellPosToWorldDown = findCellPosToWorld(0, -1);
    }

    Vector3 findCellPosToWorld(int horizontalOffset, int verticalOffset)
    {
        Vector3Int cellPosTemp = new Vector3Int(cellPosition.x + horizontalOffset, cellPosition.y + verticalOffset + 1, cellPosition.z);
        Vector3 resPos = furnitureTilemap.CellToWorld(cellPosTemp);
        return new Vector3(resPos.x + 0.1f, resPos.y , resPos.z);
    }
    public Vector3 getBestPositionToIn()
    {
        switch ((positionToIn.Count > 0)? positionToIn[Random.Range(0, positionToIn.Count)] : PositionToIn.Down)
        {
            case PositionToIn.Left: return cellPosToWorldLeft;
            case PositionToIn.Right: return cellPosToWorldRight; 
            case PositionToIn.Up: return cellPosToWorldUp; 
            case PositionToIn.Down: return cellPosToWorldDown; 
            case PositionToIn.Center: return worldPosition;
            default: return new Vector3(0,0);
        }
    }
}
