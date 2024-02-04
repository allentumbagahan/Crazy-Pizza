using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionAndDirection
{
    public Vector3Int Position;
    public Directions DirectionType;
    public string Direction;
    public enum Directions
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    public PositionAndDirection(Vector3Int Position, Directions Direction)
    {
        this.Position = Position;
        this.DirectionType = Direction;
        switch (DirectionType)
        {
            case Directions.None :
                this.Direction = "Idle";
                break;
            case Directions.Left :
                this.Direction = "Left";
                break;
            case Directions.Right :
                this.Direction = "Right";
                break;
            case Directions.Down :
                this.Direction = "Down";
                break;
            case Directions.Up :
                this.Direction = "Up";
                break;
        }
    }
}
