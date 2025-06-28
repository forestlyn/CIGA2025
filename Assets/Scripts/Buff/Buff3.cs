using CardHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 你打出的下一张卡消耗疲劳值为0
/// </summary>
internal class Buff3 : Buff
{

    public override List<CurrencyQuantity> CalculateCardCost(MyCardData myCardData, List<CurrencyQuantity> costs)
    {
        var newCosts = new List<CurrencyQuantity>();
        foreach (var cost in costs)
        {
            newCosts.Add(new CurrencyQuantity { CurrencyType = cost.CurrencyType, Amount = 0 });
        }
        return newCosts;
    }

    public override Effect CalculateCardEffect(MyCardData myCardData, Effect effect)
    {
        return new Effect(effect);
    }

    public override bool CheckCondition(MyCardData myCardData)
    {
        return true;
    }

    internal override void Notify()
    {
    }
}
