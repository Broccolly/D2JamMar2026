using UnityEngine;

public class Item : MonoBehaviour
{

    Transform newParent = null;
    
    GameClock clock;

    private void Awake()
    {
        clock = GameObject.FindGameObjectWithTag("Level").GetComponent<GameClock>();
        clock.ClockTickEvent.AddListener(OnTick);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateConveyor()
    {
        Debug.Log("UpdateConveyor");
        if (transform.parent == null)
        {
            return;
        }
        Transform? newParent = transform.parent.parent.parent.GetComponent<Conveyor>().GetNextItemPoint();
        if (newParent == null)
        {
            transform.SetParent(null);
        }
        else
        {
            transform.SetParent(newParent);
            transform.localPosition = Vector3.up * 0.2f;
        }

    }

    void OnTick()
    {
        UpdateConveyor();
    }
}
