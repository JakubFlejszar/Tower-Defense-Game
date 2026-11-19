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
    [SerializeField] private AudioSource audioSourceAttack;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private int goldForKill = 2;
    private Animator animator;
    private SpriteRenderer banditSprite;
    private GoldSystem goldSystem;

    private float healthBarWidth;
    private float timeSinceLastAttack;
    private float attackCooldown;
    private int damage;
    private int health;
    private float movingSpeed;
    private Vector3 healthBarScale;
    private float healthBarFragment;
    private bool isDead;

    private void Start()
    {
        animator = GetComponent<Animator>();
        healthBarWidth = healthBar.localScale.x;
        healthBarScale = healthBar.localScale;
        attackCooldown = startingAttackCooldown;
        timeSinceLastAttack = attackCooldown;
        damage = startingAttackDamage;
        health = startingHealth;
        movingSpeed = startMovingSpeed;
        transform.position = waypoints[0].position;
        banditSprite = GetComponent<SpriteRenderer>();
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
        banditSprite.flipX = !UpdateDirection();
    }

    private void AttackCastle()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack >= attackCooldown)
        {
            timeSinceLastAttack = 0;
            animator.SetTrigger("Attack");
        }
    }

    public void ReceiveDamage(int receivedDamage)
    {
        if (isDead)
        {
            return;
        }
        health -= receivedDamage;
        healthBarFragment = Mathf.Clamp01((float)health / startingHealth);
        UpdateHealthBar();

        if (health <= 0)
        {
            isDead = true;

            Destroy(gameObject);
            goldSystem.AddGold(goldForKill);
            Debug.Log("Bandit's dead");
            return;
        }

        Debug.Log($"Bandit {health}");
    }

    public void Setup(Castle setupCastle, Transform[] setupWaypoints, GoldSystem gold)
    {
        goldSystem = gold;
        waypoints = setupWaypoints;
        castle = setupCastle;
    }

    public void UpdateHealthBar()
    {
        healthBarScale.x = healthBarWidth * healthBarFragment;
        healthBar.localScale = healthBarScale;
    }

    public int CurrentHealth()
    {
        return health;
    }

    public bool UpdateDirection()
    {
        return transform.position.x < waypoints[targetWaypoint].position.x;
    }
    public void PlayAttackSound()
    {
        castle.ReceiveDamage(damage);
        audioSourceAttack.PlayOneShot(attackSound);
    }
}