using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardHouse
{
    public class CurrencyCost : MonoBehaviour
    {
        [Serializable]
        public class CostWithLabel
        {
            public CurrencyQuantity Cost;
            public TextMeshPro Label;
        }

        public List<CostWithLabel> Cost;

        void Start()
        {
            foreach (var cost in Cost)
            {
                if (cost.Label != null)
                {
                    cost.Label.text = cost.Cost.Amount.ToString();
                }
            }
        }

        public void UpdateCost(MyCardData myCardData)
        {
            foreach (var cost in Cost)
            {
                foreach (var resource in myCardData.listOfCosts)
                {
                    if (resource.CurrencyType.Name == cost.Cost.CurrencyType.Name)
                    {
                        Debug.Log($"Updating cost for {cost.Cost.CurrencyType.Name} to {resource.Amount} for CardId: {gameObject.name}");
                        cost.Cost.Amount = resource.Amount;
                        break;
                    }
                }
                if (cost.Label != null)
                {
                    cost.Label.text = cost.Cost.Amount.ToString();
                }
            }
        }


        public void Activate()
        {
            Debug.Log("Activating CurrencyCost for PlayerCard: " + gameObject.name);
            if (Cost == null || Cost.Count == 0)
            {
                //Debug.LogWarning("No costs defined for CurrencyCost.");
                return;
            }
            foreach (var resource in Cost)
            {
                Debug.Log(resource.Cost.CurrencyType.Name + " change " + resource.Cost.Amount);
                CurrencyRegistry.Instance.AdjustCurrency(resource.Cost.CurrencyType.Name, PhaseManager.Instance.PlayerIndex, -1 * resource.Cost.Amount);
            }
        }
    }
}
