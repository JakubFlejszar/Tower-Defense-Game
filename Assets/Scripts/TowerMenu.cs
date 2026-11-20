using TMPro;
using UnityEngine;

public class TowerMenu : MonoBehaviour
{
    [SerializeField] private GameObject towerMenu;

    private Tower chosenTower;

    [SerializeField] private TMP_Dropdown priorityOptionsList;

    private void Start()
    {
        towerMenu.SetActive(false);
        priorityOptionsList.onValueChanged.AddListener(OnPriorityChanged);
    }

    public void Open(Tower tower)
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        chosenTower = tower;
        priorityOptionsList.SetValueWithoutNotify(chosenTower.ReturnPriority());
        towerMenu.SetActive(true);
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

    private void OnPriorityChanged(int priorityId)
    {
        if (chosenTower == null)
        {
            return;
        }
        chosenTower.SetPriority(priorityId);
    }
}