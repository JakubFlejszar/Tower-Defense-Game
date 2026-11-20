using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private EndScreen endScreen;
    [SerializeField] private StartMenu startMenu;

    private void Start()
    {
        menu.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Show();
        }
    }

    public void Show()
    {
        if (endScreen.isEndScreenActive() == true || startMenu.isStartMenuActive() == true)
        {
            return;
        }
        menu.SetActive(!menu.activeSelf);
        if (menu.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void Hide()
    {
        menu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Hide();
    }

    public void Restart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}