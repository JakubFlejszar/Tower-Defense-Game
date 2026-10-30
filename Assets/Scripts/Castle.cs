using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Castle : MonoBehaviour
{
    [SerializeField] private EndScreen endScreen;
    [SerializeField] private int startHealth = 100;
    [SerializeField] private TMP_Text healthUi;
    [SerializeField] private Transform healthBar;
    private float fullBarScale;
    private Vector3 healthBarScale;
    private int health;
    private bool castleAlive = true;
    private float healthBarHealth;

    private void Start()
    {
        fullBarScale = healthBar.localScale.y;
        healthBarScale = healthBar.localScale;
        health = startHealth;
        healthUi.text = $"Castle HP: {health}";
    }

    private void Update()
    {
        if (castleAlive == true)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                int damage = 7;
                ReceiveDamage(damage);
            }
        }
    }

    public void ReceiveDamage(int damage)
    {
        if (castleAlive == false)
        {
            return;
        }
        health -= damage;
        healthBarHealth = Mathf.Clamp01((float)health / startHealth);
        ShowHealthBar();
        if (health <= 0)
        {
            endScreen.Lose();
            castleAlive = false;
        }
        healthUi.text = $"Castle HP: {health}";
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
}