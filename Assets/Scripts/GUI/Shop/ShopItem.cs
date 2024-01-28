using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
    [CreateAssetMenu]
public class ShopItem : ScriptableObject
{   
    public string Name;
    public Sprite ItemIcon;
    public GameObject ItemGameObjectPrefab;
    public int Price;
}
