using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBarrierControl : MonoBehaviour
{
    public PlayerController player; //to access Player script
    public bool isBarrierActive;
    
    private float initialBarrierPosition;

    // Start is called before the first frame update
    void Start()
    {     
        player = GameObject.Find("Player")
                           .GetComponent<PlayerController>(); //to get player script component  
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBarrierActive && player.transform.position.x <= 3f)
        {  
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            isBarrierActive = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {    
        if (other.gameObject.tag == "playerTag" && IsPlayerHoldingSuitcase())
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            isBarrierActive = false;
        }        
    }

    private bool IsPlayerHoldingSuitcase()    
    {
        if (player.hasSuitcase)
        {
            return true;
        }
        
        return false;
    }
}
