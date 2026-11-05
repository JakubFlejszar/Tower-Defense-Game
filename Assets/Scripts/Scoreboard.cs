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
        GetTimes();
        ShowScoreboard();
    }

    private void Update()
    {
    }

    public void AddTimes(float time)
    {
        times.Add((time));
        times.Sort();
        if (times.Count > 5)
        {
            times.RemoveAt(5);
        }
        SaveTimes();

        ShowScoreboard();
    }

    private void ShowScoreboard()
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

    private void SaveTimes()
    {
        int i = 1;
        foreach (float time in times)
        {
            PlayerPrefs.SetFloat($"time{i}", time);
            i++;
        }
        PlayerPrefs.SetInt("timeCount", times.Count);
        PlayerPrefs.Save();
    }

    private void GetTimes()
    {
        var timeCount = PlayerPrefs.GetInt("timeCount");
        int i = 1;
        while (i <= timeCount)
        {
            times.Add(PlayerPrefs.GetFloat($"time{i}"));
            i++;
        }
    }
}