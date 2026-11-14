using TMPro;
using UnityEngine;

public class TowerMenu : MonoBehaviour
{
    [SerializeField] private GameObject towerMenu;

    private Tower chosenTower;

    private TMP_Dropdown priorityOptionsList;
    private int chosenPriority;

    private void Start()
    {
        towerMenu.SetActive(false);
    }

    public void Open(Tower tower)
    {
        chosenTower = tower;
        towerMenu.SetActive(true);
        Debug.Log(tower.transform.position);
    }

    public void Close()
    {
        towerMenu.SetActive(false);
        chosenTower = null;
    }

    public void UpgradeChosenTower()
    {
        if (chosenTower == null)
        {
            return;
        }
        chosenTower.UpgradeTower();
    }
}