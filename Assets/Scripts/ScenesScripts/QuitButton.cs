using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
   void OnMouseDown()
	{
		Debug.Log ("QUIT!");
		Application.Quit();
	}
}
