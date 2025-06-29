using Assets.Scripts;
using CardHouse;
using MyTools.MyEventSystem;
using System;
using System.Diagnostics;
using UnityEngine;

public class EventTirednessChangeArgs : EventArgs
{
    public int OriginalTiredness { get; private set; }
    public int NewTiredness { get; private set; }
    public int TirednessChange { get; private set; }
    public EventTirednessChangeArgs(int originalTiredness, int newTiredness)
    {
        TirednessChange = originalTiredness - newTiredness;
        OriginalTiredness = originalTiredness;
        NewTiredness = newTiredness;
    }
}

public class EventPlayCardArgs : EventArgs
{
    public MyCardData Card { get; private set; }
    public EventPlayCardArgs(MyCardData myCardData)
    {
        Card = myCardData;
    }
}

internal class PlayerManager
{
    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new System.Exception("PlayerManager instance is not initialized.");
            }
            return _instance;
        }
    }
    public const string PlayerTirednessCurrencyType = "Tired";
    private int _PlayerTirednessMax;
    public int PlayerTirednessMax
    {
        get => _PlayerTirednessMax;
        private set
        {
            if (_PlayerTirednessMax != value)
            {
                _PlayerTirednessMax = value;
                CurrencyRegistry.Instance.PlayerWallets[0].Currencies.Find(c => c.CurrencyType.Name == PlayerTirednessCurrencyType)
                    .Max = value;
            }
        }
    }

    public int PlayerTiredness
    {
        get => (int)CurrencyRegistry.Instance.GetCurrency(PlayerTirednessCurrencyType, PhaseManager.Instance.PlayerIndex);
        set
        {
            UnityEngine.Debug.Log($"Setting PlayerTiredness: {value} delta:{value - PlayerTiredness}");
            if (PlayerTiredness != value)
            {
                var previousTiredness = PlayerTiredness;
                CurrencyRegistry.Instance.AdjustCurrency(PlayerTirednessCurrencyType, PhaseManager.Instance.PlayerIndex, value - PlayerTiredness);
                TirednessChange.Invoke(this, new EventTirednessChangeArgs(previousTiredness, PlayerTiredness));
                if (value <= 0)
                {
                    TransitionManager.Instance.Win = false;
                    TransitionManager.Instance.Transition("CiGATestWithUI", "End");
                }
            }
        }
    }

    public MyEvent TirednessChange = MyEvent.CreateEvent((int)EventTypeEnum.PlayerTirednessChange);
    public MyEvent PlayCardEvent = MyEvent.CreateEvent((int)EventTypeEnum.PlayCard);
    public PlayerManager()
    {
        _instance = this;

    }

    public void Init(int playerTiredness, int playerTirednessMax)
    {
        ReInit(playerTiredness, playerTirednessMax);
    }

    public void ReInit(int playerTiredness, int playerTirednessMax)
    {
        PlayerTiredness = playerTiredness;
        PlayerTirednessMax = playerTirednessMax;
    }

    internal void ChangePlayerTiredness(int tirednessChange)
    {
        if (PlayerTiredness + tirednessChange < 0)
        {
            PlayerTiredness = 0;
        }
        else if (PlayerTiredness + tirednessChange > PlayerTirednessMax)
        {
            PlayerTiredness = PlayerTirednessMax;
        }
        else
        {
            PlayerTiredness += tirednessChange;
        }
    }
}