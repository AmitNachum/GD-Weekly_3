using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component moves its object when the player clicks the arrow keys.
 */
public class InputMover: MonoBehaviour {
    [Tooltip("Speed of movement, in meters per second")]
    [SerializeField] float initialSpeed = 0.91f;
    [SerializeField] float gradualIncrease = 1.008f;
    [SerializeField] float currentSpeed = 0.91f;

    private bool isMoving = false;
   

    [SerializeField] InputAction move = new InputAction(
        type: InputActionType.Value, expectedControlType: nameof(Vector2));

    void OnEnable()  {
        move.Enable();
    }

    void OnDisable()  {
        move.Disable();
    }

    void Update() {

        if (move.IsPressed())
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            currentSpeed *= gradualIncrease;
        }
        else 
        {
            currentSpeed = initialSpeed;
        }
        Vector2 moveDirection = move.ReadValue<Vector2>();
        Vector3 movementVector = new Vector3(moveDirection.x, moveDirection.y, 0) * currentSpeed * Time.deltaTime;
        transform.position += movementVector;
        

        //transform.Translate(movementVector);
        // NOTE: "Translate(movementVector)" uses relative coordinates - 
        //       it moves the object in the coordinate system of the object itself.
        // In contrast, "transform.position += movementVector" would use absolute coordinates -
        //       it moves the object in the coordinate system of the world.
        // It makes a difference only if the object is rotated.
    }
}
