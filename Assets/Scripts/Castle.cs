using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Castle : MonoBehaviour
{
    [SerializeField] private GoldSystem goldSystem;
    [SerializeField] private EndScreen endScreen;
    [SerializeField] private int startHealth = 100;
    [SerializeField] private Transform healthBar;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private int upgradeCost = 10;
    private float fullBarScale;
    private Vector3 healthBarScale;
    private int health;
    private int maxHealth;
    private bool castleAlive = true;
    private float healthBarHealth;
    private bool upgradedInCurrentWave = false;
    private int castleLevel;
    private Vector3 onMapMouse;
    private SpriteRenderer castle;

    private void Start()
    {
        castle = GetComponent<SpriteRenderer>();
        fullBarScale = healthBar.localScale.y;
        healthBarScale = healthBar.localScale;
        health = startHealth;
        maxHealth = health;
        healthText.SetText($"Castle health: {health}");
    }

    private void Update()
    {
        WhereClicked();
    }

    public void ReceiveDamage(int damage)
    {
        if (castleAlive == false)
        {
            return;
        }
        health -= damage;
        UpdateHealthUi();
        ShowHealthBar();
        healthText.SetText($"Castle health: {health}");

        if (health <= 0)
        {
            endScreen.Lose();
            castleAlive = false;
        }
    }

    public bool IsAlive()
    {
        return castleAlive;
    }

    private void ShowHealthBar()
    {
        healthBarScale.y = healthBarHealth * fullBarScale;
        healthBar.localScale = healthBarScale;
    }

    private void WhereClicked()
    {
        var mouseClick = Mouse.current.leftButton.wasPressedThisFrame;
        if (mouseClick)
        {
            var mouseClickPos = Mouse.current.position.ReadValue();
            onMapMouse = Camera.main.ScreenToWorldPoint(mouseClickPos);
            onMapMouse.z = 0;
            ClickedOnCastle();
        }
    }

    private void ClickedOnCastle()
    {
        if (castle.bounds.Contains(onMapMouse))
        {
            UpgradeCastle();
        }
    }

    private void UpgradeCastle()
    {
        if (castleLevel == 3 || upgradedInCurrentWave == true || !goldSystem.TrySpend(upgradeCost))
        {
            return;
        }
        maxHealth += 25;
        health += 25;
        castleLevel++;
        upgradedInCurrentWave = true;
        UpdateHealthUi();
        ShowHealthBar();
        healthText.SetText($"Castle health: {health}");
    }

    private void UpdateHealthUi()
    {
        healthBarHealth = Mathf.Clamp01((float)health / maxHealth);
    }

    public void ResetUpgrade()
    {
        upgradedInCurrentWave = false;
    }
}