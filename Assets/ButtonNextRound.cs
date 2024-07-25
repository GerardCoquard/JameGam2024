using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNextRound : MonoBehaviour
{
    [SerializeField] GameObject button;
    private void Update()
    {
        if(FindObjectOfType<Enemy>() == null)
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
        }
    }
    public void ButtonActive()
    {
        FindObjectOfType<WaveManager>().SpawnWave();
    }
}
