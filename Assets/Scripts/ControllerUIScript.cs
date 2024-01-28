using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ControllerUIScript : MonoBehaviour
{

    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button UpButton = root.Q<Button>("Up");
        Button DownButton = root.Q<Button>("Down");
        Button RightButton = root.Q<Button>("Right");
        Button LeftButton = root.Q<Button>("Left");
        UpButton.RegisterCallback<PointerEnterEvent>(OnUpPointerEnter);
        DownButton.RegisterCallback<PointerEnterEvent>(OnPointerEnter);
        RightButton.RegisterCallback<PointerEnterEvent>(OnPointerEnter);
        LeftButton.RegisterCallback<PointerEnterEvent>(OnPointerEnter);

    }
    private void OnPointerEnter(PointerEnterEvent evt)
    {
        Debug.Log("Pointer Enter");
    }
    private void OnUpPointerEnter(PointerEnterEvent evt)
    {
        Debug.Log("Up Pointer Enter");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
