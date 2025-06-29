using TMPro;
using UnityEngine;



public class ShowNumGroup : MonoBehaviour
{
    public static ShowNumGroup Create(Vector3 position, int damageAmount)
    {
        Transform popupTransform = Instantiate(Resources.Load<Transform>("ShowNumGroup"), position, Quaternion.identity);
        ShowNumGroup popup = popupTransform.GetComponent<ShowNumGroup>();
        popup.Setup(damageAmount);
        return popup;
    }

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Vector3 moveVector;
    private Color textColor;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
        textColor = textMesh.color;
        disappearTimer = 1f; // 显示时间
        moveVector = new Vector3(0.5f, 1f) * 1f; // 浮动方向
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 5f * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            // 渐隐
            textColor.a -= 3f * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
