using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinDisplayText : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI coinDisplay;
    public CoinCounter coinCounter;


    // Update is called once per frame
    void Update()
    {
        if(coinDisplay != null){
            string toDisplayText = "0";
            if(coinCounter.totalCoin <= 999)
            {
               toDisplayText = coinCounter.totalCoin.ToString("0.00");
            }
            else if(coinCounter.totalCoin <= 9999)
            {
                toDisplayText = (coinCounter.totalCoin/1000).ToString("0.00") + "K";
            }
            else if(coinCounter.totalCoin <= 99999)
            {
                toDisplayText = (coinCounter.totalCoin/10000).ToString("0.00") + "M";
            }
            else if(coinCounter.totalCoin <= 999999)
            {
                toDisplayText = (coinCounter.totalCoin/100000).ToString("0.00") + "B";
            }
            else if(coinCounter.totalCoin <= 9999999)
            {
                toDisplayText = (coinCounter.totalCoin/1000000).ToString("0.00") + "T";
            }
            coinDisplay.text  = toDisplayText;

        }
    }
}
