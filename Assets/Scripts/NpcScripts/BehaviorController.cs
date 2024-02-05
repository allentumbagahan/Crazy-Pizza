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
    private void FixedUpdate() {
        int i = 0;
        if(movePathManager.TargetReached && i == 0)
        {
            i++;
            GoToChair();
        }
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
    void GoToChair()
    {
        List<GameObject> chairObjectsTemp;
        chairObjectsTemp = objectLister.GetObjects(ObjectLister.ObjectType.Chair);
        if(chairObjectsTemp.Count > 0) 
        {
            GameObject SelectedChair = chairObjectsTemp[0];
            movePathManager.MoveTo(SelectedChair);
        }
    }
}
