using CardHouse;
using MyTools.MyEventSystem;
using System;
using UnityEngine;

public class PlayerCard : MonoBehaviour
{
    private MyCardData myCardData;

    internal void SetMyCardData(MyCardData myCard)
    {
        myCardData = myCard;
        GetComponent<MyCardSetup>().ReDraw(myCard);
    }

    internal MyCardData GetMyCardData()
    {
        return myCardData;
    }

    internal void Init()
    {
        BuffManager.Instance.BuffChange -= OnBuffChange;
        BuffManager.Instance.BuffChange += OnBuffChange;
    }

    private void OnBuffChange(object sender, EventArgs e)
    {
        Debug.Log($"Buff change detected for CardId: {myCardData.CardName}");
        var tempCardData = BuffManager.Instance.CalculateBuff(myCardData);
        GetComponent<MyCardSetup>().ReDraw(tempCardData);
        GetComponent<CurrencyCost>().UpdateCost(tempCardData);
    }

    public void Activate()
    {
        if (myCardData.CardType == MyCardDefType.Skill)
        {
            ActivateEffect();
        }
        else if (myCardData.CardType == MyCardDefType.Prop)
        {
            ToolManager.Instance.CreateTool(0, this);            
        }
        else if (myCardData.CardType == MyCardDefType.Item)
        {
            ActivateEffect();
        }
        else
        {
            Debug.LogError($"Unknown CardId type: {myCardData.CardType} for CardId: {myCardData.CardName}");
        }
    }

    public void ActivateEffect()
    {
        Debug.Log($"Activating CardId: {myCardData.CardName}");
        if (myCardData == null)
        {
            Debug.LogError("MyCardDef is not set for PlayerCard.");
            return;
        }

        if (GameManger.Instance.StateType == Assets.Scripts.StateType.OnPlay)
        {
            MyLog.LogWithTime($"Card play effect. {GameManger.Instance.StateType}");
            var nowEffect = CalculateMyEffect();
            BuffManager.Instance.PlayCard(myCardData);
            nowEffect.PlayEffect();
            Debug.Log($"Card {myCardData.CardName} activated with effect: {nowEffect.WorkDelta} Work, {nowEffect.TirednessDelta} Tiredness, Draw {nowEffect.DrawCardCount} Cards, Buff ID: {nowEffect.BuffID}");
        }
    }

    /// <summary>
    /// 计算当前卡牌在当前buff下的效果
    /// </summary>
    /// <returns></returns>
    public Effect CalculateMyEffect()
    {
        var tempData = BuffManager.Instance.CalculateBuff(myCardData);
        Effect effect = new Effect(tempData.Effect);
        return effect;
    }
}
