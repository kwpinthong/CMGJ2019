using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingPlayer : MonoBehaviour
{
    public Transform target;
    public int whichone;
    //public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(whichone == 1){
            transform.position = new Vector3(transform.position.x, transform.position.y, target.transform.position.z); // z
        }else if (whichone == 0) {
            transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
        }


    }
}
