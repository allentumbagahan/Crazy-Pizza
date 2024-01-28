using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform objectToFollow; 

    private void Update()
    {
        if (objectToFollow != null)
        {
            transform.position = new Vector3(objectToFollow.position.x, objectToFollow.position.y, transform.position.z);
        }
    }
}
