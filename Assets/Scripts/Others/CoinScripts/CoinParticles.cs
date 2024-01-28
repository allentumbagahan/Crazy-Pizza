using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CoinParticles : MonoBehaviour
{
    [Header("Attach")]
    public GameObject endPoint;
    public GameObject spawnPoint;
    public GameObject coinPrefab;
    [Space]
    [Space]
    [Header("Data Settings")]
    public float moveSpeed = 5;
    public float coinsTotalAmount;
    public int maxCoin = 5;
    public bool isStart = false;
    [Space]
    [Space]
    [Header("Calculating Data")]
    public int coinGenerateParticles = 0;
    public List<GameObject> coins;
    public List<GameObject> coinsToDelete;
    public float eachCoinAmount;
    void Start(){
        if(endPoint == null)
        {
            endPoint = GameObject.Find("Coin Icon");
        }
    }
    void Update(){
        if(coinsTotalAmount == 0)
        {
            eachCoinAmount = 0;
        }
        if(coinsTotalAmount > 0 && eachCoinAmount == 0 && isStart)
        {
            eachCoinAmount = coinsTotalAmount/maxCoin;
            coinGenerateParticles = maxCoin;
            isStart = false;
        }
        if(coins.Count > 0)
        {
            try
            {
                foreach (GameObject coin in coins)
                {
                    if(coin == null){
                        coins.RemoveAt(coins.IndexOf(coin));
                        continue;
                    }
                    if(Vector2.Distance(coin.transform.position, endPoint.transform.position) > 0.5)
                    {
                        Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
                        Vector2 direction = ((Vector2)endPoint.transform.position - (Vector2)coin.transform.position).normalized;
                        Vector2 velocity = direction * moveSpeed;
                        rb.velocity = velocity;
                    }
                }                
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex);
            }
        }
        if(endPoint != null && spawnPoint != null)
        {
            if(coinGenerateParticles > 0 && coinsTotalAmount > 0)
            {
                GameObject coinTemp = Instantiate(coinPrefab, spawnPoint.transform.position, Quaternion.identity);
                coinTemp.GetComponent<CoinData>().coinAmount = eachCoinAmount;
                coinGenerateParticles--;
                coinsTotalAmount -= eachCoinAmount;
                coins.Add(coinTemp);
                DelayedFor(1.0f);
            }
        }
    }
    IEnumerator DelayedFor(float delay){
        yield return new WaitForSeconds(delay);
    }
}
