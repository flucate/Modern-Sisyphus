using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtraButton : MonoBehaviour
{
 void OnMouseDown()
	{
		SceneManager.LoadScene("Extra");
	}
}
