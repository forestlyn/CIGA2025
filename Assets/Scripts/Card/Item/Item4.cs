using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 你的手牌上限改为15
/// </summary>
internal class Item4 : Item
{
    public int ChangeMaxCardCount = 15;
    private int PreMaxCardCount;
    public override void Active()
    {
        PreMaxCardCount = GameManger.Instance.MaxCardCount;
        GameManger.Instance.MaxCardCount = ChangeMaxCardCount;
    }

    public override void InActive()
    {
        GameManger.Instance.MaxCardCount = PreMaxCardCount;
    }
}

