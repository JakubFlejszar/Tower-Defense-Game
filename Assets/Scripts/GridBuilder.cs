using UnityEngine;
using UnityEngine.InputSystem;

public class GridBuilder : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private Transform leftBottomCorner;
    [SerializeField] private SpriteRenderer[] path;
    [SerializeField] private SpriteRenderer castle;
    [SerializeField] private Vector2Int mapSize = new Vector2Int(20, 20);
    [SerializeField] private SpriteRenderer previewTower;
    private float tileSize = 4;

    private Vector3 mapCorner;
    private int tileX;
    private int tileY;
    private Vector3 towerPos;
    private Vector2 mousePos;

    private bool[,] isOccupied;

    private void Start()
    {
        isOccupied = new bool[mapSize.x, mapSize.y];
        mapCorner = leftBottomCorner.position;
        MarkBlockedTiles();
    }

    private void Update()
    {
        MousePosition();
        UpdatePreview();
        HandleClick();
    }

    private void HandleClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!IsInsideMap())
            {
                return;
            }
            if (isOccupied[tileX, tileY])
            {
                return;
            }
            PlaceTower();
        }
    }

    private void PlaceTower()
    {
        towerPos = GetTileCenter(tileX, tileY);
        GameObject.Instantiate(towerPrefab, towerPos, Quaternion.identity);
        isOccupied[tileX, tileY] = true;
    }

    private Vector3 GetTileCenter(int tileX, int tileY)
    {
        Vector3 center;
        var posX = tileX * tileSize + tileSize / 2 + mapCorner.x;
        var posY = tileY * tileSize + tileSize / 2 + mapCorner.y;
        center.x = posX;
        center.y = posY;
        center.z = 0;
        return center;
    }

    private void MarkBlockedTiles()
    {
        int x = 0;
        int y = 0;
        while (x < mapSize.x)
        {
            y = 0;
            while (y < mapSize.y)
            {
                var tileCenter = GetTileCenter(x, y);
                foreach (SpriteRenderer pathFragment in path)
                {
                    if (pathFragment.bounds.Contains(tileCenter))
                    {
                        isOccupied[x, y] = true;
                    }
                }
                if (castle.bounds.Contains(tileCenter))
                {
                    isOccupied[x, y] = true;
                }

                y++;
            }
            x++;
        }
    }

    private void MousePosition()
    {
        mousePos = Mouse.current.position.ReadValue();
        var worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        worldMousePos.z = 0;
        var offsetFromCorner = worldMousePos - mapCorner;
        float tileExactX = offsetFromCorner.x / tileSize;
        float tileExactY = offsetFromCorner.y / tileSize;
        tileX = Mathf.FloorToInt(tileExactX);
        tileY = Mathf.FloorToInt(tileExactY);
    }

    private bool IsInsideMap()
    {
        if (tileX < 0 || tileY < 0 || tileX >= mapSize.x || tileY >= mapSize.y)
        {
            return false;
        }
        return true;
    }

    private void UpdatePreview()
    {
        var tileCenter = GetTileCenter(tileX, tileY);
        previewTower.transform.position = tileCenter;
        if (!IsInsideMap())
        {
            previewTower.color = new Color(r: 1, g: 0, b: 0, a: 0.5f);
            return;
        }
        if (IsInsideMap())
        {
            if (isOccupied[tileX, tileY])
            {
                previewTower.color = new Color(r: 1, g: 0, b: 0, a: 0.5f);
            }
            else
            {
                previewTower.color = new Color(r: 0, g: 1, b: 0, a: 0.7f);
            }
        }
    }
}