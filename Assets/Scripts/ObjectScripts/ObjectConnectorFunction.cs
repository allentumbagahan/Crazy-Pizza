using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConnectorFunction : MonoBehaviour
{
    // Set the function data to be call in object connector when connected and collide
    [Header("Overwrite the action of function with name of \"func\"")]
    public Action<GameObject> func; // set this func in other scritps
}
