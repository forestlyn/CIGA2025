using CardHouse;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    public int BuffID { get; private set; }
    public string BuffName { get; private set; }
    public string BuffDescription { get; private set; }

    /// <summary>
    /// 使用牌时调用，自己计算是否被使用了
    /// </summary>
    /// <param name="myCardDef"></param>
    public virtual bool Apply(MyCardData myCardData)
    {
        if (!CheckCondition(myCardData))
        {
            return false;
        }
        Activate(myCardData);
        if(IsExpired(myCardData))
        {
            OnExpired();
        }
        return true;
    }

    public abstract bool CheckCondition(MyCardData myCardData);

    public abstract Effect CalculateCardEffect(MyCardData myCardData, Effect effect);
    public abstract List<CurrencyQuantity> CalculateCardCost(MyCardData myCardData, List<CurrencyQuantity> costs);
    public virtual bool IsExpired(MyCardData myCardData)
    {
        return true;
    }
    public virtual void Activate(MyCardData myCardData)
    {

    }

    public virtual void OnExpired()
    {
        BuffManager.Instance.RemoveBuff(this);
    }

    internal abstract void Notify();
}