using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject screenTint;
    [SerializeField] private TMP_Text finalText;
    [SerializeField] private Button playAgainButton;

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
    }

    public void Win()
    {
        Time.timeScale = 0;
        finalText.SetText("Victory");
        screenTint.SetActive(true);
    }
    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}