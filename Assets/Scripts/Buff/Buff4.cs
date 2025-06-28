using CardHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 你打出的下一张非用品牌面上的所有数值翻倍
/// </summary>
internal class Buff4 : Buff
{
    public override List<CurrencyQuantity> CalculateCardCost(MyCardData myCardData, List<CurrencyQuantity> costs)
    {
        var nowCosts = new List<CurrencyQuantity>(costs);
        return nowCosts;
    }

    public override Effect CalculateCardEffect(MyCardData myCardData, Effect effect)
    {
        var nowEffect = new Effect(effect);
        if (!CheckCondition(myCardData))
        {
            return nowEffect;
        }
        if (nowEffect.WorkDelta != 0)
            nowEffect.WorkDelta *= 2;
        if (nowEffect.TirednessDelta != 0)
            nowEffect.TirednessDelta *= 2;
        if (nowEffect.DrawCardCount != 0)
            nowEffect.DrawCardCount *= 2;
        return nowEffect;
    }

    public override bool CheckCondition(MyCardData myCardData)
    {
        if(myCardData.CardType != MyCardDefType.Item)
        {
            return true;
        }
        return false;
    }

    internal override void Notify()
    {
    }
}

