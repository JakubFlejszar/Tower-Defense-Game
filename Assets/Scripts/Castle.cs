using UnityEngine;
using UnityEngine.InputSystem;

public class Castle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int startHealth = 50;

    private int health;
    private bool castleAlive = true;

    private void Start()
    {
        health = startHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        if (castleAlive == true)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                int damage = 7;
                ReceiveDamage(damage);
                Debug.Log(health);
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
    }
}