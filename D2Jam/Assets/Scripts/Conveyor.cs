using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Directions;

public class Conveyor : MonoBehaviour, IMachine
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public Vector3Int gridPos { get; set; }

    [SerializeField]
    public Direction currentDirection;

    private Animator animator;

    private GameClock clock;

    private Transform activeItemSlot;

    //[ContextMenu("Update Prefab")]
    //public void UpdatePrefab
    //{

    //}

    public Transform ItemPoint { get; private set; }

    public enum ConveyorType
    {
        Straight,
        Left,
        Right
    }

    [SerializeField]
    private ConveyorType conveyorType;


    void Awake()
    {
        animator = GetComponent<Animator>();

        switch (conveyorType)
        {
            case ConveyorType.Left:
                if (animator != null)
                {
                    animator.SetBool("IsStraight", false);
                    animator.SetBool("Right", false);
                    animator.SetBool("Left", true);
                }
                currentDirection = Direction.Left;
                break;
            case ConveyorType.Right:
                if (animator != null)
                {
                    animator.SetBool("IsStraight", false);
                    animator.SetBool("Right", true);
                    animator.SetBool("Left", false);
                }
                currentDirection = Direction.Right;
                break;
            case ConveyorType.Straight:
                if (animator != null)
                {
                    animator.SetBool("IsStraight", true);
                    animator.SetBool("Right", false);
                    animator.SetBool("Left", false);
                }
                currentDirection = Direction.Straight;
                break;
            default:
                break;

        }

        clock = GameObject.FindGameObjectWithTag("Level").GetComponent<GameClock>();
        ItemPoint = transform.Find("Pivot").Find("ItemPoint");
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
        item.transform.SetParent(ItemPoint);
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
