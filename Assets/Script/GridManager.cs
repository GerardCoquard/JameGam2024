using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    
    [SerializeField] private Grid _grid;
    [SerializeField] private Transform _baseGroundHolder;
    [SerializeField] private Transform _collider;
    [SerializeField] private Transform _holders;
    [SerializeField] private GameObject _groundPrefab;
    private Dictionary<Tile, Transform> _holdersDictionary;
    public int gridWidth;
    public int gridDepth;

    private void Awake()
    {
        instance = this;
        GetHolders();
    }

    public void FillGridWithGround()
    {
        transform.position = new Vector3(-_grid.cellSize.x * gridWidth / 2, 0, -_grid.cellSize.z * gridDepth / 2);
        
        _collider.localPosition = new Vector3(_grid.cellSize.x * gridWidth / 2, -0.5f, _grid.cellSize.z * gridDepth / 2);
        _collider.localScale = new Vector3(gridWidth, 1f, gridDepth);
        
        Transform[] grounds = _baseGroundHolder.GetComponentsInChildren<Transform>();
        foreach (var t in grounds)
        {
            if(t == _baseGroundHolder) continue;
            if(!t.Equals(null)) DestroyImmediate(t.gameObject);
        }
        
        for (int i = 0; i < gridWidth; i++)
        {
            int x = (int)_grid.cellSize.x * i;
            for (int j = 0; j < gridDepth; j++)
            {
                int z = (int)_grid.cellSize.z * j;
                Vector3Int cellPos = _grid.LocalToCell(new Vector3(x, 0, z));
                Instantiate(_groundPrefab, _grid.GetCellCenterWorld(cellPos), Quaternion.identity, _baseGroundHolder);
            }
        }
    }

    public void Add(Vector3 pos, GameObject prefab, Tile tile)
    {
        if (!_holdersDictionary.ContainsKey(tile))
            CreateHolder(tile);
        Vector3Int cellPos = _grid.WorldToCell(pos);
        Instantiate(prefab, _grid.GetCellCenterWorld(cellPos), Quaternion.identity, _holdersDictionary[tile]);
    }

    public Vector3 WorldPosToGridPos(Vector3 pos)
    {
        return _grid.GetCellCenterWorld(_grid.WorldToCell(pos));
    }

    private void CreateHolder(Tile tile)
    {
        GameObject holder = new GameObject(tile.ToString());
        holder.transform.position = transform.position;
        holder.transform.SetParent(_holders);
        _holdersDictionary.Add(tile,holder.transform);
    }

    public void GetHolders()
    {
        _holdersDictionary = new Dictionary<Tile, Transform>();
        for (int i = 0; i < _holders.childCount; i++)
        {
            GameObject h = _holders.GetChild(i).gameObject;
            _holdersDictionary.Add((Tile) Enum.Parse(typeof(Tile), h.name, true),h.transform);
        }
    }
}

public enum Tile
{
    BaseGround,
    Ground,
    Wizard,
    Decoration
}
