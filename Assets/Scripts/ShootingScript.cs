using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{
    public enum Weapon { Laser, Rocket, Burst }
    [SerializeField] private Weapon currentWeapon;

    [Header("Prefabs")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private GameObject burstPrefab;

    [Header("General")]
    [SerializeField] private Transform firePoint;

    [Header("Laser")]
    [SerializeField] private float laserSpeed = 10f;

    [Header("Rocket")]
    [SerializeField] private float rocketSpeed = 4f;
    [SerializeField] private float rocketCooldown = 0.7f;
    private float rocketTimer = 0f;

    [Header("Burst")]
    [SerializeField] private float burstSpeed = 8f;
    [SerializeField] private int burstCount = 3;
    [SerializeField] private float burstSpread = 15f;

    [Header("Combo System")]
    [SerializeField] private SwitchSystem switchSystem;

    private InputAction shoot = new InputAction(
        type: InputActionType.Button,
        binding: "<Keyboard>/space");

    void OnEnable()
    {
        shoot.Enable();
    }

    void OnDisable()
    {
        shoot.Disable();
    }

    void Update()
    {
        rocketTimer -= Time.deltaTime;

      
        if (switchSystem != null &&
            switchSystem.TryGetCombo(out Weapon newWeapon))
        {
            currentWeapon = newWeapon;
            Debug.Log("Current weapon: " + currentWeapon);
        }

        if (shoot.WasPressedThisFrame())
            Shoot();
    }

    void Shoot()
    {
        switch (currentWeapon)
        {
            case Weapon.Laser: ShootLaser(); break;
            case Weapon.Rocket: ShootRocket(); break;
            case Weapon.Burst: ShootBurst(); break;
        }
    }

    void ShootLaser()
    {
        var b = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        b.GetComponent<Rigidbody2D>().linearVelocity = firePoint.up * laserSpeed;
    }

    void ShootRocket()
    {
        if (rocketTimer > 0f) return;

        var b = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
        b.GetComponent<Rigidbody2D>().linearVelocity = firePoint.up * rocketSpeed;

        rocketTimer = rocketCooldown;
    }

    void ShootBurst()
    {
        for (int i = 0; i < burstCount; i++)
        {
            float offset = i - (burstCount - 1) / 2f;
            float angle = offset * burstSpread;
            var rot = firePoint.rotation * Quaternion.Euler(0, 0, angle);

            var b = Instantiate(burstPrefab, firePoint.position, rot);
            b.GetComponent<Rigidbody2D>().linearVelocity =
                (Vector2)(rot * Vector3.up) * burstSpeed;

        }
    }
}
