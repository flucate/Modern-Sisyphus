using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    [SerializeField] private float secondsToSkip = 30f;

    private void FixedUpdate()
    {
        if (Time.time >= secondsToSkip)
        {
            LoadGameScene();
        }
    }

    private void OnMouseDown()
    {
        LoadGameScene();
    }

    private void LoadGameScene() => SceneManager.LoadScene("Game");
}
