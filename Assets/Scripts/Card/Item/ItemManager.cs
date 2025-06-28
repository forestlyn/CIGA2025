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
    private List<Item> itemList = new List<Item>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init()
    {

    }

    public bool HasRoom()
    {
        return itemList.Count < GameManger.Instance.MaxItemCount;
    }

    internal Item GetItem(int itemID)
    {
        return null;
    }

    internal void AddItem(Item item)
    {
        if (HasRoom())
        {
            itemList.Add(item);
            item.gameObject.SetActive(true);
            item.Active();
        }
        else
        {
            Debug.LogWarning("No room for more items.");
        }
    }
}

