using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointItem : MonoBehaviour
{
    public string needItem;
    public GameObject player;
    public GameObject pickupbutton;
    private Animator animator;

    public GameObject tree;

    void Start(){
        animator = player.gameObject.GetComponent<Animator>();
    }
    
    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            if(collision.gameObject.transform.Find(needItem)){
                GameObject.Destroy(collision.gameObject.transform.Find(needItem).gameObject);
                animator.SetBool("grabbed",false);
                pickupbutton.SetActive(false);
            }

            if(collision.gameObject.transform.Find(needItem) && needItem == "Bridge_sapling"){
                tree.SetActive(true);
                GameObject.Destroy(collision.gameObject.transform.Find(needItem).gameObject);
                animator.SetBool("grabbed",false);
                pickupbutton.SetActive(false);
            }

            if(collision.gameObject.transform.Find(needItem)&& this.gameObject.CompareTag("Wall")){
                Destroy(this.gameObject);
                GameObject.Destroy(collision.gameObject.transform.Find(needItem).gameObject);
            }


        }

    }
}
