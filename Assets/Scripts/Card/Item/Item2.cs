using MyTools.MyEventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 每次增加疲劳值后，疲劳值降低1
/// </summary>
internal class Item2 : Item
{
    

    public override void Active()
    {
        PlayerManager.Instance.TirednessChange += OnTirednessChange;
    }

    private void OnTirednessChange(object sender, EventArgs e)
    {
        if (e is EventTirednessChangeArgs tirednessChangeArgs)
        {
            if (tirednessChangeArgs.TirednessChange > 0)
            {
                PlayerManager.Instance.PlayerTiredness = Math.Max(0, PlayerManager.Instance.PlayerTiredness + 1);
            }
        }
    }
    public override void InActive()
    {
        PlayerManager.Instance.TirednessChange -= OnTirednessChange;
    }
}

