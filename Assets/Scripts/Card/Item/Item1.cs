using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 每个回合结束时，增加5块碎片
/// </summary>
internal class Item1 : Item
{
    public override void Active()
    {
        RoundManager.Instance.EndRoundEvent += OnEndRound;
    }

    private void OnEndRound(object sender, EventArgs eventArgs)
    {
        GameManger.Instance.AddWork(5);
    }
    public override void InActive()
    {
        RoundManager.Instance.EndRoundEvent -= OnEndRound;
    }
}

