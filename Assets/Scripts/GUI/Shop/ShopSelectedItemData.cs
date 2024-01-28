using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopSelectedItemData
{
    private string name;
    private int quantity;
    private int price;
    private int subtotal;
    private GameObject crate;
    private List<ShopSelectedItemData> parentList;
    public VisualElement element;
    

    public ShopSelectedItemData(string itemName, int itemQuantity, int itemPrice, VisualElement thisElement, List<ShopSelectedItemData> parent, GameObject crateBox)
    {
        name = itemName;
        quantity = itemQuantity;
        price = itemPrice;
        subtotal = itemQuantity*itemPrice;
        element = thisElement;
        parentList = parent;
        crate = crateBox;
        initComponent();
    }

    public global::System.String Name { get => name; set => name = value; }
    public global::System.Int32 Quantity { get => quantity; set => quantity = value; }
    public global::System.Int32 Price { get => price; set => price = value; }
    public GameObject Crate { get => crate; set => crate = value; }
    public void IncreaseQuantity()
    {
        quantity++;
        renderComponent();
    }
    public void DecreaseQuantity()
    {
        quantity--;
        renderComponent();
        if(quantity == 0)
        {
            parentList.RemoveAt(parentList.IndexOf(this));
            element.parent.Remove(element);
        }
    }
    public void renderComponent()
    {
            VisualElement itemImage = element.Q<VisualElement>("ItemImageSelected");
            VisualElement imageLabel2 = element.Q<VisualElement>("ImageLabel2");
            Label itemName = element.Q<Label>("ItemName");
            Label itemQty = imageLabel2.Q<Label>("itemQuantity");
            itemName.text = name;
            itemQty.text = quantity + "";

    }
    public void initComponent()
    {
        element.RegisterCallback<ClickEvent>((ClickEvent evt)=>
        {
            this.DecreaseQuantity();
        });
        renderComponent();
    }
}
