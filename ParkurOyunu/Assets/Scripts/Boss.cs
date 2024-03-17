using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    private Scene _scene;
    private float puan = 0f;
    private void Awake()
    {
        _scene = SceneManager.GetActiveScene(); //caching
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("OzelEsya"))
        {
            Destroy(other.gameObject);
            puan++;
        }
        if (puan == 3f)
        {
            SceneManager.LoadScene(_scene.buildIndex + 1); //þu anki sahnemden 1 sonraki sahneyi çaðýrýcam
        }
    }
}