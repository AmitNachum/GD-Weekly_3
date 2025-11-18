using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public static int playerHealth = 3;
    [SerializeField] private Image[] hearts;
    
   public void TakeDamage()
    {
        if (playerHealth > 0)
        {
            playerHealth--;
            hearts[playerHealth].enabled = false;
        }
        if (playerHealth <= 0)
        {
            Debug.Log("Player defeated!");
            Destroy(gameObject);
            
        }
    }



    public void Regenerate()
    {
        if (playerHealth < hearts.Length)
        {
            hearts[playerHealth].enabled = true;
            playerHealth++;
        }
    }
}
