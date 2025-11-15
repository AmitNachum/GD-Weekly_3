using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component moves its object when the player clicks the arrow keys.
 */
public class InputMover : MonoBehaviour
{
    [Tooltip("Speed of movement, in meters per second")]
    [SerializeField] private float initialSpeed = 1.04f;
    [SerializeField] private float gradualIncrease = 1.05f;
    [SerializeField] private float currentSpeed = 1.04f;
    [SerializeField] private float maxSpeed = 10.0f;

    [SerializeField] private Rigidbody2D rb;

    private bool isMoving = false;

    [SerializeField]
    private InputAction move = new InputAction(
        type: InputActionType.Value,
        expectedControlType: nameof(Vector2));

    private void Start()
    {
        // If not assigned in the inspector, grab it from the same GameObject
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        currentSpeed = initialSpeed;
    }

    private void OnEnable()
    {
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    private void Update()
    {
        Vector2 moveDirection = move.ReadValue<Vector2>();

        // Are we actually pressing a movement key?
        isMoving = moveDirection.sqrMagnitude > 0.0001f;

       if(!isMoving)
        {
            currentSpeed = initialSpeed;
            rb.linearVelocity = Vector2.zero;
            return;
        }
        // ✔ Correct way with Rigidbody2D: velocity = direction * speed

        currentSpeed *= Mathf.Pow(gradualIncrease, Time.deltaTime);

        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);


        rb.linearVelocity = moveDirection.normalized * currentSpeed;


        // ------------------------
        // OLD TRANSFORM-BASED VERSION (kept for the NOTE)
        // ------------------------
        /*
        Vector3 movementVector =
            new Vector3(moveDirection.x, moveDirection.y, 0) * currentSpeed * Time.deltaTime;

        transform.position += movementVector;
        // transform.Translate(movementVector);
        // NOTE: "Translate(movementVector)" uses relative coordinates -
        //       it moves the object in the coordinate system of the object itself.
        // In contrast, "transform.position += movementVector" would use absolute coordinates -
        //       it moves the object in the coordinate system of the world.
        // It makes a difference only if the object is rotated.
        */
    }
}
