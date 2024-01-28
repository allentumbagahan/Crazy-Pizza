using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
public class PlayerMovement : MonoBehaviour
{
    private GameObject mCamera;
    public float moveSpeed = 0.1f;
    private Rigidbody2D rb;
    private Animator animator;
    public FixedJoystick Joystick;
    private PhotonView view;
    Vector2 move;
    private enum JoystickDirection
    {
        None,     
        Up,       
        UpRight,  
        Right,    
        DownRight,
        Down,     
        DownLeft, 
        Left,     
        UpLeft    
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
        if(view.IsMine){
            mCamera = GameObject.Find("Main Camera");
            mCamera.GetComponent<FollowCamera>().objectToFollow = this.gameObject.transform;
            Joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        } 
            

    }
    void Update()
    {
        if(Joystick != null)
        {
            move.x = Joystick.Horizontal;
            move.y = Joystick.Vertical;
        }
    }
    void FixedUpdate()
    {
        switch (GetJoystickDirection(move))
        {
            case JoystickDirection.Down: MoveDown(); break;
            case JoystickDirection.Up: MoveUp(); break;
            case JoystickDirection.Right: MoveRight(); break;
            case JoystickDirection.Left: MoveLeft(); break;
            case JoystickDirection.UpRight: MoveUpRight(); break;
            case JoystickDirection.UpLeft: MoveUpLeft(); break;
            case JoystickDirection.DownRight: MoveDownRight(); break;
            case JoystickDirection.DownLeft: MoveDownLeft(); break;
            case JoystickDirection.None: Stop(); break;
        }
    }
    public void MoveUp()
    {
        Debug.Log("move");
        //animator.SetBool("isMoveUp", true);
        StopAnimation("Up");
        Vector2 velocity = new Vector2(0, moveSpeed);
        rb.velocity = velocity;
    }
    public void MoveDown()
    {
        Debug.Log("move");
        StopAnimation("Down");
        //animator.SetBool("isMoveDown", true);
        Vector2 velocity = new Vector2(0, -moveSpeed);
        rb.velocity = velocity;
    }
    public void MoveLeft()
    {
        Debug.Log("move");
        StopAnimation("Left");
        Vector2 velocity = new Vector2(-moveSpeed, 0);
        rb.velocity = velocity;
    }
    public void MoveRight()
    {
        Debug.Log("move");
        StopAnimation("Right");
        Vector2 velocity = new Vector2(moveSpeed, 0);
        rb.velocity = velocity;
    }
    public void MoveUpRight()
    {
        Debug.Log("move");
        StopAnimation("Right");
        Vector2 velocity = new Vector2(moveSpeed, moveSpeed);
        rb.velocity = velocity;
    }
    public void MoveUpLeft()
    {
        Debug.Log("move");
        StopAnimation("Left");
        Vector2 velocity = new Vector2(-moveSpeed, moveSpeed);
        rb.velocity = velocity;
    }
    public void MoveDownRight()
    {
        Debug.Log("move");
        StopAnimation("Right");
        Vector2 velocity = new Vector2(moveSpeed, -moveSpeed);
        rb.velocity = velocity;
    }
    public void MoveDownLeft()
    {
        Debug.Log("move");
        StopAnimation("Left");
        Vector2 velocity = new Vector2(-moveSpeed, -moveSpeed);
        rb.velocity = velocity;
    }
    public void Stop()
    {
        StopAnimation("");
        rb.velocity = Vector2.zero;
    }
    public void StopAnimation(string Exception)
    {
        animator.SetBool("isMoveUp", (Exception == "Up"));
        animator.SetBool("isMoveDown", (Exception == "Down"));
        animator.SetBool("isMoveRight", (Exception == "Right"));
        animator.SetBool("isMoveLeft", (Exception == "Left"));
    }
    private JoystickDirection GetJoystickDirection(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            return JoystickDirection.None;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = (angle + 360) % 360; // Normalize angle to be in the range [0, 360]

        if (angle >= 22.5f && angle < 67.5f)
        {
            return JoystickDirection.UpRight;
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            return JoystickDirection.Up;
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            return JoystickDirection.UpLeft;
        }
        else if (angle >= 157.5f && angle < 202.5f)
        {
            return JoystickDirection.Left;
        }
        else if (angle >= 202.5f && angle < 247.5f)
        {
            return JoystickDirection.DownLeft;
        }
        else if (angle >= 247.5f && angle < 292.5f)
        {
            return JoystickDirection.Down;
        }
        else if (angle >= 292.5f && angle < 337.5f)
        {
            return JoystickDirection.DownRight;
        }
        else
        {
            return JoystickDirection.Right;
        }
    }



}
