using UnityEngine;

public class Conveyor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    enum Direction { Straight, Left, Right };

    [SerializeField]
    private Direction direction;

    private Animator animator;

    private GameClock clock;

    public Transform? ItemPoint { get; private set; }


    void Awake()
    {
        animator = GetComponent<Animator>();

        switch (direction)
        {
            case Direction.Left:
                if (animator != null)
                {
                    animator.SetBool("IsStraight", false);
                    animator.SetBool("Right", false);
                    animator.SetBool("Left", true);
                }
                break;
            case Direction.Right:
                if (animator != null)
                {
                    animator.SetBool("IsStraight", false);
                    animator.SetBool("Right", true);
                    animator.SetBool("Left", false);
                }
                break;
            case Direction.Straight:
                if (animator != null)
                {
                    animator.SetBool("IsStraight", true);
                    animator.SetBool("Right", false);
                    animator.SetBool("Left", false);
                }
                break;
            default:
                break;

        }

        clock = GameObject.FindGameObjectWithTag("Level").GetComponent<GameClock>();

        ItemPoint = transform.Find("Pivot").Find("ItemPoint");
    }

    private void Start()
    {
        clock.ClockTickEvent.AddListener(OnTick);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("AnimTime",clock.CurrentTime);// + (resetRequired ? -1f : 0f));
    }

    void OnTick()
    {
        animator.SetFloat("AnimTime", clock.CurrentTime);// + (resetRequired ? -1f : 0f));
    }

    public Transform GetNextItemPoint()
    {
        Vector3 aimDirection=Vector3.zero;

        switch(direction)
        {
            case (Direction.Right):
                aimDirection = Vector3.forward; break;
            case (Direction.Left):
                aimDirection = Vector3.back; break;
            case (Direction.Straight):
                aimDirection = Vector3.left; break;
        }

        RaycastHit hit;

       if(Physics.Raycast(transform.position, transform.TransformDirection(aimDirection), out hit, 0.7f))
       {
            return hit.transform.gameObject.GetComponent<Conveyor>().ItemPoint;
       }
        return null;
    }

}
