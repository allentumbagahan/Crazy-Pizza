using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerData : MonoBehaviour
{
    public GameObject interactBtnImageDisplay; //attach
    public GameObject interactBtn; //attach

    public List<GameObject> inventory; //calculate
    public GameObject collideWith; //calculate
    public SpriteRenderer InteractBtnSpriteRender; //calculate
    public Sprite defaultInteractBtnSprite; //calculate
    public Sprite interactBtnSpriteToRender;
    public ObjectCollsionBehaivior objCollideBehaivior; //calculate
    
    private PhotonView view;

    public void addItemInInventory(GameObject Item){ 
        if(Item != null){
            inventory.Add(Item);
        }
     }
    void Start()
    {
        view = GetComponent<PhotonView>();
        if(view.IsMine)
        {
            interactBtn = GameObject.Find("Interact");
            ControllerBehavior interactBtnControllerBehaivior = interactBtn.GetComponent<ControllerBehavior>();
            interactBtnControllerBehaivior.Player = gameObject;
            interactBtnControllerBehaivior.playerData = gameObject.GetComponent<PlayerData>();
            interactBtnControllerBehaivior.playerMovement = gameObject.GetComponent<PlayerMovement>();
            interactBtnControllerBehaivior.playerCollision = gameObject.GetComponent<PlayerCollision>();
            interactBtnImageDisplay = GameObject.Find("InteractImage");
            InteractBtnSpriteRender = interactBtnImageDisplay.GetComponent<SpriteRenderer>();
            defaultInteractBtnSprite = InteractBtnSpriteRender.sprite;
        }
    }
    void Update()
    {
        if(interactBtnImageDisplay == null) interactBtnImageDisplay = GameObject.Find("InteractImage");
        if(interactBtn == null ) interactBtn = GameObject.Find("Interact");
        if(InteractBtnSpriteRender == null && interactBtnImageDisplay) InteractBtnSpriteRender = interactBtnImageDisplay.GetComponent<SpriteRenderer>();
        if(interactBtnSpriteToRender != null){
            InteractBtnSpriteRender.sprite = interactBtnSpriteToRender;
        }else{
            if (inventory.Count > 0)
            {
                InteractBtnSpriteRender.sprite = inventory[0].GetComponent<SpriteRenderer>().sprite;
            }else
            {
                if(collideWith == null) { InteractBtnSpriteRender.sprite = defaultInteractBtnSprite; }
            }
        }

    }
    public void playerHoldInteract(int holdTime){
        if(objCollideBehaivior != null && collideWith != null){
            objCollideBehaivior.processManuallly(holdTime);
            objCollideBehaivior.lastPlayerCollideData = this;
        }
    }
    public void playerInteract()
    {
        if (collideWith != null && inventory.Count == 0 && objCollideBehaivior != null)
        {
            addItemInInventory(objCollideBehaivior.interact(null));
        }
        else
        {
            if(inventory.Count == 1)
            {
                if(objCollideBehaivior != null){
                    if(objCollideBehaivior.isReceiveItem){
                        addItemInInventory(objCollideBehaivior.interact(inventory[0]));
                        inventory.RemoveAt(0);
                    }
                }else{
                    inventory[0].transform.position = new Vector2(transform.position.x, transform.position.y - 0.8f);
                    inventory.RemoveAt(0);
                }
            }
        }
    }

}
