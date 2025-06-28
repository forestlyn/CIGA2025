using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 每次增加碎片块数后，碎片块数增加1
/// </summary>
internal class Item3 : Item
{
    public override void Active()
    {
        GameManger.Instance.WorkChangeEvent += OnWorkChange;
    }

    private void OnWorkChange(object sender,EventArgs eventArgs)
    {
        if(eventArgs is WorkChangeEventArgs workChangeEventArgs)
        {
            if (workChangeEventArgs.WorkChange > 0)
            {
                GameManger.Instance.AddWork(1, false);
            }
        }
    }
    public override void InActive()
    {
        GameManger.Instance.WorkChangeEvent -= OnWorkChange;
    }
}

