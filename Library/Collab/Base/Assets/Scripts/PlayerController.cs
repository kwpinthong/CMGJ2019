using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    Vector3 moveDistance;
    Rigidbody playerRigidbody;

    private int stage = 0;
    
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();   
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); //Input for left/right arrow

        moveDistance.Set (h, 0.0f, 0.0f);

        moveDistance = transform.right * h * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + moveDistance);

        degrees = degrees%360.0f;
        transform.DORotate(new Vector3( 0.0f, degrees, 0.0f), 1);

        if(Input.GetKeyDown(KeyCode.Space)){
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }


        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            if(!isOnCornor){
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                buttonCText.text = "C";
                headbuttonC.SetActive(true);
                //Debug.Log("Did Hit");
                if(Input.GetKeyDown(KeyCode.C)){
                    if(hit.collider.tag == "objective"){
                        //Debug.Log(hit.collider.tag);
                    }
                    
                    if(hit.collider.tag == "dialogue"){
                        //Debug.Log(hit.collider.tag);
                        headbuttonC.SetActive(false);
                        diaglogue.SetActive(true);
                        // if(Input.GetKeyDown(KeyCode.C)){
                        //     diaglogue.SetActive(false);
                        // }
                    }
                }

            }
        }
        else
        {
            headbuttonC.SetActive(false);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    
    }

    private bool isDialogueShow = false;
    public GameObject diaglogue;

    public GameObject headbutton;
    public GameObject headbuttonC;
    public Text buttonCText;
    public Text buttonText;
    private float degrees = 0.0f;
    private bool isOnCornor = false;
    void OnCollisionStay (Collision other){
        if(other.gameObject.CompareTag ("filppoint")){
            isOnCornor = true;
            headbutton.SetActive(true);
            stage = Checking(other.gameObject.name, stage);
        }
    }

    private int Checking(string currentpos, int stage){
        if(currentpos == "Filp_Point1" && stage == 0){
            buttonText.text = "X";
            if(Input.GetKeyDown(KeyCode.X)){
                degrees -= 90;
                stage = 1;
            }
        }
        if(currentpos == "Filp_Point1" && stage == 1){
            buttonText.text = "Z";
            if(Input.GetKeyDown(KeyCode.Z)){
                degrees += 90;
                stage = 0;
            }
        }
        //-------------------------------------------//
        if(currentpos == "Filp_Point4" && stage == 1){
            buttonText.text = "X";
            if(Input.GetKeyDown(KeyCode.X)){
                degrees -= 90;
                stage = 0;
            }
        }
        if(currentpos == "Filp_Point4" && stage == 0){
            buttonText.text = "Z";
            if(Input.GetKeyDown(KeyCode.Z)){
                degrees += 90;
                stage = 1;
            }
        }
        //-------------------------------------------//
        if(currentpos == "Filp_Point3" && stage == 0){
            buttonText.text = "X";
            if(Input.GetKeyDown(KeyCode.X)){
                degrees -= 90;
                stage = 1;
            }
        }
        if(currentpos == "Filp_Point3" && stage == 1){
            buttonText.text = "Z";
            if(Input.GetKeyDown(KeyCode.Z)){
                degrees += 90;
                stage = 0;
            }
        }
        //--------------------------------------------//
        if(currentpos == "Filp_Point2" && stage == 1){
            buttonText.text = "X";
            if(Input.GetKeyDown(KeyCode.X)){
                degrees -= 90;
                stage = 0;
            }
        }
        if(currentpos == "Filp_Point2" && stage == 0){
            buttonText.text = "Z";
            if(Input.GetKeyDown(KeyCode.Z)){
                degrees += 90;
                stage = 1;
            }
        }
        return stage;
    }
    void OnCollisionExit (Collision other){
        headbutton.SetActive(false);
        isOnCornor = false;
    }
}
