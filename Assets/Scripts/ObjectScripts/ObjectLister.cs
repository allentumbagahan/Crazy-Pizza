using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLister : MonoBehaviour
{
    public List<GameObject> furnitureList;
    public List<GameObject> CustomerInteraction;
    public List<GameObject> itemList;
    public List<GameObject> customerExit;
    public void AddObject(GameObject obj)
    {
        ObjectData objectData = obj.GetComponent<ObjectData>();
        switch (objectData.objectType)
        {
            case ObjectData.ObjectType.Furniture: furnitureList.Add(obj); break;
            case ObjectData.ObjectType.Item: itemList.Add(obj); break;
            case ObjectData.ObjectType.CustomerExit: customerExit.Add(obj); break;
            case ObjectData.ObjectType.FurnitureCustomerInteraction: CustomerInteraction.Add(obj); break;
        }
       
    }
}
