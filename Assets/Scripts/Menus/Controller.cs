using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public Transform selector;
    public RawImage blackOut;
    public GameObject controls;
    bool curserLeft = true;
    bool curserMoved = false;
    bool selected = false;

    public float fadeSpeed = 0.8f;
    float alpha = 0.0f;
    int fadeDir = -1;

    private void Start()
    {
        Cursor.visible = false;
        PlayerPrefs.SetInt("TotalScore", 0); //Reset player score if they return to main menu after losing
    }

    private void Update()
    {
        if(curserMoved == false && selected == false)
        {
            if(Input.GetAxisRaw("Horizontal") != 0)
            {
                curserLeft = !curserLeft;
                SetCursorPosition();
                curserMoved = true;
            }
        }
        
        if (Input.GetAxisRaw("Horizontal") == 0)
            curserMoved = false;

        if(Input.GetAxisRaw("Submit") != 0)
        {
            selected = true;
            if (curserLeft)
            {
                BeginFade(1);
            }
            else
            {
                BeginFade(1);
            }
        }

        Fade();

        if(alpha == 1)
        {
            if (!curserLeft)
            {
                controls.SetActive(!controls.activeInHierarchy);
                if (!controls.activeInHierarchy)
                    selected = false;
                BeginFade(-1);
            }
            else
            {
                SceneManager.LoadScene(sceneBuildIndex: 1);
            }
        }
    }

    void Fade()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        blackOut.color = new Color(blackOut.color.r, blackOut.color.g, blackOut.color.b, alpha);
    }

    float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeDir);
    }

    void SetCursorPosition()
    {
        if (curserLeft)
            selector.position = new Vector2(60, 465);
        else
            selector.position = new Vector2(395, 465);
    }
}
