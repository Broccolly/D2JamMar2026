using System.Collections.Generic;
using UnityEngine;
using static Directions;

public class Splitter : MonoBehaviour, IMachine
{
    private Animator animator;
    private GameClock clock;
    public Vector3Int gridPos { get; set; }
    private Level level;
    public Direction currentDirection;

    [SerializeField]
    private Transform itemPointLeft;
    [SerializeField]
    private Transform itemPointRight;

    void Awake()
    {
        animator = GetComponent<Animator>();
        clock = GameObject.FindGameObjectWithTag("Level").GetComponent<GameClock>();
        level = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clock.ClockTickEvent.AddListener(OnTick);
        if (currentDirection == Direction.Left)
        {
            animator.SetTrigger("Left");
        }
        else if (currentDirection == Direction.Right)
        {
            animator.SetTrigger("Right");
        }
    }

    void OnTick()
    {
        animator.SetFloat("AnimTime", clock.CurrentTime);
        level.UpdateTargetForGridPos(gridPos);
    }
    public void TriggerItem(Item item) 
    {
        item.currentGridPos = gridPos;
        if (currentDirection == Direction.Left)
        {
            item.transform.SetParent(itemPointLeft);
        }
        else if (currentDirection == Direction.Right)
        {
            item.transform.SetParent(itemPointRight);
        }
        else
        {
            item.transform.SetParent(null);
        }
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("AnimTime", clock.CurrentTime);
    }

    public void OnActivate()
    {
        if (currentDirection == Direction.Left)
        {
            currentDirection = Direction.Right;
            animator.SetTrigger("Right");
        }
        else
        {
            currentDirection = Direction.Left;
            animator.SetTrigger("Left");
        }

        level.UpdateTargetForGridPos(gridPos);
    }

    public Vector3Int GetWorldTargetDirection()
    {
        Vector3 dir = Vector3.zero;
        if (directionary.ContainsKey(currentDirection))
        {
            dir = directionary[currentDirection];
        }
        else
        {
            Debug.Log($"Directionary missing {currentDirection}");
        }

        dir = transform.TransformDirection(dir);
        return new Vector3Int((int)Mathf.Round(dir.x), (int)Mathf.Round(dir.y), (int)Mathf.Round(dir.z));
    }
}
