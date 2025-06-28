using CardHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 弃掉你的手牌，抽取5张牌
/// </summary>
internal class Buff2 : Buff
{

    public override List<CurrencyQuantity> CalculateCardCost(MyCardData myCardData, List<CurrencyQuantity> costs)
    {
        var nowCosts = new List<CurrencyQuantity>(costs);
        return nowCosts;
    }

    public override Effect CalculateCardEffect(MyCardData myCardData, Effect effect)
    {
        return effect;
    }


    public override bool CheckCondition(MyCardData myCardData)
    {
        return false;
    }

    internal override void Notify()
    {
        GameManger.Instance.ClearCardHand();
        GameManger.Instance.DrawCard(5);
        MyLog.LogWithTime($"Buff2 activated: Discarding hand and drawing 5 cardIds.");
        BuffManager.Instance.RemoveBuff(this);
    }
}

