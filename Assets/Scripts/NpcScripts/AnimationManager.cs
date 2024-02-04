using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private void Start() {
        animator = gameObject.GetComponent<Animator>();
    }
    public void MoveAnimation(string Exception)
    {
        animator.SetBool("isMoveUp", (Exception == "Up"));
        animator.SetBool("isMoveDown", (Exception == "Down"));
        animator.SetBool("isMoveRight", (Exception == "Right"));
        animator.SetBool("isMoveLeft", (Exception == "Left"));
    }
    public void IdleAnimation(string DirectionToIDle){
        animator.SetBool("isIdleUp", (DirectionToIDle == "Up"));
        animator.SetBool("isIdleDown", (DirectionToIDle == "Down"));
        animator.SetBool("isIdleRight", (DirectionToIDle == "Right"));
        animator.SetBool("isIdleLeft", (DirectionToIDle == "Left"));
    }
    public void SitIdleAnimation(string DirectionToIDle){
        animator.SetBool("isSitIdle", (DirectionToIDle == "Down"));
        animator.SetBool("isSitRightIdle", (DirectionToIDle == "Right"));
        animator.SetBool("isSitLeftIdle", (DirectionToIDle == "Left"));
    }
    public void SitEatingAnimation(string DirectionToIDle){
        animator.SetBool("isEating", (DirectionToIDle == "Down"));
        animator.SetBool("isEatingRight", (DirectionToIDle == "Right"));
        animator.SetBool("isEatingLeft", (DirectionToIDle == "Left"));
    }
}
