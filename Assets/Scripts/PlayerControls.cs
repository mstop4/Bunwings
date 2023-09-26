using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = movement.ReadValue<Vector2>();
        transform.localPosition = new Vector3(
            transform.localPosition.x + inputVector.x * moveSpeed * Time.deltaTime,
            transform.localPosition.y + inputVector.y * moveSpeed * Time.deltaTime,
            transform.localPosition.z
        );

        // float horizontalThrow = Input.GetAxis("Horizontal");
        // float verticalThrow = Input.GetAxis("Vertical");
        // Debug.Log("H: " + horizontalThrow + "\nV: " + verticalThrow);
    }
}
