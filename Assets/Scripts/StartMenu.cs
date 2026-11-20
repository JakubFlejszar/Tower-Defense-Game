using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    private bool isActive;
    void Start()
    {
        isActive = true;
        Time.timeScale = 0;
        startMenu.SetActive(true);
    }

    void Update()
    {
    }
    public void Play()
    {
        startMenu.SetActive(false);
        isActive = false;
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
    public bool isStartMenuActive()
    {
        return isActive;
    }
}