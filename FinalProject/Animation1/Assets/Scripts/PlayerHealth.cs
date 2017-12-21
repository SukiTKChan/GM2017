using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//https://unity3d.com/learn/tutorials/topics/multiplayer-networking/player-health-single-player?playlist=29690

public class PlayerHealth : NetworkBehaviour
{
    public RectTransform healthBar;

    public const int maxHealth = 100;

    [SyncVar]
    public int currentHealth = maxHealth;

    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            return;
        }
        

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
        }

        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }
}
