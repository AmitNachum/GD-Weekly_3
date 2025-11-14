using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private GameObject ShieldPrefab;
    [SerializeField] private float TopBound;
    [SerializeField] private float LeftBound;
    [SerializeField] private float RightBound;
    [SerializeField] private float BottomBound;
    [SerializeField] private float checkInterval = 5f;
    private float timer = 0f;
    

    private void Update()
    {
        timer += Time.deltaTime;

        if( timer > checkInterval)
        {
            timer = 0f;
            GeneratePosition();
        }

    }

    private void GeneratePosition()
    {
        bool[] distributedBooleans = { true, false, false, false, false, false, false, false, false, false };

        int randomIndex = (int)Random.value % distributedBooleans.Length;

        if (!distributedBooleans[randomIndex]) return;


        float xPosition = Random.Range(LeftBound, RightBound);
        float yPosition = Random.Range(TopBound, BottomBound);

        SpawnShield(xPosition, yPosition);
    }


    void SpawnShield(float x,float y)
    {
        Instantiate(ShieldPrefab,new Vector2(x,y),Quaternion.identity);
    }

    [ContextMenu("OnClick")]
    void onClick()
    {
        SpawnShield(2, 1);
    }
}
