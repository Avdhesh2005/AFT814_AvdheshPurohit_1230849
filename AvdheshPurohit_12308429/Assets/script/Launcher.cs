using UnityEngine;

public class Launcher : MonoBehaviour
{
    public float launchForce = 20f;
    public GameObject launchVFX;

    private bool playerInRange = false;
    private Rigidbody2D playerRb;
    private Transform playerTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerRb = collision.GetComponent<Rigidbody2D>();
            playerTransform = collision.transform;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerRb = null;
            playerTransform = null;
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            LaunchPlayer();
        }
    }

    private void LaunchPlayer()
    {
        if (playerRb != null && playerTransform != null)
        {
            float direction = playerTransform.position.x < transform.position.x ? -1f : 1f;

            Vector2 launchDir = new Vector2(direction, 0f); // purely horizontal

            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(launchDir * launchForce, ForceMode2D.Impulse);

            if (launchVFX != null)
            {
                Instantiate(launchVFX, playerTransform.position, Quaternion.identity);
            }
        }
    }
}
