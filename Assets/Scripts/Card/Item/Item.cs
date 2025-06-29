using Assets.Scripts;
using System;
using System.ComponentModel.Design;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int ItemID;
    public string text;
    public abstract void Active();

    public abstract void InActive();

    internal void SetDescriptionText(string v)
    {
        text = v;
    }

    private void OnMouseEnter()
    {
        UIManager.Instance.SetDescriptionText(text);
    }

    private void OnMouseExit()
    {
        UIManager.Instance.SetDescriptionText(string.Empty);
    }
}
