using UnityEngine;
using UnityEngine.InputSystem;

public class TowerBuilding : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    private bool isOccupied;
    [SerializeField] private float radius = 4;

    private void Update()
    {
        PlaceTower();
    }

    private void PlaceTower()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var mousePos = Mouse.current.position.ReadValue();
            var cameraPos = Camera.main.ScreenToWorldPoint(mousePos);
            cameraPos.z = 0;
            float clickDistance = Vector3.Distance(cameraPos, transform.position);
            if (clickDistance <= radius)
            {
                if (isOccupied == true)
                {
                    Debug.Log("This spot is already occupied by tower");
                    return;
                }
                GameObject.Instantiate(towerPrefab, transform.position, Quaternion.identity);
                isOccupied = true;
            }
        }
    }
}