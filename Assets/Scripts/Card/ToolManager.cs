using Assets.Scripts;
using CardHouse;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


internal class ToolManager
{
    private static ToolManager _instance;
    public static ToolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new System.Exception("RoundManager instance is not initialized. Please call Init() before accessing the instance.");
            }
            return _instance;
        }
    }

    public ToolManager()
    {
        _instance = this;
    }
    Dictionary<int, GameObject> toolPrefabs = new Dictionary<int, GameObject>();

    List<Tool> tools = new List<Tool>();

    public void Init()
    {
        toolPrefabs.Add(0, Resources.Load<GameObject>("Tools/Tool"));
    }

    internal bool CreateTool(int v, PlayerCard playerCard)
    {
        if (!HasRoom())
        {
            return false;
        }
        toolPrefabs.TryGetValue(v, out GameObject toolPrefab);
        if (toolPrefab == null)
        {
            Debug.LogError($"Tool prefab with ID {v} not found.");
            return false;
        }
        GameObject toolInstance = UnityEngine.Object.Instantiate(toolPrefab);
        Tool tool = toolInstance.GetComponent<Tool>();
        tool.playerCard = playerCard;
        tools.Add(tool);
        GameManger.Instance.RemoveCard(playerCard.GetComponent<Card>());

        return true;
    }

    internal void RemoveTool(Tool tool)
    {
        tools.Remove(tool);
    }

    internal bool HasRoom()
    {
        //Debug.Log($"Current tool count: {tools.Count}, Max tool count: {GameManger.Instance.MaxToolCount}");
        return GameManger.Instance.MaxToolCount > tools.Count;
    }
}

