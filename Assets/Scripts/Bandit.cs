using UnityEngine;

public class Bandit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform[] waypoints;

    [SerializeField] private int targetWaypoint;
    [SerializeField] private float startMovingSpeed = 2;
    private float movingSpeed;
    private void Start()
    {
        movingSpeed = startMovingSpeed;
        transform.position = waypoints[0].position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (targetWaypoint == waypoints.Length)
        {
            return;
        }

        MovingTowards();

        if (Vector3.Distance(transform.position, waypoints[targetWaypoint].position) <= 0.1)
        {
            targetWaypoint++;
            if (targetWaypoint == waypoints.Length)
            {
                Debug.Log("Castle reached");
            }
        }
    }

    private void MovingTowards()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[targetWaypoint].position, maxDistanceDelta: movingSpeed * Time.deltaTime);
    }
}