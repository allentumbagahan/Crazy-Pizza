using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcScriptManager : MonoBehaviour
{
    [SerializeField] private AnimationManager animationManager;
    [SerializeField] private MovePath movePathManager;
    void Start()
    {
        InitializeComponent();
    }
    void InitializeComponent()
    {
        animationManager = GetComponent<AnimationManager>();
        movePathManager = GetComponent<MovePath>();
        //Connect AnimationManager to movePathManager
        movePathManager.SetMoveFunc(SetMoveDirectionAnim);
    }
    private void SetMoveDirectionAnim(string direction)
    {
        Debug.Log("move direction : " + direction);
        animationManager.MoveAnimation(direction);
    }
}
