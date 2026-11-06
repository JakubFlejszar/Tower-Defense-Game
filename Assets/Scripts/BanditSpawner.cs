using UnityEngine;

public class BanditSpawner : MonoBehaviour
{
    [SerializeField] private Bandit banditPrefab;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Castle castle;
    [SerializeField] private float[] spawnCooldownPerWave;
    [SerializeField] private int[] banditCountPerWave;
    [SerializeField] private EndScreen endScreen;
    [SerializeField] private WaveUi waveUi;

    private int currentWave;
    private float timeSinceLastSpawn;
    private float banditSpawningCooldown;
    private int banditCount;
    private bool playerWon;

    private void Start()
    {
        banditSpawningCooldown = spawnCooldownPerWave[currentWave];
        timeSinceLastSpawn = banditSpawningCooldown;
    }

    private void Update()
    {
        Spawning();
        Win();
    }

    private void Spawning()
    {
        if (banditCount == banditCountPerWave[currentWave])
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
        if (banditCount == banditCountPerWave[currentWave])
        {
            var bandits = FindObjectsByType<Bandit>();
            if (castle.IsAlive() && bandits.Length == 0)
            {
                if (currentWave == banditCountPerWave.Length - 1)
                {
                    endScreen.Win();
                    playerWon = true;
                }
                else
                {
                    currentWave++;
                    banditCount = 0;
                    banditSpawningCooldown = spawnCooldownPerWave[currentWave];
                    timeSinceLastSpawn = banditSpawningCooldown;
                    waveUi.WaveEnd();
                    castle.ResetUpgrade();
                }
            }
        }
    }
}