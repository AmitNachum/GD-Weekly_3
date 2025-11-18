using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private GameObject ShieldPrefab;
    [SerializeField] private float TopBound;
    [SerializeField] private float LeftBound;
    [SerializeField] private float RightBound;
    [SerializeField] private float BottomBound;
    [SerializeField] private float checkInterval = 10f;
    [SerializeField] [Range(0f, 1f)] private float spawnProbability = 0.65f;
    private float timer = 0f;


    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > checkInterval)
        {
            timer = 0f;
            GeneratePosition();
        }

    }

    private void GeneratePosition()
    {

        if (Random.value <= spawnProbability)
        {

            float yPosition = Random.Range(TopBound, BottomBound);
            float xPosition = Random.Range(LeftBound, RightBound);
            SpawnShield(xPosition, yPosition);

        }
    }


    void SpawnShield(float x, float y)
    {
        Instantiate(ShieldPrefab, new Vector2(x, y), Quaternion.identity);
    }


}
