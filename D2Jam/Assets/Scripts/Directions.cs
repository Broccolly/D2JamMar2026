using System.Collections.Generic;
using UnityEngine;

public static class Directions
{
    public enum Direction { None, Straight, Left, Right, Up, UpForward };

    public static readonly Dictionary<Direction, Vector3> directionary = new Dictionary<Direction, Vector3>
    {
        { Direction.Left, Vector3.left },
        { Direction.Right, Vector3.right },
        { Direction.Up, Vector3.up },
        { Direction.UpForward, Vector3.up + Vector3.forward },
        { Direction.Straight, Vector3.forward }
    };
}