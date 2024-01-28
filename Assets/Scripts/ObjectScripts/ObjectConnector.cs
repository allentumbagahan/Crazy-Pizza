using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectConnector : MonoBehaviour
{
    // add ObjectConnectorFunction to target object to collide and pass the func
    bool isCalledFunc = false;
    public Action<GameObject> onCollideFunction = null;
    public GameObject collideObject;
    private GameObject prevCollideObject;
    private void OnCollisionEnter2D(Collision2D collision){
        collideObject = collision.gameObject;
        if(collision.gameObject.GetComponent<ObjectConnectorFunction>() != null )
        {
            onCollideFunction = collision.gameObject.GetComponent<ObjectConnectorFunction>().func;
        }
    }
    private void OnCollisionExit2D(Collision2D collision){
        collideObject = null;
    }
    
    void Update(){
        bool isSameObject = prevCollideObject == collideObject;
        if(onCollideFunction != null && collideObject != null){
            Debug.Log("Obj Connector Run" );
            onCollideFunction(collideObject);
            prevCollideObject = collideObject;
        }
    }
}
