using System;
using UnityEngine;

/// <summary>
/// 每当你使用增加疲劳值为0的卡牌后，碎片块数增加5
/// </summary>
internal class Item5 : Item
{
    public override void Active()
    {
        PlayerManager.Instance.PlayCardEvent += OnPlayCard;
    }

    private void OnPlayCard(object sender, EventArgs e)
    {
        if (e is EventPlayCardArgs playCardArgs)
        {
            foreach (var cost in playCardArgs.Card.listOfCosts)
            {
                Debug.Log($"{cost.CurrencyType.Name}:{cost.Amount}");
                if (cost.CurrencyType.Name == "Tired" && cost.Amount == 0)
                {
                    GameManger.Instance.AddWork(5);
                }
            }
        }
    }

    public override void InActive()
    {
        PlayerManager.Instance.PlayCardEvent -= OnPlayCard;
    }
}

