using UnityEngine;

public class ArmSwingMovementSystem : MonoBehaviour
{
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    [SerializeField] GameObject CenterEyeCamera;
    [SerializeField] float baseSpeed = 5f;

    private Vector3 LeftHandPrevPos;
    private Vector3 rightHandPrevPos;
    private Vector3 PlayerPrevPos;
    private float handSpeed;

    private MovementBehaviour MB;
    void Start()
    {
        MB = GetComponent<MovementBehaviour>();
        PlayerPrevPos = Vector3.up * transform.position.y;
        LeftHandPrevPos = Vector3.up * leftHand.transform.position.y;
        rightHandPrevPos = Vector3.up * rightHand.transform.position.y;
    }

    void Update()
    {
        float playerMovedDist = Vector3.Distance(PlayerPrevPos, transform.position);
        float leftHandMovedDist = Vector3.Distance(LeftHandPrevPos, leftHand.transform.position);
        float rightHandMovedDist = Vector3.Distance(rightHandPrevPos, rightHand.transform.position);

        handSpeed = 0;

        if (leftHandMovedDist > 0.2f || rightHandMovedDist > 0.2f)
        {
            handSpeed = (leftHandMovedDist - playerMovedDist) + (rightHandMovedDist - playerMovedDist);
        }

        PlayerPrevPos = Vector3.up * transform.position.y;
        LeftHandPrevPos = Vector3.up * leftHand.transform.position.y;
        rightHandPrevPos = Vector3.up * rightHand.transform.position.y;
    }

    private void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad > 1f && handSpeed != 0)
        {
            MB.MoveRB3D(handSpeed + baseSpeed, CenterEyeCamera.transform.forward);
        }
    }
}