using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenChangeSprite : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite IdleSprite;
    public Sprite CookSprite;
    public void cook(){
        gameObject.GetComponent<SpriteRenderer>().sprite = CookSprite;
    }
    public void idle(){
        gameObject.GetComponent<SpriteRenderer>().sprite = IdleSprite;
    }
}
