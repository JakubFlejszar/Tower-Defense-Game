using UnityEngine;

public class Bandit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform[] waypoints;

    [SerializeField] private Castle castle;
    [SerializeField] private int targetWaypoint;
    [SerializeField] private float startMovingSpeed = 2;
    [SerializeField] private int startingHealth = 50;
    [SerializeField] private int startingDamage = 10;
    [SerializeField] private float startingAttackCooldown = 1.5f;

    private float timeSinceLastAttack;
    private float attackCooldown;
    private int damage;
    private int health;
    private float movingSpeed;

    private void Start()
    {
        attackCooldown = startingAttackCooldown;
        timeSinceLastAttack = attackCooldown;
        damage = startingDamage;
        health = startingHealth;
        movingSpeed = startMovingSpeed;
        transform.position = waypoints[0].position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (targetWaypoint == waypoints.Length)
        {
            AttackCastle();
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

    private void AttackCastle()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack >= attackCooldown)
        {
            castle.ReceiveDamage(damage);
            timeSinceLastAttack = 0;
        }
    }
}