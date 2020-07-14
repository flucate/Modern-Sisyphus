using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFollow : MonoBehaviour
{

    //attributes
    public Transform player; //to receive player object


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //add to the ground position the player position on x axis
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
