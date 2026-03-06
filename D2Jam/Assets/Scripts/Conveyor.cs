using UnityEngine;

public class Conveyor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    enum Direction { Straight, Left, Right };

    [SerializeField]
    private Direction direction;

    private Animator animator;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
