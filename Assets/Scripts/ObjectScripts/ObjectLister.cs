using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLister : MonoBehaviour
{
    public List<GameObject> furnitureList;
    public List<GameObject> cashier;
    public List<GameObject> CustomerInteraction;
    public List<GameObject> itemList;
    public List<GameObject> customerExit;
    public enum ObjectType
    {
        Furniture,
        FurnitureCustomerInteraction,
        Item,
        CustomerExit,
        Cashier,
        Chair
    }
    [SerializeField] private List<GameObject> ObjectsList;
    public void AddObject(GameObject obj)
    {
        ObjectsList.Add(obj);
    }
    public List<GameObject> GetObjects(ObjectType type)
    {
        return ObjectsList.Where(item => item.GetComponent<ObjectData>().objectType == type).ToList();
    }
}
