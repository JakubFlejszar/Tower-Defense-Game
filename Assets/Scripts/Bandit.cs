using UnityEngine;

public class Bandit : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform healthBar;
    [SerializeField] private Castle castle;
    [SerializeField] private int targetWaypoint;
    [SerializeField] private float startMovingSpeed = 2;
    [SerializeField] private int startingHealth = 50;
    [SerializeField] private int startingAttackDamage = 10;
    [SerializeField] private float startingAttackCooldown = 1.5f;
    private float healthBarWidth;
    private float timeSinceLastAttack;
    private float attackCooldown;
    private int damage;
    private int health;
    private float movingSpeed;
    private Vector3 healthBarScale;
    private float healthBarFragment;

    private void Start()
    {
        healthBarWidth = healthBar.localScale.x;
        healthBarScale = healthBar.localScale;
        attackCooldown = startingAttackCooldown;
        timeSinceLastAttack = attackCooldown;
        damage = startingAttackDamage;
        health = startingHealth;
        movingSpeed = startMovingSpeed;
        transform.position = waypoints[0].position;
    }

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

    public void ReceiveDamage(int receivedDamage)
    {
        health -= receivedDamage;
        healthBarFragment = Mathf.Clamp01((float)health / startingHealth);
        UpdateHealthBar();
        if (health <= 0)
        {
            Debug.Log("Bandit's dead");
            Destroy(gameObject);
            return;
        }

        Debug.Log($"Bandit {health}");
    }

    public void Setup(Castle setupCastle, Transform[] setupWaypoints)
    {
        waypoints = setupWaypoints;
        castle = setupCastle;
    }

    public void UpdateHealthBar()
    {
        healthBarScale.x = healthBarFragment;
        healthBar.localScale = healthBarScale;
    }

    public int CurrentHealth()
    {
        return health;
    }
}