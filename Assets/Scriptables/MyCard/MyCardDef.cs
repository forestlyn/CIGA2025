using CardHouse;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum MyCardDefType
{
    None,
    Skill,
    Prop,
    Item
}

[CreateAssetMenu(menuName = "CIGA/MyCard")]
public class MyCardDef : CardDefinition
{
    public Sprite Sprite;
    public MyCardDefType CardType;
    public string CardName;
    public string CardDescription;
    public string CardFlavorText;

    public Effect Effect;
    public int UseTimes;
    public List<CurrencyQuantity> listOfCosts;
}

[Serializable]
public class Effect
{
    public int WorkDelta;
    public bool AddTirednessDelta;
    public int TirednessDelta;
    public int DrawCardCount;

    public int BuffID;
    public int ItemID;
    public int ToolID;
    public int SkillID;
    public Effect(Effect effect)
    {
        WorkDelta = effect.WorkDelta;
        AddTirednessDelta = effect.AddTirednessDelta;
        TirednessDelta = effect.TirednessDelta;
        DrawCardCount = effect.DrawCardCount;
        BuffID = effect.BuffID;
        ItemID = effect.ItemID;
        ToolID = effect.ToolID;
        SkillID = effect.SkillID;
    }

    public void PlayEffect(MyCardData myCardData)
    {
        if (WorkDelta != 0)
        {
            GameManger.Instance.AddWork(WorkDelta);
        }
        if (TirednessDelta != 0)
        {
            int flag = AddTirednessDelta ? -1 : 1;
            PlayerManager.Instance.ChangePlayerTiredness(flag * TirednessDelta);
        }
        if (DrawCardCount > 0)
        {
            GameManger.Instance.DrawCard(DrawCardCount);
        }
        if (BuffID != -1)
        {
            Buff buff = BuffManager.Instance.GetBuff(BuffID);
            if (buff != null)
            {
                BuffManager.Instance.AddBuff(buff);
            }
        }
        if (ItemID != -1)
        {
            var item = ItemManager.Instance.GetItemGO(ItemID);
            if (item != null)
            {
                item.SetActive(true);
                ItemManager.Instance.AddItem(item.GetComponent<Item>());
                item.GetComponent<Item>().SetDescriptionText($"{myCardData.CardName}:{myCardData.CardFlavorText} ({myCardData.CardDescription})");
            }
        }
    }

}