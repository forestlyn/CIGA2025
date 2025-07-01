using CardHouse;
using MyTools.MyEventSystem;
using System;
using UnityEngine;

public class PlayerCard : MonoBehaviour
{
    private MyCardData myCardData;

    /// <summary>
    /// 用于道具牌存储数值
    /// </summary>
    private MyCardData currentCardData;
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
        //Debug.Log($"Buff change detected for CardId: {myCardData.CardName}");
        var tempCardData = BuffManager.Instance.CalculateBuff(myCardData);
        GetComponent<MyCardSetup>().ReDraw(tempCardData);
        GetComponent<CurrencyCost>().UpdateCost(tempCardData);
    }

    public void Activate()
    {
        UIManager.Instance.SetDescriptionText(myCardData.CardDescription);

        if (myCardData.CardType == MyCardDefType.Skill)
        {
            ApplyCost();
            currentCardData = ApplyBuff();
            ApplyEffect();
            SkillManager.Instance.ActiveSkillGo(myCardData.SkillID);
        }
        else if (myCardData.CardType == MyCardDefType.Prop)
        {
            ApplyCost();
            currentCardData = ApplyBuff();
            ToolManager.Instance.CreateTool(myCardData.ToolID, this);
            GameManger.Instance.RemoveCard(GetComponent<Card>());
        }
        else if (myCardData.CardType == MyCardDefType.Item)
        {
            ApplyCost();
            currentCardData = ApplyBuff();
            ApplyEffect();
            GameManger.Instance.RemoveCard(GetComponent<Card>());
        }
        else
        {
            Debug.LogError($"Unknown CardId type: {myCardData.CardType} for CardId: {myCardData.CardName}");
        }
    }


    public void ApplyCost()
    {
        GetComponent<CurrencyCost>().Activate();
    }


    public MyCardData ApplyBuff()
    {
        if (GameManger.Instance.StateType == Assets.Scripts.StateType.OnPlay)
        {
            //MyLog.LogWithTime($"Card play effect. {GameManger.Instance.StateType}");
            var currentCardData = CalculateMyCardData();
            BuffManager.Instance.PlayCard(myCardData);
            PlayerManager.Instance.PlayCardEvent.Invoke(this, new EventPlayCardArgs(currentCardData));
            return currentCardData;
        }
        return null;
    }
    public void ApplyEffect()
    {
        //Debug.Log($"Activating CardId: {myCardData.CardName}");
        if (myCardData == null)
        {
            Debug.LogError("MyCardDef is not set for PlayerCard.");
            return;
        }

        if (GameManger.Instance.StateType == Assets.Scripts.StateType.OnPlay)
        {
            currentCardData.Effect.PlayEffect(currentCardData);
            Debug.Log($"Card {currentCardData.CardName} activated with effect: {currentCardData.Effect.WorkDelta} Work, " +
                $"{currentCardData.Effect.TirednessDelta} Tiredness, Draw {currentCardData.Effect.DrawCardCount} Cards, Buff ID: {currentCardData.Effect.BuffID} " +
                $"{currentCardData.listOfCosts[0].CurrencyType.Name} {currentCardData.listOfCosts[0].Amount}");
        }
    }



    /// <summary>
    /// 计算当前卡牌在当前buff下的效果
    /// </summary>
    /// <returns></returns>
    public MyCardData CalculateMyCardData()
    {
        var tempData = BuffManager.Instance.CalculateBuff(myCardData);
        return tempData;
    }
}
