using UnityEngine;
using UnityEngine.InputSystem;

public class Tower : MonoBehaviour
{
    [SerializeField] private float startingShootingCooldown = 1;
    [SerializeField] private float startingRange = 1;
    [SerializeField] private int startingDamage = 5;
    [SerializeField] private TowerArrow towerArrow;
    [SerializeField] private AudioSource arrowSoundSource;
    [SerializeField] private AudioClip arrowSound;

    private enum PriorityOption
    { ClosestBandit, MostHpBandit, LeastHpBandit }

    [SerializeField] private PriorityOption selectedPriority;
    private float shootingCooldown;
    private float timeSinceLastShot;
    private float range;
    private int damage;
    private Vector3 onMapMouse;
    private SpriteRenderer towerSprite;
    private bool upgradedThisWave;
    private int towerLevel = 0;

    private void Start()
    {
        timeSinceLastShot = startingShootingCooldown;
        shootingCooldown = startingShootingCooldown;
        range = startingRange;
        damage = startingDamage;
        towerSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Shooting();
        MouseClick();
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
            arrowSoundSource.PlayOneShot(arrowSound);
            timeSinceLastShot = 0;
        }
    }

    private Bandit LocateTarget()
    {
        Bandit target = null;
        int compareHpLeast = int.MaxValue;
        int compareHpMost = int.MinValue;
        float targetDistance = range;
        var banditsLoaded = FindObjectsByType<Bandit>();
        foreach (Bandit bandit in banditsLoaded)
        {
            float banditDistanceFromTower = Vector3.Distance(transform.position, bandit.transform.position);
            if (banditDistanceFromTower >= range)
            {
                continue;
            }
            switch (selectedPriority)
            {
                case PriorityOption.ClosestBandit:

                    if (banditDistanceFromTower < targetDistance)
                    {
                        target = bandit;
                        targetDistance = banditDistanceFromTower;
                    }
                    break;

                case PriorityOption.MostHpBandit:
                    if (bandit.CurrentHealth() >= compareHpMost)
                    {
                        target = bandit;
                        compareHpMost = target.CurrentHealth();
                    }
                    break;

                case PriorityOption.LeastHpBandit:
                    if (bandit.CurrentHealth() <= compareHpLeast)
                    {
                        target = bandit;
                        compareHpLeast = target.CurrentHealth();
                    }
                    break;
            }
        }
        return target;
    }

    private void MouseClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var mousePos = Mouse.current.position.ReadValue();
            onMapMouse = Camera.main.ScreenToWorldPoint(mousePos);
            onMapMouse.z = 0;
            if (WasPressedOnTower())
            {
                UpgradeTower();
                //towerMenu.OpenMenu();
            }
        }
    }

    private bool WasPressedOnTower()
    {
        if (towerSprite.bounds.Contains(onMapMouse))
        {
            return true;
        }
        return false;
    }

    public void UpgradeTower()
    {
        if (upgradedThisWave == true || towerLevel == 3)
        {
            return;
        }
        damage += 5;
        towerLevel++;
        Debug.Log($"Tower Upgraded - damage: {damage}");
        upgradedThisWave = true;
    }

    public void ResetUpgrade()
    {
        upgradedThisWave = false;
    }
}