using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Grid _grid;
    public GameObject fillPrefab;

    public int gridWidth;
    public int gridHeight;
    // Start is called before the first frame update
    void Start()
    {
        _grid = GetComponent<Grid>();
        FillGrid();
    }

    private void FillGrid()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            int x = (int)_grid.cellSize.x * i;
            for (int j = 0; j < gridHeight; j++)
            {
                int z = (int)_grid.cellSize.z * j;
                Vector3Int cellPos = _grid.WorldToCell(new Vector3(x, 0, z));
                Instantiate(fillPrefab, _grid.GetCellCenterWorld(cellPos), Quaternion.identity);
            }
        }
    }
    
    public void Add(Vector3 pos, GameObject prefab)
    {
        Vector3Int cellPos = _grid.WorldToCell(pos);
        Instantiate(prefab, _grid.GetCellCenterWorld(cellPos), Quaternion.identity);
    }
}
