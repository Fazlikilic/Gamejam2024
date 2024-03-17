using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class OzelEsyaToplama : MonoBehaviour
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
            puan = 1f;
        }
        if (other.gameObject.CompareTag("NextLevel") && puan == 1f)
        {
            SceneManager.LoadScene(_scene.buildIndex + 1); //þu anki sahnemden 1 sonraki sahneyi çaðýrýcam
            puan = 0f;
        }
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(_scene.buildIndex + 1);
    }

}
