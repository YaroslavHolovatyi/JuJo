using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public bool useAccelerometer = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Jump();
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
            return;
        HandleMovement();
        WrapAroundScreen();
        if (transform.position.y < Camera.main.transform.position.y - 6f)
        {
            GameOver();
        }
    }

    void HandleMovement()
    {
        float moveInput = 0f;

        if (useAccelerometer)
        {
            moveInput = Input.acceleration.x;
        }
        else
        {
            moveInput = 0f;

            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began ||
                    touch.phase == TouchPhase.Stationary ||
                    touch.phase == TouchPhase.Moved)
                {
                    if (touch.position.x < Screen.width / 2)
                    {
                        // Тап по лівій стороні — рух вліво
                        moveInput = -1f;
                    }
                    else
                    {
                        // Тап по правій стороні — рух вправо
                        moveInput = 1f;
                    }
                }
            }
        }

        Vector2 velocity = rb.linearVelocity;
        velocity.x = moveInput * moveSpeed;
        rb.linearVelocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            Jump();
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            GameOver();
        }
    }
    void WrapAroundScreen()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPos.x < 0f)
        {
            viewportPos.x = 1f;
        }
        else if (viewportPos.x > 1f)
        {
            viewportPos.x = 0f;
        }

        transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
    }
    void Jump()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.y = jumpForce;
        rb.linearVelocity = velocity;
    }

    void GameOver()
    {
        GameManager.Instance.GameOver();
    }
}
