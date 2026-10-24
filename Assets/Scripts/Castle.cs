using UnityEngine;
using UnityEngine.InputSystem;

public class Castle : MonoBehaviour
{
    [SerializeField] private int startHealth = 50;

    private int health;
    private bool castleAlive = true;
    private bool isAlive;

    private void Start()
    {
        health = startHealth;
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
        Debug.Log(health);
    }

    public bool IsAlive()
    {
        if (health >= 1)
        {
            return isAlive = true;
        }

        return isAlive = false;
    }
}