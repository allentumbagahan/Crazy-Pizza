using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using Photon.Pun;
public class ShopUI : MonoBehaviour
{
    public VisualTreeAsset slotComponent; //attach
    public VisualTreeAsset selectedItemComponent; //attach
    public StyleSheet ussStyleSheet; //attach
    public List<ShopItem> items; //attach
    public GameObject shopCrate; //attach
    VisualElement root;
    VisualElement slotContainer; 
    Button closeBtn;
    Button btn1;
    VisualElement modal;
    //
    //selectedItemList
    //
    VisualElement selectedItemList; 
    [SerializeField]
    public List<ShopSelectedItemData> selectedItemDataList = new List<ShopSelectedItemData>();
    void OnEnable()
    {
        float filledRowCount = items.Count/4.0f;
        int freeSlot = 4 - (int)((filledRowCount-((int)(items.Count/4)))*4);

        //
        //root
        //
        root = GetComponent<UIDocument>().rootVisualElement;

        //
        //slotContainer
        //
        slotContainer = root.Q<VisualElement>("SlotsContainer");
        //
        //selectedItemList
        //
        selectedItemList = root.Q<VisualElement>("SelectedItemListContainer");
        //
        //closeBtn
        //
        closeBtn = root.Q<Button>("CloseButton");
        closeBtn.clicked += () => 
        {
            Close();
        };
        //
        //btn1
        //
        btn1 = root.Q<Button>("Button1");
        btn1.clicked += () => 
        {
        };
        Debug.Log(freeSlot + " slot");
 
        foreach (var item in items)
        {
            AddSlot(item);
        }
        for (int i = 1; i <= freeSlot; i++)
        {
            AddSlot(null);
        }
    }
    public void Close()
    {
        root.style.display = DisplayStyle.None;
    }
    public void Show()
    {
        Debug.Log("allen");
        root.style.display = DisplayStyle.Flex;
    }
    void AddSlot(ShopItem itemDetails){
        VisualElement instantiatedSlot = slotComponent.CloneTree();
        instantiatedSlot.styleSheets.Add(ussStyleSheet);
        instantiatedSlot.AddToClassList("SlotStyle");
        if(itemDetails != null)
        {
            VisualElement image = instantiatedSlot.Q<VisualElement>("ItemImage");
            Label price = instantiatedSlot.Q<Label>("ItemPrice");
            StyleBackground backgroundImage = new StyleBackground(itemDetails.ItemIcon);
            image.style.backgroundImage = backgroundImage;
            price.text = itemDetails.Price + "";
        }
        instantiatedSlot.RegisterCallback<ClickEvent>((ClickEvent evt)=>
        {
            AddSelectedItem(itemDetails.Name, itemDetails.Price);
        });
        slotContainer.Add(instantiatedSlot);
    }
    void AddSelectedItem(string name, int price){
        List<ShopSelectedItemData> containSameItem = selectedItemDataList.Where(elem => elem.Name == name && elem.Quantity < 9).ToList();
        VisualElement instantiatedSelectedItemComnponent = selectedItemComponent.CloneTree();
        instantiatedSelectedItemComnponent.styleSheets.Add(ussStyleSheet);
        instantiatedSelectedItemComnponent.AddToClassList("SelectedItem");
        if(containSameItem.Count > 0)
        {
          selectedItemDataList[selectedItemDataList.IndexOf(containSameItem[0])].IncreaseQuantity();
        }
        else
        {
            Vector2 hideItemPos = new Vector2(99999, 99999);
            GameObject crateBox = PhotonNetwork.Instantiate(shopCrate.name, hideItemPos, Quaternion.identity);
            selectedItemDataList.Add(new ShopSelectedItemData(name, 1, price, instantiatedSelectedItemComnponent, selectedItemDataList, crateBox));
            selectedItemList.Add(instantiatedSelectedItemComnponent);
        } 
    }
}
