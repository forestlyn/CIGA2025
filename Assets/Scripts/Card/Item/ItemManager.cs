using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


internal class ItemManager
{
    private static ItemManager _instance;
    public static ItemManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("ItemManager instance is not initialized. Please call Initialize() first.");
            }
            return _instance;
        }
    }
    private ItemManager()
    {
        _instance = this;
    }

    Dictionary<int,GameObject> itemPrefabs = new Dictionary<int, GameObject>();
    List<Item> itemList = new List<Item>();

    public void Init()
    {

    }

    public bool HasRoom()
    {
        return itemList.Count < GameManger.Instance.MaxItemCount;
    }
}

