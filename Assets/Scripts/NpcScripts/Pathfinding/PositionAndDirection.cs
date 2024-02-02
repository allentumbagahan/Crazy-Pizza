using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionAndDirection
{
    public Vector3Int Position;
    public Directions Direction;
    public enum Directions
    {
        None,
        Left,
        Right,
        Top,
        Down
    }

    public PositionAndDirection(Vector3Int Position, Directions Direction)
    {
        this.Position = Position;
        this.Direction = Direction;
    }
}
