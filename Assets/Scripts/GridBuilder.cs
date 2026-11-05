using UnityEngine;
using UnityEngine.InputSystem;

public class GridBuilder : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private Transform leftBottomCorner;
    [SerializeField] private Vector2Int mapSize = new Vector2Int(20, 20);
    private float tileSize = 4;

    private Vector3 mapCorner;
    private int xPoint;
    private int yPoint;
    private Vector3 towerPos;

    private bool[,] isOccupied;

    private void Start()
    {
        isOccupied = new bool[mapSize.x, mapSize.y];
        mapCorner = leftBottomCorner.position;
    }

    private void Update()
    {
        calculateSize();
    }

    private void calculateSize()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var mousePos = Mouse.current.position.ReadValue();
            var cameraPos = Camera.main.ScreenToWorldPoint(mousePos);
            cameraPos.z = 0;
            var sizeOfMap = cameraPos - mapCorner;
            float xSize = sizeOfMap.x / tileSize;
            float ySize = sizeOfMap.y / tileSize;
            xPoint = Mathf.FloorToInt(xSize);
            yPoint = Mathf.FloorToInt(ySize);
            if (xPoint < 0 || yPoint < 0 || xPoint >= mapSize.x || yPoint >= mapSize.y)
            {
                return;
            }
            if (isOccupied[xPoint, yPoint])
            {
                return;
            }
            TowerPosition();
        }
    }

    private void TowerPosition()
    {
        var towerPosX = xPoint * tileSize + tileSize / 2 + mapCorner.x;
        var towerPosY = yPoint * tileSize + tileSize / 2 + mapCorner.y;
        towerPos.x = towerPosX;
        towerPos.y = towerPosY;
        towerPos.z = 0;
        GameObject.Instantiate(towerPrefab, towerPos, Quaternion.identity);
        isOccupied[xPoint, yPoint] = true;
    }
}