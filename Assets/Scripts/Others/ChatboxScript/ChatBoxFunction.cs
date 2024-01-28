using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBoxFunction : MonoBehaviour
{
    public GameObject ChatBoxOrderSprite;
    public GameObject ChatBox;
    public ObjectCollsionBehaivior OrderList; // objectcollisionbehavior
    GameObject OrderSprite;
    public bool showChatBox = false;
    public float opacity = 0.5f;
    void Start()
    {
        if(OrderList != null){ 
            if(OrderList.recieveItem.Count > 0){
                OrderSprite = OrderList.recieveItem[0];
            }
        }
        if(ChatBoxOrderSprite != null && OrderSprite != null){
            ChatBoxOrderSprite.GetComponent<SpriteRenderer>().sprite = OrderSprite.GetComponent<SpriteRenderer>().sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Renderer>().enabled = showChatBox;
        ChatBoxOrderSprite.GetComponent<Renderer>().enabled = showChatBox;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Show Box Collide");
        Color currentColor = gameObject.GetComponent<SpriteRenderer>().color;
        currentColor.a = opacity;
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;
        ChatBoxOrderSprite.GetComponent<SpriteRenderer>().color = currentColor;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Show Box Collide Exit");
        Color currentColor = gameObject.GetComponent<SpriteRenderer>().color;
        currentColor.a = 1.0f;
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;
        ChatBoxOrderSprite.GetComponent<SpriteRenderer>().color = currentColor;
    }
}
