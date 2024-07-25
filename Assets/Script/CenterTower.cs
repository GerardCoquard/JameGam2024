using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CenterTower : MonoBehaviour
{
    [SerializeField] private AmountDisplay _amountDisplay;
    [SerializeField] private int _health;
    [SerializeField] private int _segments;
    private int currentHealth;
    private bool gameOver;
    public static Action OnGameOver;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.PlayOneShot(audioSource.clip);
        _amountDisplay.InitializeAll(_health,0,0, _health, (float)_health/_segments);
        currentHealth = _health;
    }
    private void OnDestroy()
    {
        OnGameOver = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            TakeDamage(enemy.GetDamage());
            enemy.Die();
        }
    }

    private void TakeDamage(int amount)
    {
        int previousAmount = currentHealth;
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, _health);
        _amountDisplay.SetFillsFollow(currentHealth, previousAmount);
        if(!gameOver && currentHealth <= 0)
            GameOver();
    }

    private void GameOver()
    {
        OnGameOver?.Invoke();
        global::GameOver.instance.Activate();
    }
}
