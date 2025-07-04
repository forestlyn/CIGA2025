using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Events;
using UnityEngine;
namespace CardHouse
{
    public class GroupConditional : Activatable
    {
        public Card MyCard;
        public List<GroupNameUnityActionKvp> Responses;

        protected override void OnActivate()
        {
            foreach (var kvp in Responses)
            {
                if (kvp.Key == GroupRegistry.Instance?.GetGroupName(MyCard.Group))
                {
                    //UnityEngine.Debug.Log($"Activating response for group: {kvp.Key}");
                    kvp.Value.Invoke();
                    break;
                }
            }
        }
    }

    [Serializable]
    public class GroupNameUnityActionKvp
    {
        public GroupName Key;
        public UnityEvent Value;
    }
}