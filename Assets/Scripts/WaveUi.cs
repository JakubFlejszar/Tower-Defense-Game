using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveUi : MonoBehaviour
{
    [SerializeField] private GameObject screenTintWave;
    [SerializeField] private TMP_Text nextWaveButtonText;
    [SerializeField] private Button nextWaveButton;

    private void Start()
    {
        screenTintWave.SetActive(false);
        nextWaveButton.onClick.AddListener(NextWave);
    }

    //private void Update()
    //{
    //}

    public void WaveEnd()
    {
        Time.timeScale = 0;
        screenTintWave.SetActive(true);
    }

    public void NextWave()
    {
        screenTintWave.SetActive(false);
        Time.timeScale = 1;
    }
}