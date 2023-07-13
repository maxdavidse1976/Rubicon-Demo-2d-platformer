using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform leftPatrolPoint, rightPatrolPoint;
    private bool movingRight;

    private Rigidbody2D enemy;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float timeToMove, timeToWait;
    private float moveCounter, waitCounter;

    private Animator animator;

    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        leftPatrolPoint.parent = null;
        rightPatrolPoint.parent = null;
        movingRight = true;
        moveCounter = timeToMove;
    }

    void Update()
    {
        if (moveCounter > 0)
        {
            moveCounter -= Time.deltaTime;
            if (movingRight)
            {
                spriteRenderer.flipX = true;
                enemy.velocity = new Vector2(movementSpeed, enemy.velocity.y);
                if (transform.position.x > rightPatrolPoint.position.x)
                {
                    movingRight = false;

                }
            }
            else
            {
                spriteRenderer.flipX = false;
                enemy.velocity = new Vector2(-movementSpeed, enemy.velocity.y);
                if (transform.position.x < leftPatrolPoint.position.x)
                {
                    movingRight = true;
                }
            }
            if (moveCounter <= 0)
            {
                waitCounter = Random.Range(timeToWait * .75f, timeToWait * 1.25f);
            }
            animator.SetBool("isMoving", true);
        }
        else if (waitCounter > 0)
        {
            waitCounter -= Time.deltaTime;
            enemy.velocity = new Vector2(0, enemy.velocity.y);
            if (waitCounter <= 0)
            {
                moveCounter = timeToMove;
            }
            animator.SetBool("isMoving", false);
        }
    }
}
