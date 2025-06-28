using CardHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 你打出的下一张不增加疲劳值，而是增加相同数量的碎片块数
/// </summary>
internal class Buff5 : Buff
{
    public override List<CurrencyQuantity> CalculateCardCost(MyCardData myCardData, List<CurrencyQuantity> costs)
    {
        var newCosts = new List<CurrencyQuantity>();
        foreach (var cost in costs)
        {
            if (cost.CurrencyType.Name == "Tired")
                newCosts.Add(new CurrencyQuantity { CurrencyType = cost.CurrencyType, Amount = 0 });
            else
            {
                newCosts.Add(new CurrencyQuantity { CurrencyType = cost.CurrencyType, Amount = cost.Amount });
            }
        }
        return newCosts;
    }

    public override Effect CalculateCardEffect(MyCardData myCardData, Effect effect)
    {
        var newEffect = new Effect(effect);
        newEffect.WorkDelta += myCardData.listOfCosts
            .Where(c => c.CurrencyType.Name == "Tired")
            .Sum(c => c.Amount);
        return newEffect;
    }

    public override bool CheckCondition(MyCardData myCardData)
    {
        return true;
    }

    internal override void Notify()
    {

    }
}

