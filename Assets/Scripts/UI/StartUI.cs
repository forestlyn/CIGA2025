using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public Button startBtn;
    public Button exitBtn;


    private void Start()
    {
        startBtn.onClick.AddListener(OnStartButtonClicked);
        exitBtn.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnStartButtonClicked()
    {
        TransitionManager.Instance.Win = false;
        TransitionManager.Instance.Transition("StartScene", "CiGATestWithUI");
    }



}
