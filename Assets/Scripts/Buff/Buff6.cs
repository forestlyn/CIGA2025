using CardHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

/// <summary>
/// 使你手中所有牌的疲劳值增加量减少3
/// </summary>
internal class Buff6 : Buff
{
    private List<int> cardIds = new List<int>();
    public override List<CurrencyQuantity> CalculateCardCost(MyCardData myCardData, List<CurrencyQuantity> costs)
    {
        var newCosts = new List<CurrencyQuantity>();
        foreach (var cost in costs)
        {
            if (cost.CurrencyType.Name == "Tired")
                newCosts.Add(new CurrencyQuantity { CurrencyType = cost.CurrencyType, Amount = math.max(cost.Amount - 3, 0) });
            else
            {
                newCosts.Add(new CurrencyQuantity { CurrencyType = cost.CurrencyType, Amount = cost.Amount });
            }
        }
        return newCosts;
    }



    public override void Activate(MyCardData myCardData)
    {
        base.Activate(myCardData);
        cardIds.Remove(myCardData.CardId);
        UnityEngine.Debug.Log($"Buff6: Activated for CardId {myCardData.CardId}. Remaining cardIds: {string.Join(", ", cardIds)}.");    
        BuffManager.Instance.BuffChange.Invoke(this, EventArgs.Empty);
    }

    public override bool IsExpired(MyCardData myCardData)
    {
        return cardIds.Count == 0;
    }

    public override Effect CalculateCardEffect(MyCardData myCardData, Effect effect)
    {
        return effect;
    }

    public override bool CheckCondition(MyCardData myCardData)
    {
        bool result = cardIds.Any(card => card == myCardData.CardId);
        UnityEngine.Debug.Log($"Buff6: CheckCondition for CardId {myCardData.CardId} returned {result}.");
        return result;
    }

    internal override void Notify()
    {
        cardIds.Clear();
        foreach (var card in GameManger.Instance.CardHand.MountedCards.ToArray())
        {
            cardIds.Add(card.GetComponent<MyCardSetup>().CardId);
            UnityEngine.Debug.Log($"Buff6: Adding CardId {card.gameObject.name} to buff.");
        }
    }
}

