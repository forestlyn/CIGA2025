using CardHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 你打出的下一张技能牌面上的所有数值+3
/// </summary>
internal class Buff1 : Buff
{

    public MyCardDefType CardType = MyCardDefType.Skill;

    public override bool CheckCondition(MyCardData myCardDef)
    {
        if (myCardDef.CardType == CardType)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 计算卡牌效果
    /// </summary>
    /// <param name="myCardDef"></param>
    /// <returns></returns>
    public override Effect CalculateCardEffect(MyCardData myCardData, Effect effect)
    {
        var nowEffect = new Effect(myCardData.Effect);
        if (!CheckCondition(myCardData))
        {
            return nowEffect;
        }
        if (nowEffect.WorkDelta != 0)
            nowEffect.WorkDelta += 3;
        if (nowEffect.TirednessDelta != 0)
            nowEffect.TirednessDelta += 3;
        if (nowEffect.DrawCardCount != 0)
            nowEffect.DrawCardCount += 3;
        return nowEffect;
    }
    public override List<CurrencyQuantity> CalculateCardCost(MyCardData myCardData, List<CurrencyQuantity> costs)
    {
        var nowCosts = new List<CurrencyQuantity>(costs);
        return nowCosts;
    }

    /// <summary>
    /// buff通知:自己被添加到buff里面了
    /// 对于立即生效的buff，使用这个方法立刻生效
    /// </summary>
    internal override void Notify()
    {

    }


}

