
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{

    [SerializeField] private PlayerHealth PlayerHealth;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !ShieldCollision.isShieldActive)
        {
            Debug.Log("Player hit!");
            PlayerHealth.TakeDamage();
        }
    }


}

