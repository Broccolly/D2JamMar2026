using UnityEngine;

public class Item : MonoBehaviour
{
    GameClock clock;
    Level level;
    public Vector3Int currentGridPos;

    private void Awake()
    {
        clock = GameObject.FindGameObjectWithTag("Level").GetComponent<GameClock>();
        level = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
        clock.ClockTickEvent.AddListener(OnTick);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentGridPos = new Vector3Int((int)Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y), (int)Mathf.Round(transform.position.z));
        // Debug.Log(currentGridPos);
        IMachine conv = level.GetObjectInGridPos(currentGridPos).GetComponent<IMachine>();
        conv.TriggerItem(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Advance()
    {
        GameObject? obj = level.GetTargetObjectForGridPos(currentGridPos);
        IMachine conv = obj?.GetComponent<IMachine>() ?? null;
        if (conv != null)
        {
            conv.TriggerItem(this);
        }
        else
        {
            Debug.Log("Couldn't Find Next Machine");
            transform.SetParent(null);
        }
    }

    void OnTick()
    {
        Advance();
    }
}
