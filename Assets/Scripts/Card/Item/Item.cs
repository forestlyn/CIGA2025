using Assets.Scripts;
using CardHouse;
using System;
using System.ComponentModel.Design;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int ItemID;
    public string text;
    public Card card;
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
