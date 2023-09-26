using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 5f;

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
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessTranslation()
    {
        Vector2 inputVector = movement.ReadValue<Vector2>();
        float newX = Mathf.Clamp(transform.localPosition.x + inputVector.x * moveSpeed * Time.deltaTime, -xRange, xRange);
        float newY = Mathf.Clamp(transform.localPosition.y + inputVector.y * moveSpeed * Time.deltaTime, -yRange, yRange);

        transform.localPosition = new Vector3(
            newX,
            newY,
            transform.localPosition.z
        );

        // float horizontalThrow = Input.GetAxis("Horizontal");
        // float verticalThrow = Input.GetAxis("Vertical");
        // Debug.Log("H: " + horizontalThrow + "\nV: " + verticalThrow);
    }

    private void ProcessRotation()
    {
        transform.localRotation = Quaternion.Euler(-30f, 30f, 0f);
    }
}
