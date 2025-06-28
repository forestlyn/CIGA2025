using CardHouse;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolSO", menuName = "CIGA/ToolSO", order = 1)]
public class ToolSO : ScriptableObject
{
    public MyCardDef cardDef;
    public GameObject tool;
}
