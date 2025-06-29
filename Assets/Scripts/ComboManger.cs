using UnityEngine;

public class ComboManger : MonoBehaviour
{
    public static ComboManger _instance;
    public static ComboManger Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new System.Exception("ComboManger instance is not initialized.");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("ComboManger instance already exists. Destroying duplicate.");
        }
    }
}
