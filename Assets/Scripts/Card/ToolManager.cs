using Assets.Scripts;
using CardHouse;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


internal class ToolManager : MonoBehaviour
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

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Init();
        }
        else if (_instance != this)
        {
            Debug.LogError("ToolManager instance already exists. Destroying duplicate.");
        }
    }

    [SerializeField]
    List<GameObject> toolGOs = new List<GameObject>();

    List<Tool> tools = new List<Tool>();

    public void Init()
    {
        //toolGOs.Add(0, Resources.Load<GameObject>("Tools/Tool"));
    }

    public bool HasTool(int v)
    {
        return tools.Any(t => t.ToolID == v);
    }

    internal bool CreateTool(int v,PlayerCard playerCard)
    {
        if (!HasRoom())
        {
            return false;
        }
        if (HasTool(v))
        {
            Debug.LogWarning($"Tool with ID {v} already exists.");
            return false;
        }
        GameObject toolGo = toolGOs.FirstOrDefault(go => go.GetComponent<Tool>().ToolID == v);
        if (toolGo == null)
        {
            Debug.LogError($"Tool prefab with ID {v} not found.");
            return false;
        }
        Debug.Log($"Creating tool with ID {v} from prefab {toolGo.name}.");
        toolGo.SetActive(true);
        Tool tool = toolGo.GetComponent<Tool>();
        tool.playerCard = playerCard;
        tools.Add(tool);
        tool.IsActive = true;
        if (!HasRoom())
        {
            UIManager.Instance.ShowThought("...已经思考得太多了", 2f);
        }
        return true;
    }

    internal void RemoveTool(Tool tool)
    {
        tools.Remove(tool);
        tool.IsActive = false; 
        tool.gameObject.SetActive(false);
    }

    internal bool HasRoom()
    {
        //Debug.Log($"Current tool count: {tools.Count}, Max tool count: {GameManger.Instance.MaxToolCount}");
        return GameManger.Instance.MaxToolCount > tools.Count;
    }
}

