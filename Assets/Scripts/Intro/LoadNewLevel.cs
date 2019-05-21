using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LoadNewLevel : MonoBehaviour
{
    public int level;

    private void Start()
    {
        level = PlayerPrefs.GetInt("LastLevel");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            LoadLevel(level);

        if (Input.GetKeyDown(KeyCode.Escape))
            LoadLevel(0);
    }


    public void LoadLevel(int level)
    {
            SceneManager.LoadScene(level);
    }
}
