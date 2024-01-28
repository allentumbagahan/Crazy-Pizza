
using UnityEngine;

public class CustomerBehaviorAndData : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Animator animator;
    public MovementController2D customerMovementPathfinding;
    GameObject objCentralTemp;
    ObjectLister objListerTemp;
    public GameObject targetObject;
    public bool isExit = false;
    public bool isStuck = true;
    public Vector2 targetingPosition;
    public string targetDirection;
    public bool isRequirementsMeet = false;

    public bool isOnTarget = false;
    bool isOnTargetInitialize = false;
    public bool isSit = false;
    public bool isSitInitialize = false;
    public bool isEating = false;
    public ObjectCollsionBehaivior objectCollsionBehaivior;
    public string OnIdleDirection; // direction of customer when stops in any object
    public ChatBoxFunction chatBoxFunction; //attach
    public CoinParticles coinParticles;


    void Start()
    {
        coinParticles = gameObject.GetComponent<CoinParticles>();
        customerMovementPathfinding = gameObject.GetComponent<MovementController2D>();
        objCentralTemp = GameObject.FindWithTag("ObjectLister");
        objListerTemp = objCentralTemp.GetComponent<ObjectLister>();
        animator = gameObject.GetComponent<Animator>();
        objectCollsionBehaivior = GetComponent<ObjectCollsionBehaivior>();
        objectCollsionBehaivior.executeWhenReceived += whenCompleted;
    }

    // Update is called once per frame
    
    void Update(){
        if(targetObject.GetComponent<CustomerLineUp>() != null && chatBoxFunction != null){
            if(isOnTarget){
                chatBoxFunction.showChatBox = targetObject.GetComponent<CustomerLineUp>().isSingleLinePoint;
            }
            if(isRequirementsMeet){
                chatBoxFunction.showChatBox = false;
            }
        }
        if(targetDirection != customerMovementPathfinding.direction && !isSit){
            targetDirection = customerMovementPathfinding.direction;
            StopAnimation(targetDirection);
        }

        
        if(isOnTarget && !isOnTargetInitialize)
        {
            isOnTargetInitialize = true;
            customerMovementPathfinding.targetPos = gameObject.transform.position;
        }
        if(isSit && !isSitInitialize){
            isSitInitialize = true;
            StopAll();
            SitIdle(OnIdleDirection);
        }
        if(customerMovementPathfinding.pathLeftToGo.Count > 0 && isStuck){
            isStuck = false;
        }
        if(customerMovementPathfinding.pathLeftToGo.Count == 0 && customerMovementPathfinding.targetPos == new Vector2(transform.position.x, transform.position.y) && !isExit && !isSit){
            isOnTarget = true;
            StopAnimation("");
            StopAndIdle(OnIdleDirection);
            if(isRequirementsMeet){
                //Generate coins
                coinParticles.coinsTotalAmount = 18;
                coinParticles.isStart = true;

                ExitCustomer();
            }
        }
        if(customerMovementPathfinding.pathLeftToGo.Count == 0 && customerMovementPathfinding.targetPos == new Vector2(transform.position.x, transform.position.y) && isExit){
            Destroy(gameObject);
        }
        if(customerMovementPathfinding.pathLeftToGo.Count == 0 && customerMovementPathfinding.targetPos != new Vector2(transform.position.x, transform.position.y) && isStuck){
            Destroy(gameObject);
        }
    }
    void ExitCustomer(){
        CustomerLineUp customerInPoint = targetObject.GetComponent<CustomerLineUp>();
        customerInPoint.inLineCustomer = null;
        GameObject randomExit = objListerTemp.customerExit[Random.Range(0, objListerTemp.customerExit.Count)];
        customerMovementPathfinding.targetPos = new Vector2(randomExit.transform.position.x, randomExit.transform.position.y-3);  
        isExit = true;
    }
    public void StopAnimation(string Exception)
    {
        animator.SetBool("isMoveUp", (Exception == "Up"));
        animator.SetBool("isMoveDown", (Exception == "Down"));
        animator.SetBool("isMoveRight", (Exception == "Right"));
        animator.SetBool("isMoveLeft", (Exception == "Left"));
    }
    public void StopAndIdle(string DirectionToIDle){
        animator.SetBool("isIdleUp", (DirectionToIDle == "Up"));
        animator.SetBool("isIdleDown", (DirectionToIDle == "Down"));
        animator.SetBool("isIdleRight", (DirectionToIDle == "Right"));
        animator.SetBool("isIdleLeft", (DirectionToIDle == "Left"));
    }
    public void SitIdle(string DirectionToIDle){
        animator.SetBool("isSitIdle", (DirectionToIDle == "Down"));
        animator.SetBool("isSitRightIdle", (DirectionToIDle == "Right"));
        animator.SetBool("isSitLeftIdle", (DirectionToIDle == "Left"));
    }
    public void SitEating(string DirectionToIDle){
        animator.SetBool("isEating", (DirectionToIDle == "Down"));
        animator.SetBool("isEatingRight", (DirectionToIDle == "Right"));
        animator.SetBool("isEatingLeft", (DirectionToIDle == "Left"));
    }
    public void StopAll(){
        animator.SetBool("isMoveUp", false);
        animator.SetBool("isMoveDown",  false);
        animator.SetBool("isMoveRight", false);
        animator.SetBool("isMoveLeft", false);
        animator.SetBool("isIdleUp", false);
        animator.SetBool("isIdleDown", false);
        animator.SetBool("isIdleRight", false);
        animator.SetBool("isIdleLeft", false);
        animator.SetBool("isSitIdle", false);
        animator.SetBool("isSitRightIdle", false);
        animator.SetBool("isSitLeftIdle", false);
        animator.SetBool("isEating", false);
        animator.SetBool("isEatingRight", false);
        animator.SetBool("isEatingLeft", false);
    }
    void whenCompleted(){
        isRequirementsMeet = true;
    }
}
