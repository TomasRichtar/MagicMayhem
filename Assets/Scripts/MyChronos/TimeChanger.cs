using Chronos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeChanger : MonoBehaviour
{
    public GlobalClock clock;
    public Text text;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            clock.localTimeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            clock.localTimeScale = -1;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            clock.localTimeScale = 1;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        text.text = clock.localTimeScale.ToString();
    }
}
