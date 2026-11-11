using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text gameTime;

    [SerializeField] private GameObject screenTint;
    [SerializeField] private TMP_Text finalText;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Timer timer;
    [SerializeField] private Scoreboard scoreboard;

    private void Start()
    {
        screenTint.SetActive(false);
        finalText.SetText("");
        playAgainButton.onClick.AddListener(PlayAgain);
    }

    //private void Update()
    //{
    //}

    public void Lose()
    {
        Time.timeScale = 0;
        finalText.SetText("Defeated");
        screenTint.SetActive(true);
        gameTime.SetText($"Time:{TimeSpan.FromSeconds(timer.ReturnPlayTime()).ToString(@"mm\:ss")}");
    }

    public void Win()
    {
        Time.timeScale = 0;
        finalText.SetText("Victory");
        gameTime.SetText($"Time:{TimeSpan.FromSeconds(timer.ReturnPlayTime()).ToString(@"mm\:ss")}");
        screenTint.SetActive(true);
        scoreboard.AddTimes(timer.ReturnPlayTime());
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}