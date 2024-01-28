using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public float totalCoin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<CoinData>() != null)
        {
            Debug.Log(collision.gameObject);
            totalCoin += collision.gameObject.GetComponent<CoinData>().coinAmount;
            Destroy(collision.gameObject);
        }
    }
}
