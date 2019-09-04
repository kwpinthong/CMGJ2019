using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public string LoadScene;

    Vector3 moveDistance;
    Rigidbody playerRigidbody;
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    public GameObject GrayPlane;
    public bool isGrounded;
    public int currentJump;
    public AudioSource audioPast;
    public AudioSource audioPresent;
    public GameObject pickupbutton;
    public Text pickupText;
    private const int MAX_Jump = 1;
    private KeyCode rotating = KeyCode.Z;
    private KeyCode Interactive = KeyCode.C;
    private Animator anim;
    private SpriteRenderer sprite;

    public AudioClip jump;
    public AudioClip pickup;
    private AudioSource source;

    public bool facingRight = true;
    public bool Controlable;
    void Start()
    {
        source = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private float degrees = 0.0f;
    private float rotate = -90;
    private bool notonBlackline = true;
    void Update()
    {
            float h = Input.GetAxisRaw("Horizontal"); //Input for left/right arrow

            moveDistance = transform.right * h * speed * Time.deltaTime;



            if (facingRight == false && h > 0)
            {

                Filp();


                //sprite.flipX = true;

                //transform.localScale = new Vector3(-transform.localScale.x , transform.localScale.y ,transform.localScale.z);
            }
            else if (facingRight == true && h < 0)
            {

                Filp();
                //sprite.flipX = false;
                //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            playerRigidbody.MovePosition(transform.position + moveDistance);


            anim.SetInteger("speed", (int)Mathf.Sign(h));
            anim.SetInteger("speed", (int)(h));


        //Rotating//
        if (notonBlackline && isGrounded)
        {
            if (Input.GetKeyDown(rotating))
            {
                transform.DOKill();
                degrees += rotate;
                rotate = -rotate;

                degrees = degrees % 360.0f;
                transform.DORotate(new Vector3(0.0f, degrees, 0.0f), 1);

            }
        }

        //Jumping//
        if (Input.GetKeyDown(KeyCode.X) && (isGrounded || MAX_Jump > currentJump))
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            currentJump++;
            anim.SetTrigger("jump");
            source.PlayOneShot(jump);
        }
        anim.SetBool("isGround",isGrounded);

        //GointoPasttime//
        if (transform.position.z > 0)
        {
            GrayPlane.SetActive(true);
        }
        else if (transform.position.z < 0)
        {
            GrayPlane.SetActive(false);
        }

        //Fading Music//
        if(transform.position.z >= -0.25 && transform.position.z <= 0.25){
            float posz = this.transform.position.z;
            audioPresent.volume = 1 - 2*(posz + 0.25f);
            audioPast.volume =  2*(posz + 0.25f);
        }
    }


    public GameObject cuttree;
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                anim.SetBool("grabbed", true);
                other.gameObject.transform.position = new Vector3(transform.position.x + 0.1f , transform.position.y + 0.1f, transform.position.z);
                other.gameObject.transform.parent = transform;
                source.PlayOneShot(pickup);
            }else{
                anim.SetBool("grabbed", false);
            }
        }

        if (other.gameObject.CompareTag("door"))
        {
            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {   //Door always move down//
                other.gameObject.transform.DOMoveY(-1.5f, 3, false);
            }
        }

        if (other.gameObject.CompareTag("tree"))
        {
            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {   
                GameObject.Destroy(other.gameObject);
                GameObject.Destroy(GameObject.Find("River"));
                cuttree.SetActive(true);
            }
        }

    }

   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("filppoint"))
        {
            notonBlackline = false;
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            notonBlackline = true;
        }

        isGrounded = true;
        currentJump = 0;

        if(collision.gameObject.CompareTag("winning")){
            SceneManager.LoadScene(LoadScene);
        }

        
        if (collision.gameObject.CompareTag("deatg"))
        {
            SceneManager.LoadScene("GameOver");
        }

    }

    private void OnCollisionExit (Collision collision){
        if(collision.gameObject.CompareTag("item") || collision.gameObject.CompareTag("door") || collision.gameObject.CompareTag("chocker")
           || collision.gameObject.CompareTag("bridgesapling") || collision.gameObject.CompareTag("key") || collision.gameObject.CompareTag("statue")  
           || collision.gameObject.CompareTag("woodenbox") || collision.gameObject.CompareTag("torch") || collision.gameObject.CompareTag("bridgetree")  
           || collision.gameObject.CompareTag("woodpile") || collision.gameObject.CompareTag("stonetable")){
            
            pickupbutton.SetActive(false);
        } 
    }

    public void Filp()
    {
        facingRight = !facingRight;
        Vector3 Scalar = transform.localScale;
        Scalar.x *= -1;
        transform.localScale = Scalar;
    }

}
