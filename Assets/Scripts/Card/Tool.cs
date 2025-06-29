using CardHouse;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Tool : MonoBehaviour
{
    public int ToolID;
    public PlayerCard playerCard;
    bool isFocused;

    public float MoveSpeed = 1f;
    private Vector3 targetPos;

    private bool isActive;
    public bool IsActive
    {
        get => isActive; 
        internal set
        {
            isActive = value;
        }
    }

    private void Awake()
    {
        //transform.position = new Vector3(Random.Range(-7, 7), Random.Range(-2.5f, 2.5f), 0);
        //targetPos = RandomTargetPos();
        IsActive = false;
        GetComponent<DragDetector>().IsActive = false;
    }

    //private Vector3 RandomTargetPos()
    //{
    //    Vector3 pos = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0) + transform.position;
    //    pos.x = Mathf.Clamp(pos.x, -7, 7);
    //    pos.y = Mathf.Clamp(pos.y, -2.5f, 2.5f);
    //    return pos;
    //}


    //private void Move(float deltaTime)
    //{
    //    if (Vector3.Distance(transform.position, targetPos) < 0.1f)
    //    {
    //        targetPos = RandomTargetPos();
    //    }
    //    transform.position = Vector3.MoveTowards(transform.position, targetPos, MoveSpeed * deltaTime);
    //}

    public void MyUpdate(float deltaTime)
    {
        //Move(deltaTime);
    }

    private void Update()
    {
        if (!isFocused)
        {
            MyUpdate(Time.deltaTime);
        }
    }

    private void OnMouseEnter()
    {
        isFocused = true;
    }

    private void OnMouseDown()
    {
        if (!isActive)
        {
            return;
        }
        playerCard.ActivateEffect();
        ToolManager.Instance.RemoveTool(this);
        //Destroy(gameObject);
    }

    private void OnMouseExit()
    {
        isFocused = false;
    }

}
