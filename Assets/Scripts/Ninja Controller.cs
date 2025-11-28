using UnityEngine;

public class NinjaController : MonoBehaviour
{
    [Header("Master Toggles")]
    public bool enableRotation = true;
    public bool enableMovement = true;

    [Header("Rotation Settings")]
    public float rotationSpeed = 2f;
    public float changeDirectionTime = 2f;

    [Header("Movement Settings")]
    public float moveDuration = 2f;
    public float moveSpeed = 2f;
    public float minMoveDistance = 1f;
    public float maxMoveDistance = 3f;

    [Header("Timing")]
    public float rotateTimeBeforeMoving = 3f;

    private Quaternion targetRotation;
    private Vector3 targetPosition;

    private float timer;
    private bool isMoving = false;

    public bool isWalking = false;

   

    private void Start()
    {
        PickNewRotation();
        timer = rotateTimeBeforeMoving;
    }

    private void Update()
    {
        if (!isMoving && enableRotation)
        {
            HandleRotation();
        }
        else if (isMoving && enableMovement)
        {
            HandleMovement();
        }


    }

    // -----------------------------------
    // ROTATION
    // -----------------------------------
    private void HandleRotation()
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            StartMovement();
        }

        changeDirectionTime -= Time.deltaTime;
        if (changeDirectionTime <= 0f)
        {
            PickNewRotation();
        }

        

    }

    private void PickNewRotation()
    {
        changeDirectionTime = Random.Range(1f, 3f);
        float yRot = Random.Range(0f, 360f);
        targetRotation = Quaternion.Euler(0, yRot, 0);

        //toggle off walking in animator controller
        isWalking = false;
    }

    // -----------------------------------
    // MOVEMENT
    // -----------------------------------
    private void StartMovement()
    {
        if (!enableMovement)
        {
            timer = rotateTimeBeforeMoving;
            return;
        }

        isMoving = true;

        float randomDistance = Random.Range(minMoveDistance, maxMoveDistance);

        // ✔ Move forward in the direction we are facing
        Vector3 moveDirection = transform.forward;

        targetPosition = transform.position + moveDirection * randomDistance;

        timer = moveDuration;

        //toggle on walking in animator controller
        isWalking = true;
    }

    private void HandleMovement()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            Time.deltaTime * moveSpeed
        );

        timer -= Time.deltaTime;

        if (timer <= 0f || Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            timer = rotateTimeBeforeMoving;
            PickNewRotation();
        }
    }
}
