using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OuterTilemapScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger tilemap");
        gameObject.GetComponent<TilemapRenderer>().enabled = false;
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        gameObject.GetComponent<TilemapRenderer>().enabled = true;
    }
}
