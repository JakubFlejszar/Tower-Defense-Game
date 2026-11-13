using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    [SerializeField] private GameObject towerMenu;
    [SerializeField] private Tower tower;

    private TMP_Dropdown priorityOptionsList;
    [SerializeField] private Button upgradeButton;
    private int chosenPriority;
    private Vector3 onMapMouse;
    private SpriteRenderer towerSprite;

    private void Start()
    {
        towerMenu.SetActive(false);
        towerSprite = tower.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            openMenu();
        }
    }

    private void Menu()
    {
    }
    public void openMenu()
    {
        towerMenu.SetActive(!towerMenu.activeSelf);
    }
}