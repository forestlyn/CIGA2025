using Assets.Scripts;
using System;
using System.ComponentModel.Design;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public PlayerCard playerCard;

    public abstract void Active();

    public abstract void InActive();
}
