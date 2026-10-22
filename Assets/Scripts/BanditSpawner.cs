using UnityEngine;

public class BanditSpawner : MonoBehaviour
{
    [SerializeField] private Bandit banditPrefab;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Castle castle;
    [SerializeField] private float startingBanditSpawningFrequency = 3.5f;
    [SerializeField] private int startingDesiredBanditCount = 5;
    private float lastBanditSpawn;
    private int desiredBanditCount;
    private float banditSpawningFrequency;
    private int banditCount;

    private void Start()
    {
        desiredBanditCount = startingDesiredBanditCount;
        banditSpawningFrequency = startingBanditSpawningFrequency;
    }

    private void Update()
    {
        Spawning();
    }

    public void Spawning()
    {
        if (banditCount == desiredBanditCount)
        {
            return;
        }
        lastBanditSpawn += Time.deltaTime;
        if (lastBanditSpawn >= banditSpawningFrequency)
        {
            Bandit newBandit = Instantiate(banditPrefab);
            newBandit.Setup(castle, waypoints);
            banditCount++;
            lastBanditSpawn = 0;
        }
    }
}