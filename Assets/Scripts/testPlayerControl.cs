using UnityEngine;

public class testPlayerControl : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    private float moveInput;

    private Rigidbody playerRigidbody;

    public int jumpSpeed;

    public bool isGrounded = false;
    private const int MAX_Jump = 2;
    private int currentHump = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded||MAX_Jump > currentHump))
        {
            playerRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
            currentHump++;
            Debug.Log(isGrounded);
            Debug.Log(currentHump);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        currentHump = 0;
    }
}
