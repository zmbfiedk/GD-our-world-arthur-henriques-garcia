using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; 
    private float runspeed;
    public float jumpHeight = 5f; 
    public float groundDistance = 1.4f; 
    public LayerMask groundMask; 
    public Transform groundCheck; 
    public float radius = 0.5f;  

    private Rigidbody rb; 
    private bool isGrounded; 
    private float movementInputX;
    private float movementInputZ;

    private Vector3 wallNormal;
    public LayerMask mask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        runspeed = speed * 1.5f;
    }

    void Update()
    {
        movementInputX = Input.GetAxis("Horizontal");
        movementInputZ = Input.GetAxis("Vertical");

        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);
        Debug.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundDistance, Color.red);
       // Debug.Log($"Is Grounded: {isGrounded}");

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) //jump
        {
            Jump();
        }

        if (TouchWall() && Input.GetKeyDown(KeyCode.Space)) // walljump
        {
            Jump();
            Debug.Log("walljump");
        }

    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(movementInputX, 0f, movementInputZ);
        movement = transform.TransformDirection(movement);
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runspeed : speed;
        rb.velocity = new Vector3(movement.x * currentSpeed, rb.velocity.y, movement.z * currentSpeed);
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
       Debug.Log("Jumped");
    }

    bool TouchWall()
    {
        RaycastHit hit;
       
        bool detectForward = Physics.Raycast(new Ray(transform.position+Vector3.up, transform.forward), out RaycastHit wallCheck,groundDistance);
        Debug.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + transform.forward * groundDistance, Color.blue);

        if (detectForward && wallCheck.collider.CompareTag("Environment"))
        {
            Debug.Log($"Touching: {detectForward} {wallCheck.collider.tag}");

            return true;
        }
        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, groundDistance))
        {
            //Debug.Log($"Touching: {hit.collider.name}");
            wallNormal = hit.normal;
            return hit.collider.CompareTag("Environment");
        }
        return false;
    }
}
