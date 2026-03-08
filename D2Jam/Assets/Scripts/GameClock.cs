using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameClock : MonoBehaviour
{

    public UnityEvent ClockTickEvent;

    [SerializeField]
    private float clockSpeed;

    public float CurrentTime { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CurrentTime += Time.fixedDeltaTime * clockSpeed;
        if (CurrentTime >= 1.0f)
        {
            ClockTickEvent.Invoke();
            CurrentTime -= 1.0f;
        }
    }
}
