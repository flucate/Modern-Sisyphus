using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    //attributes

    //Transform player;
    public float parallaxEffect; //to configure how much effect

    private float length, startPosition; //for the sprite
    public GameObject cam;
   
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x; //get player initial position on X axis

        length = GetComponent<SpriteRenderer>().bounds.size.x;        
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundParallax();
    }

    void BackgroundParallax()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect); //how far we travelled
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);
        
        if (temp > (startPosition + length)) startPosition += length;
        else if (temp < (startPosition - length)) startPosition -= length;
    }
}
