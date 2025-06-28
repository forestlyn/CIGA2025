using CardHouse;
using MyTools.MyEventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


internal class BuffManager
{
    private static BuffManager _instance;
    public static BuffManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new System.Exception("BuffManager instance is not initialized.");
            }
            return _instance;
        }
    }
    public BuffManager()
    {
        _instance = this;
    }
    private Dictionary<int, GameObject> _buffPrefabs = new Dictionary<int, GameObject>();
    private List<Buff> _buffs = new List<Buff>();

    public MyEvent BuffChange = MyEvent.CreateEvent((int)EventTypeEnum.BuffChange);

    public void Init()
    {
        _buffPrefabs.Add(1, Resources.Load<GameObject>("Buffs/Buff1"));
        _buffPrefabs.Add(2, Resources.Load<GameObject>("Buffs/Buff2"));
        _buffPrefabs.Add(3, Resources.Load<GameObject>("Buffs/Buff3"));
        _buffPrefabs.Add(4, Resources.Load<GameObject>("Buffs/Buff4"));
        _buffPrefabs.Add(5, Resources.Load<GameObject>("Buffs/Buff5"));
        _buffPrefabs.Add(6, Resources.Load<GameObject>("Buffs/Buff6"));
    }
    public Buff GetBuff(int buffID)
    {
        _buffPrefabs.TryGetValue(buffID, out var buffPrefab);
        if (buffPrefab != null)
        {
            Debug.Log($"Creating buff with ID {buffID} from prefab {buffPrefab.name}.");
            GameObject buffObject = GameObject.Instantiate(buffPrefab);
            Buff buff = buffObject.GetComponent<Buff>();
            return buff;
        }
        Debug.LogWarning($"Buff with ID {buffID} not found.");
        return null;
    }

    internal void RemoveBuff(Buff buff)
    {
        _buffs.Remove(buff);
        BuffChange.Invoke(this, EventArgs.Empty);
    }

    internal void AddBuff(Buff buff)
    {
        _buffs.Add(buff);
        buff.Notify();
        BuffChange.Invoke(this, EventArgs.Empty);
    }

    internal MyCardData CalculateBuff(MyCardData myCardData)
    {
        var effect = new Effect(myCardData.Effect);
        var costs = new List<CurrencyQuantity>(myCardData.listOfCosts);
        foreach (var buff in _buffs)
        {
            if (buff.CheckCondition(myCardData))
            {
                effect = buff.CalculateCardEffect(myCardData, effect);
                costs = buff.CalculateCardCost(myCardData, costs);
            }
        }
        MyCardData result = new MyCardData(myCardData);
        result.Effect = effect;
        if (costs == null)
        {
            Debug.LogWarning("Costs list is null after buff calculations.");
        }
        //for(int i = 0; i < costs.Count; i++)
        //{
        //    Debug.Log($"Cost {i}: {costs[i].CurrencyType.Name} : {costs[i].Amount}");
        //}
        result.listOfCosts = costs.ToArray();
        return result;
    }

    internal void PlayCard(MyCardData myCardData)
    {
        if (myCardData == null)
        {
            Debug.LogError("MyCardDef is null in BuffManager.PlayCard.");
            return;
        }
        foreach (var buff in _buffs.ToArray())
        {
            if (buff.CheckCondition(myCardData))
            {
                Debug.Log($"Applying buff {buff.BuffName} to CardId {myCardData.CardId}.");
                buff.Apply(myCardData);
            }
        }
    }
}

