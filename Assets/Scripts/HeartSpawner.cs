using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private float spawnInterval = 60.0f;
    private float timer = 0.0f;
    [SerializeField] [Range(0,1)] private float spawnHealthProbability = 0.5f;
    private int maxHealth = 3;
    [SerializeField] private float upperBoundX = 7.0f;
    [SerializeField] private float lowerBoundX = -7.0f;
    [SerializeField] private float upperBoundY = 3.0f;
    [SerializeField] private float lowerBoundY = -3.0f;
    public Canvas canvasUI;


    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnInterval)
        {
            float xPos = Random.Range(lowerBoundX, upperBoundX);
            float yPos = Random.Range(lowerBoundY,upperBoundY);
            SpawnHeart(xPos,yPos);
            timer = 0.0f;
        }
    }

    private void SpawnHeart(float x,float y)
    {

        if (Random.value <= spawnHealthProbability && PlayerHealth.playerHealth < maxHealth)
        {
            Vector3 spawnPos = new Vector3(x, y, 0f);
            Instantiate(heartPrefab, spawnPos, Quaternion.identity);
            Debug.Log("Heart spawned at: " + x + ", " + y);
        }


    }

}
