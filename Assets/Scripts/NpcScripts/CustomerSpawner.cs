using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CustomerSpawner : MonoBehaviour
{
    public GameObject civilianPrefab; //attach
    public List<GameObject> civilians;
    GameObject objCentralTemp;
    ObjectLister objListerTemp;

    void Start()
    {
       InvokeRepeating("summonCustomer", 5.0f, 5.0f);
       objCentralTemp = GameObject.FindWithTag("ObjectLister"); 
       objListerTemp = objCentralTemp.GetComponent<ObjectLister>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void RandomSummon(){
        if(Random.Range(0, 10) == 1){
            if(PhotonNetwork.IsMasterClient) summonCustomer();
        }
    }
    void summonCustomer()
    {
        Debug.Log("Summon Customer");
        //initialize data
        GameObject randomFurniture = objListerTemp.CustomerInteraction[Random.Range(0, objListerTemp.CustomerInteraction.Count)];
        //calculate
        Vector2 spawnPoint = new Vector2(transform.position.x, transform.position.y);
        GameObject civilianTemp;
        civilianTemp = PhotonNetwork.Instantiate(civilianPrefab.name, spawnPoint, Quaternion.identity);
        civilians.Add(civilianTemp);
        civilianTemp.GetComponent<MovementController2D>().targetPos = randomFurniture.GetComponent<ObjectData>().getBestPositionToIn();
        CustomerBehaviorAndData customerBehaviorAndDataTemp = civilianTemp.GetComponent<CustomerBehaviorAndData>();
        CustomerLineUp lineTarget = randomFurniture.GetComponent<CustomerLineUp>();
        if(lineTarget != null){
            if(lineTarget.checkLineUp()){
                lineTarget.GotoHere(civilianTemp);
                customerBehaviorAndDataTemp.targetObject = randomFurniture;
                customerBehaviorAndDataTemp.OnIdleDirection = lineTarget.inLineDirectionSelected;
                Debug.Log("Summoned Customer");
            }else{
                PhotonNetwork.Destroy(civilianTemp);
            }
        }else{
            PhotonNetwork.Destroy(civilianTemp);
        }
    }
}
