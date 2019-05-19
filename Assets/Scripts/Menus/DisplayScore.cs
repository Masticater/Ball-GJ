using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;
    void Awake()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        text.text = "Final Score - " + PlayerPrefs.GetInt("TotalScore");
        PlayerPrefs.DeleteAll();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
