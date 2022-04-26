using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTime : MonoBehaviour
{
    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        RemainingTime.RemainingTimeSeconds -= Time.deltaTime;
        float minutes = Mathf.FloorToInt(RemainingTime.RemainingTimeSeconds / 60);
        float seconds = Mathf.FloorToInt(RemainingTime.RemainingTimeSeconds % 60);
        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
