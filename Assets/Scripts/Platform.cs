using UnityEngine;

public class Platform : MonoBehaviour
{
    private CameraFollow cameraFollow;
    private bool counted = false;

    void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    void Update()
    {
        if (!counted && GameManager.Instance != null && GameManager.Instance.player.transform.position.y > transform.position.y)
        {
            GameManager.Instance.AddScore(1);
            counted = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f)
                {
                    cameraFollow?.UpdateHighestY(transform.position.y);
                    break;
                }
            }
        }
    }
}
