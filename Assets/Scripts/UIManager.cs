using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI cardInitCountText;
    public TextMeshProUGUI cardDiscardCountText;
    public TextMeshProUGUI descriptionText;
    public PointUI pointUI;
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new System.Exception("UIManager instance is not initialized.");
            }
            return _instance;
        }
    }


    private void Awake()
    {
        _instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateCardCountText();
    }

    private void UpdateCardCountText()
    {
        cardInitCountText.text = GameManger.Instance.CardInit.MountedCards.Count.ToString();
        cardDiscardCountText.text = GameManger.Instance.CardDiacrd.MountedCards.Count.ToString();
    }

    public void SetDescriptionText(string text)
    {
        descriptionText.text = text;
    }

}
