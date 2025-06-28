using Assets.Scripts;
using CardHouse;
using MyTools.MyEventSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.VFX;

public class EventDiscardCardArgs : EventArgs
{
    public int Count { get; private set; }
    public EventDiscardCardArgs(int count)
    {
        Count = count;
    }
}

internal class GameManger : MonoBehaviour
{
    private static GameManger _instance;
    public static GameManger Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new System.Exception("GameManger instance is not initialized.");
            }
            return _instance;
        }
    }
    private PlayerManager _playerManager;
    private RoundManager _roundManager;
    private BuffManager _buffManager;
    private ToolManager _toolManager;
    private void Awake()
    {
        _instance = this;
        _playerManager = new PlayerManager();
        _roundManager = new RoundManager();
        _buffManager = new BuffManager();
        _toolManager = new ToolManager();

        _roundManager.StartNewRoundEvent += OnRoundStart;
        _roundManager.EndRoundEvent += OnRoundEnd;
        _playerManager.TirednessChange += OnTirednessChange;
    }
    private void OnTirednessChange(object sender, EventArgs e)
    {
        //Debug.Log($"Player tiredness changed: {_playerManager.PlayerTiredness}");
    }
    private void OnRoundStart(object sender, EventArgs e)
    {
        //Debug.Log("11");
        Debug.Log($"Round started. Current round: {_roundManager.GetCurrentRound()}");
        _playerManager.ChangePlayerTiredness(-5);
        DrawCard(5);
    }
    private void OnRoundEnd(object sender, EventArgs e)
    {
        //Debug.Log($"Round ended. Current round: {_roundManager.GetCurrentRound()}");
        NextRound();
    }

    public void OnClickNextBtn()
    {
        _roundManager.EndRound();
    }

    private void Start()
    {
        StartCoroutine(DelayInit());
    }

    void Update()
    {
        _roundManager.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _roundManager.PhysicsUpdate();
    }

    private void LateUpdate()
    {
        _roundManager.LateUpdate();
    }

    IEnumerator DelayInit()
    {
        yield return new WaitForEndOfFrame();
        _playerManager.Init(100, 100);
        _roundManager.Init();
        _buffManager.Init();
        _toolManager.Init();
        _roundManager.StartNewRound();

    }

    public StateType StateType
    {
        get => _roundManager.StateType;
    }

    public MyEvent WorkChangeEvent = MyEvent.CreateEvent((int)EventTypeEnum.WorkChange);
    public MyEvent DiscardCardEvent = MyEvent.CreateEvent((int)EventTypeEnum.DiacardCardEvent);
    private int work;
    public int Work
    {
        get => work;
        private set
        {
            if (work != value)
            {
                int previousWork = work;
                work = value;
                Debug.Log($"Work changed: {work}");
                WorkChangeEvent.Invoke(this, new WorkChangeEventArgs(previousWork, work));
            }
        }
    }

    public int MaxToolCount { get; internal set; } = 3;
    public int MaxCardCount { get; internal set; } = 8;

    public int MaxItemCount { get; internal set; } = 2;

    public void AddWork(int workChange, bool invokeWorkChange = true)
    {
        int previousWork = Work;
        Work += workChange;
        if (invokeWorkChange)
        {
            WorkChangeEvent.Invoke(this, new WorkChangeEventArgs(previousWork, Work));
        }
    }

    #region card
    public CardGroup CardInit;
    public CardGroup CardHand;
    public CardGroup CardDiacrd;
    public CardGroup CardNoUse;
    public void SetCardGroupActive(bool interactive)
    {
        //CardHand.
        var groupSetting = CardHand.GetComponent<CardGroupSettings>();
        groupSetting.ForcedInteractability = interactive ? GroupInteractability.Active : GroupInteractability.Inactive;
        groupSetting.Apply(CardHand.MountedCards);
    }
    public void DrawCard(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (CardHand.MountedCards.Count >= MaxCardCount)
            {
                Debug.Log("Card hand is full, cannot draw more cardIds.");
                return;
            }

            if (CardInit.MountedCards.Count == 0)
            {
                foreach (var target in CardDiacrd.MountedCards.ToArray())
                {
                    //MyLog.Log($"Card {target.name} diacard:{CardHand.MountedCards.Count}");
                    CardInit.Mount(target);
                }
            }
            if (CardInit.MountedCards.Count > 0)
            {
                Card card = CardInit.MountedCards[0];
                CardHand.Mount(card);
            }
            else
            {
                Debug.Log("No cardIds to draw.");
            }
        }
    }

    public void NextRound()
    {
        MyLog.Log($"NextRound:{CardHand.MountedCards.Count}");
        ClearCardHand();
    }

    public void ClearCardHand()
    {
        int cardCount = CardHand.MountedCards.Count;
        foreach (var target in CardHand.MountedCards.ToArray())
        {
            CardDiacrd.Mount(target, invokeEvent: false);
        }
        DiscardCardEvent.Invoke(this, new EventDiscardCardArgs(cardCount));
    }

    internal void RemoveCard(Card card)
    {
        CardNoUse.Mount(card, invokeEvent: false);
    }
    #endregion
}

internal class WorkChangeEventArgs : EventArgs
{
    public int previousWork;
    public int work;
    public int WorkChange => work - previousWork;
    public WorkChangeEventArgs(int previousWork, int work)
    {
        this.previousWork = previousWork;
        this.work = work;
    }
}