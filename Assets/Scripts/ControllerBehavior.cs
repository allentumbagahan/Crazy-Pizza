using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public enum MovementDirection
    {
        Up, Down, Left, Right, Interact
    }
    public MovementDirection direction;
    public GameObject Player; // auto attach by player data
    public PlayerMovement playerMovement; // auto attach by player data
    public PlayerCollision playerCollision; // auto attach by player data
    public PlayerData playerData; // auto attach by player data
    bool buttonHold = false;
    int holdCount = 0;

    void Start()
    {

    }
    public void OnPointerDown()
    {
        buttonHold = true;
        Debug.Log("Mouse Entered in " + direction);
        if(holdCount < 90){
            switch(direction)
            {
                case MovementDirection.Up: playerMovement.MoveUp(); break;
                case MovementDirection.Down: playerMovement.MoveDown(); break;
                case MovementDirection.Left: playerMovement.MoveLeft(); break;
                case MovementDirection.Right: playerMovement.MoveRight(); break;
            }
        }
    }
    public void controlManager(){
        Debug.Log("Control Executed " + direction);
        switch (direction)
        {
            case MovementDirection.Up: playerMovement.MoveUp(); break;
            case MovementDirection.Down: playerMovement.MoveDown(); break;
            case MovementDirection.Left: playerMovement.MoveLeft(); break;
            case MovementDirection.Right: playerMovement.MoveRight(); break;
            case MovementDirection.Interact: playerData.playerInteract(); break;
        }
    }
    /*private void OnMouseDown()
    {
        Debug.Log("Mouse Down in " + direction);
        controlManager();
    }*/
    public void OnPointerUp()
    {
        buttonHold = false;
        playerMovement.Stop();
    }
    void Update(){
        if(Player != null){
            if(buttonHold){
                holdCount++;
                switch (direction){ case MovementDirection.Interact: playerData.playerHoldInteract(holdCount); break; }
            }else{
                if(holdCount != 0){
                    holdCount = 0;
                    switch (direction){ case MovementDirection.Interact: playerData.playerHoldInteract(holdCount); break; }
                }
            }
        }
    }

}
