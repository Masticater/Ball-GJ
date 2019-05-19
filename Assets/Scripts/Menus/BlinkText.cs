using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        StartCoroutine(Blink());
    }

   IEnumerator Blink()
    {
        while (true)
        {
            text.enabled = false;
            yield return new WaitForSeconds(2f);
            text.enabled = true;
            yield return new WaitForSeconds(1.5f);
        }
    }
}
