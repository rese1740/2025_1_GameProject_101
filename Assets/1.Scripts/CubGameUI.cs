using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubGameUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = "생존시간" + timer;
    }
}
