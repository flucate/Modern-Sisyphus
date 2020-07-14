using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Record : MonoBehaviour
{

    //Attributes

    //public Text distanceUI;
    public Text recordUI;
    
    // Recorde da Main Camera
    private float maxDistance = 0;

    private SuitcaseController suitcase;

    // Start is called before the first frame update
    void Start()
    {
        suitcase = GameObject.Find("Player").GetComponent<SuitcaseController>();
    }

    // Update is called once per frame
    void Update()
    {
        maxDistance = suitcase.CalculateTravelDistance();      

        if(maxDistance > PlayerPrefs.GetInt("txtRecord"))
        {
            PlayerPrefs.SetInt("txtRecord", (int)maxDistance);
        }

        recordUI.text = "Record: " + PlayerPrefs.GetInt("txtRecord");
    }
}
