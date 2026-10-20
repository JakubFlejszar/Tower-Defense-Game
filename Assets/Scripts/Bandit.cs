using UnityEngine;

public class Bandit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int targetWaypoint;

    void Start()
    {
        transform.position = waypoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
    }
}