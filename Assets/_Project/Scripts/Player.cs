using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSmoothTime = 0.1f;

    Rigidbody rb;
    float horizontal;
    float vertical;
    float currentVelocity;
    Vector3 input;

    public bool IsMoving => input != Vector3.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        input = new Vector3(horizontal, 0, vertical);

        if(input != Vector3.zero)
        {
            input.Normalize();
            rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);

            float targetAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, rotationSmoothTime);

            rb.MoveRotation(Quaternion.Euler(0f, smoothAngle, 0f));
        }
    }
}
