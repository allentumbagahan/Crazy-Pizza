using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



public class ObjectCollsionBehaivior : MonoBehaviour
{
    public enum ReceiveType {
        InOrdering, //from 0 to ...
        Match, // what index you give is what index you get
        Comnpleting, //Complete the list of items to get
        AnyItemInReceiveList, //Any item can give base on list
        Any 
    }
    public enum GiveType {
        Display, //display item before getting
        Direct, // get item directly
    }
    public enum GiveSource {
        FixedDefine, //display item before getting
        ThisStorage, // get item directly
    }
    [Header("Setup Data")]
    public Sprite renderingSprite;
    public bool isObtainable = false; //option
    [Space]
    [Header("Receive Settings")]
    public bool isReceiveItem = false; //option
    public List<GameObject> recieveItem;
    public ReceiveType receiveType = ReceiveType.InOrdering; //option
    [Space]
    [Space]
    [Space]
    [Header("Give Settings")]
    public bool isGiveItem = false;
    [Tooltip("Effective if GiveSource is Fixed Defined Item ")]
    public GiveSource giveSource = GiveSource.FixedDefine;
    public GameObject giveItem;
    [Header("Give List if receive type is MATCH")]
    public List<GameObject> giveItems;
    public GiveType giveType = GiveType.Direct; //option;
    int giveItemIndex = 0;
    [Space]
    [Space]
    [Space]
    [Header("Interaction With Other Object Settings")]
    [Tooltip("Enable if send signal to other object when receive data")]
    public bool isInteractWithOther1 = false; 
    public GameObject interactWithThis1; 
    [Header("Item Rendering Settings")]
    public GameObject itemToDisplayRenderingObject;
    [Tooltip("EEffective if itemToDisplayRenderingObject is Null")]
    public Sprite imageDisplayToInteractButton;
    [Header("Processing Item Settings")]
    [Tooltip("Only works in Display Give Type")]
    public bool isProcesItem = false; 
    public PogressBarScript progressSetting;
    public enum ProcessType{
        Auto,
        Longpress,
        Click
    }
    public bool isConfirmBeforeProcess = false;
    public ProcessType processType = ProcessType.Auto;
    public enum ProcessCondition {
        RequireTool, // Player shall handle specific tool to process
        Hand, // Player shall not have any item on hand
        None
        
    }
    public ProcessCondition processCondition = ProcessCondition.None;
    [Header("If RequireTool Enable")]
    public GameObject ToolRequirement;
    [Header("Object Cooldown Settings")]

    
    [Space][Space][Space][Space]
    [Header("Calculating Data")]
    public List<GameObject> itemList;
    SpriteRenderer thisRender; //calculate
    ObjectData objectData; //calculate
    public GameObject itemToDisplayInstatiate;
    public Action executeWhenReceived;
    public Action executeWhenCompleted;
    bool defaultIsReceiveItem;
    public bool isProcessingItem = false; // state of progress bar if ongoing or not
    public int progressCount = 0;
    public bool isNotReadyToGet = false; //use to prevent multiple event from process to get event
    public bool isReadyToProcess = false;
    public PlayerData lastPlayerCollideData;
    public float playerProcessingSpeed;
    void Start()
    {
        defaultIsReceiveItem = isReceiveItem;
        itemList = new List<GameObject>();
        thisRender = gameObject.GetComponent<SpriteRenderer>();
        objectData = gameObject.GetComponent<ObjectData>();
    }

    // Update is called once per frame
    void Update(){
        if(lastPlayerCollideData != null && isProcesItem)
        {
            ValidateProcessCondition();
        }
        if( progressSetting == null){
            isProcesItem = false;
        }
        if(isProcesItem){
            if(isProcessingItem && processType == ProcessType.Auto){
                if(isReadyToProcess || !isConfirmBeforeProcess){ progressSetting.progress += 1; }
            }
            if(isProcessingItem && processType == ProcessType.Longpress){
                progressSetting.progress = progressCount;
            }
            if(progressSetting.progress >= 100){
                if(processType == ProcessType.Longpress){ isNotReadyToGet = true; }
                    itemToDisplayInstatiate = null;
                if(itemToDisplayRenderingObject != null) itemToDisplayRenderingObject.GetComponent<SpriteRenderer>().sprite = null;
                progressSetting.progress = 0;
                isProcessingItem = false;
                if (giveType == GiveType.Display) isReceiveItem = false;
                if(giveType == GiveType.Display){
                    giveItemFunction();
                }else{
                    if(lastPlayerCollideData != null) lastPlayerCollideData.addItemInInventory( giveItemFunction());
                }
            }
        }
    }
    void FixedUpdate()
    {


        if(itemToDisplayInstatiate != null){
            Sprite Display = itemToDisplayInstatiate.GetComponent<SpriteRenderer>().sprite;
            if(itemToDisplayRenderingObject != null){
                itemToDisplayRenderingObject.GetComponent<SpriteRenderer>().sprite = (Display != null)? Display : null;
            }
        }
    }
    public Sprite getObjectSprite()
    {
        // render to interact button when collide
        if(renderingSprite == null){
            return thisRender.sprite; 
        }else{
            if(itemToDisplayInstatiate != null){
                if(itemToDisplayRenderingObject != null){
                    return itemToDisplayRenderingObject.GetComponent<SpriteRenderer>().sprite;
                }else{
                    return imageDisplayToInteractButton;
                }
            }
            return renderingSprite;
        }
        
    }
    public void processManuallly(int progCount){
        progressCount = progCount;
    }
    public GameObject interact(GameObject itemGive)
    {
        if(!isReadyToProcess && isProcessingItem){
            isReadyToProcess = true;
        }
        if(isObtainable){
            transform.position = new Vector2(99999, 99999);
            //Destroy(gameObject);
            return gameObject;
        }else{
            if(itemToDisplayInstatiate != null && !isProcessingItem && giveType == GiveType.Display){
                if(!isNotReadyToGet){
                    Debug.Log("Give the displayed item");
                    GameObject objTemp = itemToDisplayInstatiate;
                    itemToDisplayInstatiate = null;
                    if(itemToDisplayRenderingObject != null) itemToDisplayRenderingObject.GetComponent<SpriteRenderer>().sprite = null;
                    isReceiveItem = defaultIsReceiveItem;
                    return objTemp;
                }else{
                    isNotReadyToGet = false;
                }
            }else{
                if(isReceiveItem && !isProcessingItem){
                    if(itemGive != null){
                        string objName1 = itemGive.GetComponent<ObjectData>().objectName;
                        string objName2 = "";
                        if(receiveType == ReceiveType.InOrdering){
                            objName2 = recieveItem[0].GetComponent<ObjectData>().objectName;
                        }
                        if(receiveType == ReceiveType.AnyItemInReceiveList){
                            foreach (GameObject item in recieveItem)
                            {   
                                objName2 = item.GetComponent<ObjectData>().objectName;
                                if(objName1 == objName2){ break; }
                            }
                        }
                        if(receiveType == ReceiveType.Match){
                            giveItemIndex = 0;
                            foreach (GameObject item in recieveItem)
                            {
                                objName2 = item.GetComponent<ObjectData>().objectName;
                                if(objName1 == objName2){ break; }
                                giveItemIndex++;
                            }
                        }
                        if(receiveType == ReceiveType.Any){
                            objName2 = objName1;
                        }
                        if(objName1 == objName2){
                            Debug.Log("Item give " + objName1);
                            itemList.Add(itemGive);
                            if(isInteractWithOther1){
                                //interact to other
                                if(interactWithThis1 != null){
                                    ObjectCollsionBehaivior interactWithThis1CollisionBehavior = interactWithThis1.GetComponent<ObjectCollsionBehaivior>();
                                    if(interactWithThis1CollisionBehavior != null){
                                        interactWithThis1CollisionBehavior.interact(itemGive);
                                    }
                                }
                                if(executeWhenReceived != null){
                                    executeWhenReceived();
                                }
                                //executeWhenCompleted();
                            }
                            if(isProcesItem){
                                Debug.Log("Process Tess");
                                if(isConfirmBeforeProcess){ isReadyToProcess = false; } //porcessing but not ready to process
                                itemToDisplayInstatiate = itemGive;
                                isProcessingItem = true;
                            }
                            // process to give condition 
                        }else{
                            Debug.Log("Item not needed" + objName1 + "/ item needed : " + objName2);
                            return itemGive; //refund item
                        }
                    }else{
                        Debug.Log("No item in Inventory");
                        return null;
                    }
                }
                if(isGiveItem && !isProcessingItem){
                    return giveItemFunction();
                }
            }
        }

       return null;
    }
    GameObject giveItemFunction(){
        //make gameobject to world
        Vector2 hideItemPos = new Vector2(99999, 99999);
        GameObject itemTemp;
        //calculate the item to be send
        if(receiveType == ReceiveType.Match && giveItemIndex < giveItems.Count){
            itemTemp = Instantiate(giveItems[giveItemIndex], hideItemPos, Quaternion.identity);
        }else{
            if(giveSource == GiveSource.FixedDefine){
                itemTemp = Instantiate(giveItem, hideItemPos, Quaternion.identity);
            }else{
                if(itemList.Count > 0){
                    itemTemp = itemList[itemList.Count - 1];
                    itemList.RemoveAt(itemList.Count - 1);
                }else{
                    itemTemp = null;
                }
            }
        }
        //calculate how item send
        if(giveType == GiveType.Direct){
            return itemTemp;
        }else if(giveType == GiveType.Display){
            Debug.Log("This Object Received item");
            itemToDisplayInstatiate = itemTemp;
            isReceiveItem = false;
        }
        return null;
    }
    public void ValidateProcessCondition(){
        if(processCondition == ProcessCondition.RequireTool ){
            if(lastPlayerCollideData.inventory.Count > 0){
                string playerItemHandleName = lastPlayerCollideData.inventory[0].GetComponent<ObjectData>().objectName;
                string toolRequirementItemName = ToolRequirement.GetComponent<ObjectData>().objectName;
                if(playerItemHandleName == toolRequirementItemName) isProcessingItem = true;
                else isProcessingItem = false;
            }
            else
            {
                isProcessingItem = false;
            }
        } else if(processCondition == ProcessCondition.Hand )
        {
            if(lastPlayerCollideData.inventory.Count == 0) isProcessingItem = true;
            else isProcessingItem = false;
        }
    }

    private void OnValidate()
    {
  
    }
}

