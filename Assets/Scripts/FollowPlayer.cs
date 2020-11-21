using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform playerTransform;
    public Vector3 cameraShift;

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + cameraShift;
    }
}
