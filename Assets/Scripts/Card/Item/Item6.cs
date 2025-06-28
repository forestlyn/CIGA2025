using System;
using UnityEngine;
/// <summary>
/// 每当你弃牌时，每弃一张，碎片块数增加1
/// </summary>
public class Item6 : Item
{
    public override void Active()
    {
        GameManger.Instance.DiscardCardEvent += OnDiscardCard;
    }

    public void OnDiscardCard(object sender, EventArgs e)
    {
        if(e is EventDiscardCardArgs discardArgs)
        {
            GameManger.Instance.AddWork(discardArgs.Count);
        }
    }

    public override void InActive()
    {
        GameManger.Instance.DiscardCardEvent -= OnDiscardCard;
    }
}
