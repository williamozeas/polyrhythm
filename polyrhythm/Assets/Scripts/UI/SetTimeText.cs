using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTimeText : MonoBehaviour
{
    
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        GameManager.EndGame += OnGameEnd;
    }

    public void OnGameEnd() {
        text.text = TimeFormatter(GameManager.i.time);
    }

    string TimeFormatter( float seconds, bool forceHHMMSS = false)
    {
        float secondsRemainder = Mathf.Floor( (seconds % 60) * 100) / 100.0f;
        int minutes = ((int)(seconds / 60)) % 60;
        int hours = (int)(seconds / 3600);
 
        if (!forceHHMMSS)
        {
            if (hours == 0)
            {
                return System.String.Format ("{0:00}:{1:00.00}", minutes, secondsRemainder);
            }
        }
        return System.String.Format ("{0}:{1:00}:{2:00}", hours, minutes, secondsRemainder);
    }
}
