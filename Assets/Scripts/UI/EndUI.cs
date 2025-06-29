using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    public Button startBtn;
    public Button exitBtn;
    public GameObject winAnim;

    private void Start()
    {
        winAnim.SetActive(TransitionManager.Instance.Win);
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
        TransitionManager.Instance.Transition("End", "CiGATestWithUI");
        TransitionManager.Instance.Win = false;
    }


}
