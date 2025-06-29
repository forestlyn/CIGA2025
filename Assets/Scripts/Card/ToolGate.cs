using CardHouse;
using UnityEngine;

public class ToolGate : Gate<NoParams>
{
    PlayerCard playerCard;

    void Awake()
    {
        playerCard = GetComponent<PlayerCard>();
    }

    protected override bool IsUnlockedInternal(NoParams argObject)
    {
        if (playerCard.GetMyCardData().CardType == MyCardDefType.Prop 
            && !ToolManager.Instance.HasRoom()
            &&ToolManager.Instance.HasTool(playerCard.GetMyCardData().ToolID))
        {
            return false;
        }
        return true;
    }

}
