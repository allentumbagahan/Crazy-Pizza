using System;
using System.Collections.Generic;
using UnityEngine;

public class ChairScripts : MonoBehaviour
{
    public GameObject display; // Attach where customer
    public GameObject receivedGameObject; // Attach where customer
    Action<GameObject> a; // Declare the action without initializing it here

    void Start()
    {
        a = (customerLineUp) =>
        {
            Debug.Log(customerLineUp);
            receivedGameObject = customerLineUp;
            Debug.Log("test" + customerLineUp);
            if (customerLineUp.GetComponent<CustomerLineUp>() != null)
            {
                GameObject customer = customerLineUp.GetComponent<CustomerLineUp>().inLineCustomer;
                Debug.Log("test1" + customer);
                if (customer != null)
                {
                    Debug.Log("test2");
                    CustomerBehaviorAndData customerBehavior = customer.GetComponent<CustomerBehaviorAndData>();
                    if (customerBehavior.isOnTarget)
                    {
                        Debug.Log("test3");
                        customerBehavior.isSit = true;
                        Vector2 lastPos = customer.transform.position;
                        Rigidbody2D customerRigidbody = customer.GetComponent<Rigidbody2D>();
                        if (customerRigidbody != null)
                        {
                            //customerRigidbody.simulated = false;
                        }
                        customer.transform.position = display.transform.position;
                    }
                }
            } 
        };

        if (gameObject.GetComponent<ObjectConnectorFunction>() != null)
        {
            gameObject.GetComponent<ObjectConnectorFunction>().func = a;
        }
    }
}
