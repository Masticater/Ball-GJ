using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LoadNewLevel : MonoBehaviour
{
    public int level; //Level to load
    public bool hasLevelNum = false; //Use the public number or not

    private void Start()
    {
        if(hasLevelNum)
            PlayerPrefs.SetInt("LastLevel", level);
        else
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
