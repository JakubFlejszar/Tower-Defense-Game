using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreboardText;
    private List<float> times = new List<float>();

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void addTimes(float time)
    {
        times.Add((time));
        times.Sort();
        if (times.Count > 5)
        {
            times.RemoveAt(5);
        }
        showScoreboard();
    }

    private void showScoreboard()
    {
        string text = "";
        int i = 1;
        foreach (float time in times)
        {
            text += $"{i}. {TimeSpan.FromSeconds(time).ToString(@"mm\:ss")}\n";
            i++;
        }
        scoreboardText.SetText(text);
    }
}