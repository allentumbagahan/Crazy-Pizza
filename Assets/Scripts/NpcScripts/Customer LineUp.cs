using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomerLineUp : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("Use to connect line up")]
    public GameObject nextLinePoint; //calculate
    public InLineDirection inLineDirection;
    public GameObject furnitureInteraction; //calculate
    [Tooltip("Enable if NextLinePoint assign.")]
    public bool isSingleLinePoint = true;
    [Tooltip("Disable to ignore this object in customer furniture selection")]
    public bool isFirstLinePoint = true; 


    [Header("Calculate")]
    public GameObject inLineCustomer; //calculate
    public string inLineDirectionSelected;
    
    GameObject objCentral;
    ObjectLister objLister;

    public GameObject TesttCollideObject;
    public enum InLineDirection{
        Up,
        Down,
        Right,
        Left
    }

    void Start(){
        setInLineDirection();
    }
    void moveCustomerLinePoint(){
        if(nextLinePoint.GetComponent<CustomerLineUp>().inLineCustomer == null && inLineCustomer != null){
            Debug.Log("move customer " + gameObject.name);
                CustomerBehaviorAndData inLineCustomerBehavior = inLineCustomer.GetComponent<CustomerBehaviorAndData>();
                if(inLineCustomerBehavior != null){
                    if(inLineCustomerBehavior.customerMovementPathfinding != null){
                        Debug.Log("moving customer " + gameObject.name);
                        inLineCustomerBehavior.customerMovementPathfinding.targetPos = nextLinePoint.GetComponent<ObjectData>().getBestPositionToIn();
                        inLineCustomerBehavior.OnIdleDirection = nextLinePoint.GetComponent<CustomerLineUp>().inLineDirectionSelected;
                        inLineCustomerBehavior.targetObject = nextLinePoint;
                        nextLinePoint.GetComponent<CustomerLineUp>().inLineCustomer = inLineCustomer;
                        inLineCustomer = null;
                    }
                }
        }
    }
    void Update(){
        if(!isSingleLinePoint && inLineCustomer != null){
            moveCustomerLinePoint();
        }
        if(gameObject.GetComponent<ObjectConnector>() != null && inLineCustomer != null){
            if(gameObject.GetComponent<ObjectConnector>().onCollideFunction == null)
            {
                Debug.Log("Collision function set");
                gameObject.GetComponent<ObjectConnector>().onCollideFunction += onColliderConnector;
            }
        }
    }
    public void GotoHere(GameObject customer){
        inLineCustomer = customer;
    }
    public bool checkLineUp(){
        if(inLineCustomer == null && isFirstLinePoint){
            return true;
        }
        return false;
    }
    void setInLineDirection(){
        switch (inLineDirection)
        {
            case InLineDirection.Up:
                inLineDirectionSelected = "Up";
                break;
            case InLineDirection.Down:     
                inLineDirectionSelected = "Down"; 
                break;
            case InLineDirection.Left:     
                inLineDirectionSelected = "Left"; 
                break;
            case InLineDirection.Right:    
                inLineDirectionSelected = "Right"; 
                break;
            default: 
                inLineDirectionSelected = ""; 
                break;
        }
    }
    void onColliderConnector(GameObject obj){
        if(obj != null && inLineCustomer != null && inLineCustomer.GetComponent<CustomerBehaviorAndData>().isOnTarget){
            TesttCollideObject = obj;
            ObjectCollsionBehaivior collideObjectCollisionBehavior = obj.GetComponent<ObjectCollsionBehaivior>();
            Debug.Log("FTest Customer Line Up Meet" + (collideObjectCollisionBehavior == null));
            if(collideObjectCollisionBehavior != null){
                Debug.Log("Customer Line Up Meet" + obj);
                collideObjectCollisionBehavior.isReceiveItem = inLineCustomer.GetComponent<ObjectCollsionBehaivior>().isReceiveItem;
                collideObjectCollisionBehavior.recieveItem = inLineCustomer.GetComponent<ObjectCollsionBehaivior>().recieveItem;
                collideObjectCollisionBehavior.receiveType = inLineCustomer.GetComponent<ObjectCollsionBehaivior>().receiveType;
                collideObjectCollisionBehavior.isGiveItem = inLineCustomer.GetComponent<ObjectCollsionBehaivior>().isGiveItem;
                collideObjectCollisionBehavior.giveItem = inLineCustomer.GetComponent<ObjectCollsionBehaivior>().giveItem;
                collideObjectCollisionBehavior.interactWithThis1 = inLineCustomer;
            }   
        }
    }
}
