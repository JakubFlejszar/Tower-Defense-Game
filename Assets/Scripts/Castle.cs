using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Castle : MonoBehaviour
{
    [SerializeField] private int startHealth = 100;
    [SerializeField] private TMP_Text healthUi;

    private int health;
    private bool castleAlive = true;

    private void Start()
    {
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
        if (health <= 0)
        {
            Debug.Log("You've lost");
            castleAlive = false;
        }
        healthUi.text = $"Castle HP: {health}";
    }

    public bool IsAlive()
    {
        return castleAlive;
    }
}