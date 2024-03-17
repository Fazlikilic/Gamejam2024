using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private Scene _scene;
    private void Awake()
    {
        _scene = SceneManager.GetActiveScene(); //caching
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(_scene.buildIndex + 1); //þu anki sahnemden 1 sonraki sahneyi çaðýrýcam
        }
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(_scene.buildIndex + 1);
    }
}
