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
            ActivateEffect();
            SkillManager.Instance.ActiveSkillGo(myCardData.SkillID);
        }
        else if (myCardData.CardType == MyCardDefType.Prop)
        {
            ApplyCost();
            ToolManager.Instance.CreateTool(myCardData.ToolID, this);
            GameManger.Instance.RemoveCard(GetComponent<Card>());
        }
        else if (myCardData.CardType == MyCardDefType.Item)
        {
            ApplyCost();
            ActivateEffect();
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

    public void ActivateEffect()
    {
        //Debug.Log($"Activating CardId: {myCardData.CardName}");
        if (myCardData == null)
        {
            Debug.LogError("MyCardDef is not set for PlayerCard.");
            return;
        }

        if (GameManger.Instance.StateType == Assets.Scripts.StateType.OnPlay)
        {
            MyLog.LogWithTime($"Card play effect. {GameManger.Instance.StateType}");
            var nowCardData = CalculateMyCardData();
            BuffManager.Instance.PlayCard(myCardData);
            PlayerManager.Instance.PlayCardEvent.Invoke(this, new EventPlayCardArgs(nowCardData));
            nowCardData.Effect.PlayEffect(myCardData);
            Debug.Log($"Card {nowCardData.CardName} activated with effect: {nowCardData.Effect.WorkDelta} Work, " +
                $"{nowCardData.Effect.TirednessDelta} Tiredness, Draw {nowCardData.Effect.DrawCardCount} Cards, Buff ID: {nowCardData.Effect.BuffID} " +
                $"{nowCardData.listOfCosts[0].CurrencyType.Name} {nowCardData.listOfCosts[0].Amount}");
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
