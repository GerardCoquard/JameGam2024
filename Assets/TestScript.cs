using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.OnGridClick += Spawn;
    }

    private void Spawn(Vector3 pos, string layer)
    {
        if (layer != "Ground") return;
        GridManager.instance.Add(pos,prefab,Tile.Wizard);
    }
}
