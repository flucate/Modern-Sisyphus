using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    //Attributes
    private Vector2 screenBounds;
    private SpawnerObstacle scSpawnerObstacle;
    public Transform player;
    
    private BoxCollider2D bc2d;

    // Start is called before the first frame update
    void Start()
    {
        //get camera bounds size
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        //get script component from Spawner gameobject
        scSpawnerObstacle = GameObject.Find("Spawner").GetComponent<SpawnerObstacle>();
        player = GameObject.Find("Player").GetComponent<Transform>();
                 
    }

    // Update is called once per frame
    void Update()
    {   
        if (scSpawnerObstacle.obstacleCleaner.transform.position.x > transform.position.x)
        {
            scSpawnerObstacle.countObjects--;
            Destroy(this.gameObject);
        }
    }
}
