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
    private bool playerWon;

    private void Start()
    {
        desiredBanditCount = startingDesiredBanditCount;
        banditSpawningCooldown = startingBanditSpawningCooldown;
        timeSinceLastSpawn = banditSpawningCooldown;
    }

    private void Update()
    {
        Spawning();
        Win();
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

    private void Win()
    {
        if (playerWon)
        {
            return;
        }
        if (banditCount == desiredBanditCount)
        {
            var bandits = FindObjectsByType<Bandit>();
            if (castle.IsAlive() && bandits.Length == 0)
            {
                Debug.Log("You won");
                playerWon = true;
            }
        }
    }
}