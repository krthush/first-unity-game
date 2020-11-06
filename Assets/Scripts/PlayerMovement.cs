using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform t;
    [SerializeField] private BoxCollider bC;
    [SerializeField] private LayerMask enviromentLayerMask;

    [SerializeField] private TimeManager timeManager;

    [SerializeField] private float forwardForce = 2000f;
    [SerializeField] private float sidewaysForce = 500f;
    [SerializeField] private float jumpForce = 2.0f;
    [SerializeField] private float jumpHeightOffset = 3f;
    [SerializeField] private float fallMultiplier = 2f;
    [SerializeField] private float returnRotationScale = 1f;
    [SerializeField] private float gameOverHeight = -1f;

    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = t.position;
    }

    // Check if player is grounded by drawing box from player downwards to enviroment and check for intersection
    private bool isGrounded()
    {
        bool boxCastHit = Physics.BoxCast(bC.bounds.center, bC.bounds.size, Vector3.down, Quaternion.identity, jumpHeightOffset, enviromentLayerMask);
        return !boxCastHit;
    }

    private void Update()
    {
        if (Input.GetKey("space"))
        {
            timeManager.StartSlowMotion();
        }
        else
        {
            timeManager.StopSlowMotion();
        }

        if ((Input.GetKeyDown("w") || Input.GetKeyDown("up")) && isGrounded())
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            // Reset rotation incase of turning during jumps
            t.rotation = Quaternion.identity;
            rb.freezeRotation = true;
        } else
        {
            rb.freezeRotation = false;
        }

        // Modify jump to have faster fall
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        // Reset rotation incase of turning during jumps
        if (isGrounded())
        {
            t.rotation = Quaternion.identity;
        }

        if (rb.position.y < gameOverHeight)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
 
    }
}
