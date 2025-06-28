using CardHouse;
using UnityEngine;

public class ItemGate : Gate<NoParams>
{
    PlayerCard playerCard;

    void Awake()
    {
        playerCard = GetComponent<PlayerCard>();
    }

    protected override bool IsUnlockedInternal(NoParams argObject)
    {
        if (playerCard.GetMyCardData().CardType == MyCardDefType.Item && !ItemManager.Instance.HasRoom())
        {
            return false;
        }
        return true;
    }
}
