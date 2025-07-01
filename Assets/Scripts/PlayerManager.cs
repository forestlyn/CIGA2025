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
        TirednessChange = newTiredness - originalTiredness;
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

    private int _PlayerTiredness = 0;
    /// <summary>
    /// 这个疲劳值实际上是相反的，从满值100开始减少，0时游戏结束。
    /// </summary>
    public int PlayerTiredness
    {
        get => _PlayerTiredness;
        set
        {
            UnityEngine.Debug.Log($"Setting PlayerTiredness: {value} delta:{value - PlayerTiredness}");
            if (PlayerTiredness != value)
            {
                var previousTiredness = PlayerTiredness;
                CurrencyRegistry.Instance.AdjustCurrency(PlayerTirednessCurrencyType, PhaseManager.Instance.PlayerIndex, PlayerTiredness - value);
                _PlayerTiredness = value;
                TirednessChange.Invoke(this, new EventTirednessChangeArgs(previousTiredness, PlayerTiredness));
                if (PlayerTiredness >= PlayerTirednessMax)
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
}