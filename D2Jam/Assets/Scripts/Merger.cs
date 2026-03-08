using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Directions;

public class Merger : MonoBehaviour, IMachine
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public Vector3Int gridPos { get; set; }

    private Animator animator;

    private GameClock clock;

    [SerializeField]
    private Direction priorityDirection;

    [SerializeField]
    private Transform itemPointLeft;

    [SerializeField]
    private Transform itemPointRight;


    void Awake()
    {
        animator = GetComponent<Animator>();
        clock = GameObject.FindGameObjectWithTag("Level").GetComponent<GameClock>();
    }

    public void OnActivate()
    {

    }

    private void Start()
    {
        clock.ClockTickEvent.AddListener(OnTick);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("AnimTime",clock.CurrentTime);// + (resetRequired ? -1f : 0f))
    }

    public void TriggerItem(Item item)
    {
        Vector3Int dir = gridPos - item.currentGridPos;
        Vector3 localDir = transform.InverseTransformDirection((Vector3)dir);
        float leftDot = Vector3.Dot(localDir, directionary[Direction.Left]);
        float rightDot = Vector3.Dot(localDir, directionary[Direction.Right]);

        if (leftDot > rightDot)
        {
            // Coming from right
            item.transform.SetParent(itemPointRight);
            Debug.Log("Merger : Coming from right!");
        }
        else
        {
            item.transform.SetParent(itemPointLeft);
            Debug.Log("Merger : Coming from left!");
        }

        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.currentGridPos = gridPos;
    }

    void OnTick()
    {
        animator.SetFloat("AnimTime", clock.CurrentTime);// + (resetRequired ? -1f : 0f));
    }

    public Vector3Int GetWorldTargetDirection()
    {
        Vector3 dir = Vector3.zero;
        if (directionary.ContainsKey(Direction.Straight))
        {
            dir = directionary[Direction.Straight];
        }
        else
        {
            Debug.Log($"Directionary missing Direction.Straight");
        }

        dir = transform.TransformDirection(dir);
        return new Vector3Int((int)Mathf.Round(dir.x), (int)Mathf.Round(dir.y), (int)Mathf.Round(dir.z));
    }
}
