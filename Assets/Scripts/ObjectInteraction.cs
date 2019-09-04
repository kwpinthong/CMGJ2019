using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ObjectInteraction : MonoBehaviour
{
    private Rigidbody rb = new Rigidbody();
    private GameObject pickupPrompt;
    public GameObject button;
    public Text text;
    private Text ActualPickupPrompt;
    public GameObject realatedObject;
    public GameObject realatedGamoObject1;
    public string CompareTagName; 
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    [SerializeField]private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")&&this.gameObject.CompareTag("choker"))
        {

            button.SetActive(true);
            text.text = "C";

            //do something about realatedGamoObject
            if(Input.GetKeyDown(KeyCode.C)&&this.gameObject.CompareTag("choker")){
                Debug.Log("C");
                realatedObject.transform.DOMoveY(5.5f,2.5f,false);
                Destroy(realatedGamoObject1);
            }

        }

        if(collision.gameObject.CompareTag("Player")&&this.gameObject.CompareTag("pushablebox")){
            button.SetActive(true);
            text.text = "C";
           // rb = GetComponent<Rigidbody>();
            bool canbepush = true;
            
            if(Input.GetKeyDown(KeyCode.C) && this.gameObject.CompareTag("pushablebox")&&canbepush == true){
                    transform.Translate(Vector3.right*Time.deltaTime*30);
                    //this.transform.DOMove(Vector3.left*Time.deltaTime*100,1.5f,false);
                
            }
        }
    }

    /* private void OnCollisionStay(){
        if(Input.GetKeyDown(KeyCode.E)){
            Destroy(gameObject);
        }
    }*/

    private void OnCollisionExit(Collision colision){
            button.SetActive(false);
    }
}
