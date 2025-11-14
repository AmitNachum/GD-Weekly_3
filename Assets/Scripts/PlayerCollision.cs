
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private int playerHealth = 3;
    [SerializeField] private Image[] hearts;
    



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !ShieldCollision.isShieldActive)
       {
           Debug.Log("Player hit!");
            TakeDamage();
       }
    }

    void TakeDamage()
    {
        if(playerHealth > 0)
        {
            playerHealth--;
            hearts[playerHealth].enabled = false;
        }
        if(playerHealth <= 0)
        {
            Debug.Log("Player defeated!");
           Destroy(gameObject);
        }
    }
}

