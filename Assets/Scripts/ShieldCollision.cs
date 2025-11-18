using UnityEngine;
using System.Collections;
public class ShieldCollision : MonoBehaviour
{
    [SerializeField] private int shieldTimer = 5;
    [SerializeField] private GameObject shield;
    public static bool isShieldActive = false;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isShieldActive)
            {
                Debug.Log("Shield hit!");
                StartCoroutine(ActivateShield());

            }

        }
    }

    IEnumerator ActivateShield()
    {
        isShieldActive = true;


        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        yield return new WaitForSeconds(shieldTimer);

        isShieldActive = false;


        Destroy(gameObject);
    }

}
