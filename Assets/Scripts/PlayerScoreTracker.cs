using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    int score = 0;



    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        text.text = score.ToString();
    }
    
}
