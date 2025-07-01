using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PointUI : MonoBehaviour
{
    public float tiredStartY = -2.0f;
    public float tiredEndY = -4.0f;
    public float workStartY = -2.0f;
    public float workEndY = -4.0f;
    public TextMeshProUGUI tiredText;
    public TextMeshProUGUI workText;
    public GameObject tiredPointObject;
    public GameObject workPointObject;
    public GameObject tiredLineObject;
    public GameObject workLineObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }


    public void UpdateUI()
    {
        if (PlayerManager.Instance.PlayerTirednessMax == 0)
        {
            return;
        }
        int tiredness = PlayerManager.Instance.PlayerTiredness;
        float tirednessRatio = (float)tiredness / PlayerManager.Instance.PlayerTirednessMax;
        float tirednessY = Mathf.Lerp(tiredStartY, tiredEndY, tirednessRatio);
        tiredPointObject.transform.localPosition = new Vector3(tiredPointObject.transform.localPosition.x, tirednessY, tiredText.transform.localPosition.z);
        tiredLineObject.transform.localScale = new Vector3(tiredLineObject.transform.localScale.x, tirednessRatio, tiredLineObject.transform.localScale.z);
        int work = GameManger.Instance.Work;
        float workRatio = (float)work / GameManger.Instance.WorkMax;
        float workY = Mathf.Lerp(workStartY, workEndY, workRatio);
        workPointObject.transform.localPosition = new Vector3(workPointObject.transform.localPosition.x, workY, workText.transform.localPosition.z);
        workLineObject.transform.localScale = new Vector3(workLineObject.transform.localScale.x, Mathf.Clamp01(workRatio), workLineObject.transform.localScale.z);
        tiredText.text = PlayerManager.Instance.PlayerTiredness.ToString();
        workText.text = GameManger.Instance.Work.ToString();
    }
}
