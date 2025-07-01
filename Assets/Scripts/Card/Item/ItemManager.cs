using CardHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


internal class ItemManager : MonoBehaviour
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
    [SerializeField]
    private List<GameObject> itemGOs = new List<GameObject>();
    [SerializeField]
    private List<Item> itemList = new List<Item>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("ItemManager instance already exists. Destroying duplicate.");
        }
    }

    public void Init()
    {

    }

    public bool HasRoom()
    {
        return itemList.Count < GameManger.Instance.MaxItemCount;
    }

    internal GameObject GetItemGO(int itemID)
    {
        foreach (var item in itemGOs)
        {
            if (item.GetComponent<Item>().ItemID == itemID)
            {
                return item;
            }
        }
        return null;
    }

    internal void AddItem(Item item)
    {
        if (HasRoom())
        {
            itemList.Add(item);
            item.gameObject.SetActive(true);
            item.Active();
            if (!HasRoom())
                UIManager.Instance.ShowThought("...已经思考得太多了", 2f);
        }
        else
        {
            Debug.LogWarning("No room for more items.");
        }
    }
}

