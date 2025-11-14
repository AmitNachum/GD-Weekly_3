using UnityEngine;

public class Bullet : MonoBehaviour
{
    public MeshRenderer bulletRenderer;

    void Awake()
    {
        // Auto-grab if you forgot to assign
        if (bulletRenderer == null)
            bulletRenderer = GetComponentInChildren<MeshRenderer>();

        // Clone the material so we don't edit the shared one
        bulletRenderer.material = new Material(bulletRenderer.material);

        // Set a very obvious color
        bulletRenderer.material.color = Color.red;

        Debug.Log("Bullet Awake: color set to red");
    }
}
