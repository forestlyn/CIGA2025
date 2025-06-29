using CardHouse;
using NUnit.Framework;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyCardData
{
    public MyCardDefType CardType;
    public string CardName;
    public string CardDescription;
    public Effect Effect;
    public int UseTimes;
    public CurrencyQuantity[] listOfCosts;
    public int CardId;
    internal int ToolID;
    public int SkillID;

    public MyCardData(MyCardDef myCardData)
    {
        CardType = myCardData.CardType;
        CardName = myCardData.CardName;
        CardDescription = myCardData.CardDescription;
        Effect = new Effect(myCardData.Effect);
        UseTimes = myCardData.UseTimes;
        //foreach (var cost in myCardData.listOfCosts)
        //{
        //    if (cost == null)
        //    {
        //        Debug.LogError("Cost is null in MyCardData constructor.");
        //    }
        //}
        listOfCosts = new CurrencyQuantity[myCardData.listOfCosts.Count];
        for (int i = 0; i < myCardData.listOfCosts.Count; i++)
        {
            listOfCosts[i] = (CurrencyQuantity)myCardData.listOfCosts[i].Clone();
        }
        if (listOfCosts == null || listOfCosts.Length == 0)
        {
            Debug.LogWarning("listOfCosts is null or empty in MyCardData constructor.");
            Debug.Log(myCardData.listOfCosts.Count);
        }
        ToolID = myCardData.Effect.ToolID;
        SkillID = myCardData.Effect.SkillID;
    }

    public MyCardData(MyCardData myCardData)
    {
        CardType = myCardData.CardType;
        CardName = myCardData.CardName;
        CardDescription = myCardData.CardDescription;
        Effect = new Effect(myCardData.Effect);
        UseTimes = myCardData.UseTimes;
        listOfCosts = new CurrencyQuantity[myCardData.listOfCosts.Count()];
        for (int i = 0; i < myCardData.listOfCosts.Count(); i++)
        {
            listOfCosts[i] = (CurrencyQuantity)myCardData.listOfCosts[i].Clone();
        }
        if (listOfCosts == null || listOfCosts.Length == 0)
        {
            Debug.LogWarning("listOfCosts is null or empty in MyCardData constructor.");
            Debug.Log(myCardData.listOfCosts.Count());
        }
        CardId = myCardData.CardId;
        ToolID = myCardData.ToolID;
        SkillID = myCardData.SkillID;
    }

    public override string ToString()
    {
        string text = $"{CardName} ({CardType}) {CardDescription} Effect: {Effect.WorkDelta} Work, {Effect.TirednessDelta} Tiredness, Draw {Effect.DrawCardCount} Cards, Buff ID: {Effect.BuffID}\n Costs: {string.Join(", ", listOfCosts.Select(c => $"{c.CurrencyType.Name}: {c.Amount}"))}";
        if (listOfCosts == null || listOfCosts.Count() == 0)
        {
            Debug.LogWarning("listOfCosts is null or empty in ToString method.");
            text += " No costs defined.";
        }
        Debug.Log(text);
        return text;
    }
}

public class MyCardSetup : CardSetup
{
    public SpriteRenderer Image;
    public SpriteRenderer BackImage;
    public TextMeshProUGUI cardProperty;
    public TextMeshProUGUI costText;
    public MyCardDefType CardType { get; private set; }
    public string CardName { get; private set; }

    public PlayerCard PlayerCard;
    private MyCardData originDefData;
    public static int CardIdIndex = 0;
    public int CardId;
    public override void Apply(CardDefinition data)
    {
        if (data is MyCardDef myCard)
        {
            //Debug.Log($"Applying CardId: {myCard.name}");
            Image.sprite = myCard.Sprite;
            if (myCard.BackArt != null)
                BackImage.sprite = myCard.BackArt;
            CardType = myCard.CardType;
            CardName = myCard.CardName;
            if (GetComponent<CurrencyCost>() is CurrencyCost currencyCost)
            {
                foreach (var cost in myCard.listOfCosts)
                {
                    //Debug.Log($"Adding cost: {cost.CurrencyType} : {cost.Amount}");
                    currencyCost.Cost.Add(new CurrencyCost.CostWithLabel
                    {
                        Cost = new CurrencyQuantity { CurrencyType = cost.CurrencyType, Amount = cost.Amount },
                        Label = null
                    });
                }
            }
            costText.text = myCard.CardDescription;

            originDefData = new MyCardData(myCard);
            CardId = CardIdIndex;
            CardIdIndex++;
            gameObject.name = $"{CardName}_{CardId}";
            originDefData.CardId = CardId;
            PlayerCard = GetComponent<PlayerCard>();
            PlayerCard.SetMyCardData(originDefData);
            PlayerCard.Init();
        }
    }


    public void ReInit()
    {
        PlayerCard = GetComponent<PlayerCard>();
        PlayerCard.SetMyCardData(originDefData);
        PlayerCard.Init();
    }



    public void ReDraw(MyCardData myCardData)
    {
        foreach (var cost in myCardData.listOfCosts)
        {
            if (cost.CurrencyType.Name == "Tired")
            {
                string text = cost.Amount.ToString();
                costText.text = text;
            }
        }
    }
}

//public class MyCardSetup : CardSetup
//{
//    public SpriteRenderer Image;
//    public SpriteRenderer BackImage;
//    public CardType CardType { get; private set; }
//    public string CardName { get; private set; }

//    public override void Apply(CardDefinition data)
//    {
//        if (data is MyCardDefinition myCard)
//        {
//            Debug.Log($"Applying CardId: {myCard.name}");
//            Image.sprite = myCard.Sprite;
//            if (myCard.BackArt != null)
//                BackImage.sprite = myCard.BackArt;
//            CardType = myCard.CardType;
//            CardName = myCard.CardName;
//            if(GetComponent<CurrencyCost>() is CurrencyCost currencyCost)
//            {
//                foreach (var cost in myCard.listOfCosts)
//                {
//                    currencyCost.Cost.Add(new CurrencyCost.CostWithLabel
//                    {
//                        Cost = cost,
//                        Label = null
//                    });
//                }
//            }
//        }
//    }
//}
