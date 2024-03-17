using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cikis : MonoBehaviour
{
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
