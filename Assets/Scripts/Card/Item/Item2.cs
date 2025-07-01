using System;
using UnityEngine;

/// <summary>
/// 每次增加疲劳值后，疲劳值降低1
/// </summary>
internal class Item2 : Item
{
    

    public override void Active()
    {
        PlayerManager.Instance.TirednessChange += OnTirednessChange;
    }

    private void OnTirednessChange(object sender, EventArgs e)
    {
        if (e is EventTirednessChangeArgs tirednessChangeArgs)
        {
            Debug.Log($"Tiredness change detected: {tirednessChangeArgs.TirednessChange}");
            if (tirednessChangeArgs.TirednessChange > 0)
            {
                Debug.Log($"Reducing PlayerTiredness by 1 due to Item2 effect. Current Tiredness: {PlayerManager.Instance.PlayerTiredness}");
                PlayerManager.Instance.PlayerTiredness = Math.Max(0, PlayerManager.Instance.PlayerTiredness - 1);
            }
        }
    }
    public override void InActive()
    {
        PlayerManager.Instance.TirednessChange -= OnTirednessChange;
    }
}

