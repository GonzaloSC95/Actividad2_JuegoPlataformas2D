using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void exitGame() {
        Application.Quit();
    }
}
