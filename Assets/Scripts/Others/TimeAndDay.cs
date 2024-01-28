using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeAndDay : MonoBehaviour
{
    public int Hour = 0;
    public int Min = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("moveTimeMin", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void moveTimeMin(){
        Min++;
    }
}
