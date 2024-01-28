using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{ 
    PlayerData playerData; //calculate
    bool isCollide = false;

    void Start()
    {
        playerData = gameObject.GetComponent<PlayerData>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerData.collideWith = collision.gameObject;
        playerData.objCollideBehaivior = playerData.collideWith.GetComponent<ObjectCollsionBehaivior>();
        if (playerData.interactBtnImageDisplay != null && playerData.objCollideBehaivior != null)
        {
            isCollide = true;
            playerData.interactBtnSpriteToRender = playerData.objCollideBehaivior.getObjectSprite();
        }
        Debug.Log("Collision started.");
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        isCollide = false;
        ExitTriggerAndCollide();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isCollide == false)
        {
            playerData.collideWith = collision.gameObject;
            playerData.objCollideBehaivior = playerData.collideWith.GetComponent<ObjectCollsionBehaivior>();
            if (playerData.interactBtnImageDisplay != null && playerData.objCollideBehaivior != null)
            {
                playerData.interactBtnSpriteToRender = playerData.objCollideBehaivior.getObjectSprite();
            }
        }
        Debug.Log("Entered trigger area.");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ExitTriggerAndCollide();
        Debug.Log("Exited trigger area.");
    }
    private void ExitTriggerAndCollide()
    {
        playerData.interactBtnSpriteToRender = null;
        playerData.collideWith = null;
        playerData.objCollideBehaivior = null;
    }
}
