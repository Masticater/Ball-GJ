using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LoadNewLevel : MonoBehaviour
{
    public int level;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            LoadLevel(level);
    }


    public void LoadLevel(int level)
    {
        if(EditorApplication.isPlaying)
            SceneManager.LoadScene(level);
    }
}
