using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorController : MonoBehaviour
{
    [Header("Attach")]
    [SerializeField] private GameObject objectListerObject; //Attach
    [Header("Initialize")]
    [SerializeField] private ObjectLister objectLister; 
    [SerializeField] private MovePath movePathManager;
    void Start()
    {
        InitializeFields();
        GoToCashier();
    }
    void InitializeFields()
    {
        objectLister = objectListerObject.GetComponent<ObjectLister>();
        movePathManager = GetComponent<MovePath>();
    }
    void GoToCashier()
    {
        List<GameObject> cashierObjectsTemp;
        cashierObjectsTemp = objectLister.GetObjects(ObjectLister.ObjectType.Cashier);
        if(cashierObjectsTemp.Count > 0) 
        {
            GameObject SelectedCashier = cashierObjectsTemp[0];
            movePathManager.MoveTo(SelectedCashier);
        }
    }
}
