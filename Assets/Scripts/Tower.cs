using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float startingShootingCooldown = 1;
    [SerializeField] private float startingRange = 1;
    [SerializeField] private int startingDamage = 5;
    [SerializeField] private TowerArrow towerArrow;

    private float shootingCooldown;
    private float timeSinceLastShot;
    private float range;
    private int damage;

    private void Start()
    {
        timeSinceLastShot = startingShootingCooldown;
        shootingCooldown = startingShootingCooldown;
        range = startingRange;
        damage = startingDamage;
    }

    private void Update()
    {
        Shooting();
    }

    private void Shooting()
    {
        Bandit target = LocateTarget();
        if (target == null)
        {
            return;
        }
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= shootingCooldown)
        {
            TowerArrow newTowerArrow = Instantiate(towerArrow, transform.position, Quaternion.identity);
            newTowerArrow.Setup(damage, target);
            timeSinceLastShot = 0;
        }
    }

    private Bandit LocateTarget()
    {
        Bandit closestTarget = null;
        float closestTargetDistance = range;
        var banditsLoaded = FindObjectsByType<Bandit>();
        foreach (Bandit bandit in banditsLoaded)
        {
            float banditDistanceFromTower = Vector3.Distance(transform.position, bandit.transform.position);
            if (banditDistanceFromTower < closestTargetDistance)
            {
                closestTarget = bandit;
                closestTargetDistance = banditDistanceFromTower;
            }
        }
        return closestTarget;
    }
}