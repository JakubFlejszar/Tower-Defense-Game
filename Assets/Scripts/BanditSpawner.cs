using UnityEngine;

public class BanditSpawner : MonoBehaviour
{
    [SerializeField] private Bandit banditPrefab;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Castle castle;
    [SerializeField] private float startingBanditSpawningCooldown = 3.5f;
    [SerializeField] private int startingDesiredBanditCount = 5;
    private float timeSinceLastSpawn;
    private int desiredBanditCount;
    private float banditSpawningCooldown;
    private int banditCount;

    private void Start()
    {
        timeSinceLastSpawn = banditSpawningCooldown;
        desiredBanditCount = startingDesiredBanditCount;
        banditSpawningCooldown = startingBanditSpawningCooldown;
    }

    private void Update()
    {
        Spawning();
    }

    private void Spawning()
    {
        if (banditCount == desiredBanditCount)
        {
            return;
        }
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= banditSpawningCooldown)
        {
            Bandit newBandit = Instantiate(banditPrefab);
            newBandit.Setup(castle, waypoints);
            banditCount++;
            timeSinceLastSpawn = 0;
        }
    }
}