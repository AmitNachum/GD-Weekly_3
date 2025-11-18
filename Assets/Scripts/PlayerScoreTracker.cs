using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    int score = 0;
    private int scoreToRegenerate = 15;
    [SerializeField] private PlayerHealth playerHealth;



    public void AddScore(int scoreToAdd)
    {
        if (score % scoreToRegenerate == 0 && PlayerHealth.playerHealth < 3)
        {
            playerHealth.Regenerate();
        }
        score += scoreToAdd;

        text.text = score.ToString();
    }

}
