using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float playTime;
    [SerializeField] private TMP_Text gameTimeText;

    private void Start()
    {
    }

    private void Update()
    {
        PlayTime();
    }

    private void PlayTime()
    {
        playTime += Time.deltaTime;
    }

    public float ReturnPlayTime()
    {
        return playTime;
    }
}