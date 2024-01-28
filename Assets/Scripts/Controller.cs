using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject UpButton;
    public GameObject DownButton;
    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject Player;
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = Player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {

    }
    private void FixedUpdate()
    {
        /*playerMovement.MoveUp(UpButton.GetComponent<ControllerBehavior>().IsPress);
        playerMovement.MoveDown(DownButton.GetComponent<ControllerBehavior>().IsPress);
        playerMovement.MoveRight(RightButton.GetComponent<ControllerBehavior>().IsPress);
        playerMovement.MoveLeft(LeftButton.GetComponent<ControllerBehavior>().IsPress); */
    }
}
