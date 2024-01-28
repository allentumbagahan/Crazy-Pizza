using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStoredDisplayNodesManager : MonoBehaviour
{
    public List<GameObject> displayBox = new List<GameObject>(); //attach
    public ObjectCollsionBehaivior thisObjectBehavior; // attach
    public int itemListSyncToDisplay = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(itemListSyncToDisplay != thisObjectBehavior.itemList.Count)
        {
            refreshDisplayToEmpty();
            itemListSyncToDisplay = thisObjectBehavior.itemList.Count;
            int indexTemp = 0;
            foreach (GameObject item in thisObjectBehavior.itemList)
            {
                if(indexTemp > displayBox.Count - 1) break;
                displayBox[indexTemp++].GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                
            }
        }
    }
    void refreshDisplayToEmpty(){
        foreach (GameObject display in displayBox)
        {
            display.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
