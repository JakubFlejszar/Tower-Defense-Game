using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    [SerializeField] private GameObject towerMenu;
    [SerializeField] private Tower tower;

    private TMP_Dropdown priorityOptionsList;
    [SerializeField] private Button upgradeButton;
    private Tower chosenTower;
    private int chosenPriority;

    private void Start()
    {
        towerMenu.SetActive(false);
    }

    private void Update()
    {
    }

    private void Menu()
    {
    }
}