using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenPartManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> parts;
    void Update(){
        if(gameObject.GetComponent<ObjectCollsionBehaivior>().progressSetting.progress > 0){
            cook();
        }else{
            idle();
        }
    }
    public void cook(){
        foreach (GameObject part in parts)
        {
            part.GetComponent<OvenChangeSprite>().cook();
        }
    }
    public void idle(){
        foreach (GameObject part in parts)
        {
            part.GetComponent<OvenChangeSprite>().idle();
        }
    }
}
