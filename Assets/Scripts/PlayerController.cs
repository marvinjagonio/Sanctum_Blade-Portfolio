using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    public InputActionAsset InputActions;

    private InputAction p_moveAction;
    private Vector2 p_moveAmt;
    private Vector3 velocity;
    private CharacterController p_Controller;

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float rotateSpeed = 2f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private bool shouldFaceMoveDirection = false;
    [SerializeField] Transform cameraTransform;

    private void OnEnable()
    {
        InputActions.FindActionMap("Gameplay").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Gameplay").Disable();
    }

    private void Awake()
    {
        p_moveAction = InputSystem.actions.FindAction("Move");
    }
    void Start()
    {
        p_Controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {

        p_moveAmt = p_moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = Vector3.zero;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        moveDirection = forward * p_moveAmt.y + right * p_moveAmt.x;

        p_Controller.Move(moveDirection * walkSpeed * Time.deltaTime);

        if (shouldFaceMoveDirection && moveDirection.sqrMagnitude > 0.001f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        p_Controller.Move(velocity * Time.deltaTime);
    }
}
