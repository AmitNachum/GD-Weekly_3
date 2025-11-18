using UnityEngine;

public class HeartCollide : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        // Try on the object itself
        PlayerHealth health = collision.GetComponent<PlayerHealth>();

        // If not there, try on a parent (typical case: collider is on a child)
        if (health == null)
            health = collision.GetComponentInParent<PlayerHealth>();

        if (health != null)
        {
            health.Regenerate();
            Debug.Log("Heart Regenerate");
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Problem PlayerHealth is NULL");
        }
    }
}
