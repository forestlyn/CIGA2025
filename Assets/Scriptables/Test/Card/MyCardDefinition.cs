using CardHouse;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    None,
    Verification,
    Threat,
    Bait
}

[CreateAssetMenu(menuName = "MyCardSetup")]
public class MyCardDefinition : CardDefinition
{
    public Sprite Sprite;
    public CardType CardType;
    public string CardName;

    public List<CurrencyQuantity> listOfCosts;
}
