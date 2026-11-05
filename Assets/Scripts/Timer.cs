using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float playTime;

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