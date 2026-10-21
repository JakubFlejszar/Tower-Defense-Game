using UnityEngine;
using UnityEngine.InputSystem;

public class BanditSpawner : MonoBehaviour
{
    [SerializeField] private Bandit banditPrefab;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Castle castle;

    private void Start()
    {
    }

    private void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Bandit newBandit = Instantiate(banditPrefab);
            newBandit.Setup(castle, waypoints);
        }
    }
}