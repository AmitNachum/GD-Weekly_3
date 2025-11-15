# 🎮 Game Development Assignment – Final README

This README summarizes all the features added in the current assignment and includes short code samples taken directly from the project files. The goal is to clearly explain the new mechanics, how they were implemented, and where in the code they can be found.

---

## ⭐ Feature 1(Not In the Assignment's Ideas List): Exponential Acceleration Movement

*Instead of constant Movment, the Player's Movment gradually increase under the condition that one of the movement keys has been pressed.*

### Code Example:
```csharp

 private void Update()
 {
     Vector2 moveDirection = move.ReadValue<Vector2>();

     // Are we actually pressing a movement key?
     isMoving = moveDirection.sqrMagnitude > 0.0001f;

    if(!isMoving)
     {
         currentSpeed = initialSpeed;
         rb.linearVelocity = Vector2.zero;
         return;
     }
     // ✔ Correct way with Rigidbody2D: velocity = direction * speed

     currentSpeed *= Mathf.Pow(gradualIncrease, Time.deltaTime);

     currentSpeed = Mathf.Min(currentSpeed, maxSpeed);


     rb.linearVelocity = moveDirection.normalized * currentSpeed;
}
```
### - The main point here is to use rb.velocity instead of transform.position in order to include accurate physics and to combine the properties that the RigidBody2D component grants us.
---

## ⭐ Feature 2: Heart Regeneration Based on Score(Not In the Assignment's Ideas List)

The player automatically regenerates 1 heart after reaching a certain score milestone (every **15 points**).

### 🔍 Description

Instead of regenerating health through pickups, the player now regains hearts by performing well — earning score. This rewards skill and encourages active gameplay.

### 🧩 Code Example (from `PlayerScoreTracker.cs`)

```csharp
public void AddScore(int scoreToAdd)
{
    if (score % scoreToRegenerate == 0 && PlayerHealth.playerHealth < 3)
    {
        playerHealth.Regenerate(); // restores heart
    }

    score += scoreToAdd;
    text.text = score.ToString();
}
```

*Every 15 points, if the player is not at full health, one heart is restored.*

---

## ⭐ Feature 3: Random Shield Generator (Power-up Spawning)

The shield now spawns randomly with a probability check every fixed time interval.

### 🔍 Description

The game attempts to spawn a shield every `checkInterval` seconds. Each attempt succeeds with a probability defined by `spawnProbability`.

### 🧩 Code Example (from `PowerUpManager.cs`)

```csharp
if (timer > checkInterval)
{
    timer = 0f;
    GeneratePosition();
}

private void GeneratePosition()
{
    if (Random.value <= spawnProbability)
    {
        float y = Random.Range(BottomBound, TopBound);
        float x = Random.Range(LeftBound, RightBound);
        SpawnShield(x, y);
    }
}
```

*This ensures shields appear unpredictably but fairly across the game map.*

---

## ⭐ Feature 4: Shield Activation System

When the player touches a shield, they become temporarily invincible.

### 🔍 Description

Once activated:

* The shield object becomes invisible
* The player cannot take damage for **`shieldTimer` seconds**
* After time expires, the shield deactivates automatically

### 🧩 Code Example (from `ShieldCollision.cs`)

```csharp
IEnumerator ActivateShield()
{
    isShieldActive = true;

    var sr = GetComponent<SpriteRenderer>();
    if (sr != null) sr.enabled = false;

    var col = GetComponent<Collider2D>();
    if (col != null) col.enabled = false;

    yield return new WaitForSeconds(shieldTimer);

    isShieldActive = false;
    Destroy(gameObject);
}
```

*This creates a fully timed power-up effect controlled by a simple coroutine.*

---

## ⭐ Feature 5: Player Takes Damage Only When Shield is Inactive

The collision system ensures that the player only receives damage when the shield is turned **off**.

### 🧩 Code Example (from `PlayerCollision.cs`)

```csharp
if (collision.gameObject.CompareTag("Enemy") && !ShieldCollision.isShieldActive)
{
    PlayerHealth.TakeDamage();
}
```

*This ties the shield system into the core gameplay loop.*

---

## ⭐ Feature 6: Player Health System (Hearts UI)

A simple and effective heart-based health system.

### 🧩 Code Example (from `PlayerHealth.cs`)

```csharp
public void TakeDamage()
{
    if (playerHealth > 0)
    {
        playerHealth--;
        hearts[playerHealth].enabled = false;
    }
    if (playerHealth <= 0)
    {
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
```

*Hearts appear/disappear visually in the UI as health changes.*

---

## ⭐ Feature 7: Weapon Combo Switching System

The player can unlock different weapons by pressing specific key sequences (like ABC, ABD, ABF).

### 🧍‍♂️ How It Works

* The system listens to the last 3 pressed keys
* If they match a known combo → weapon switches instantly

### 🧩 Code Example and Keys (from `SwitchSystem.cs`)

```csharp
if (keyToWeapon.TryGetValue(combo, out weapon))
{
    keysPressed.Clear();
    return true;
}
```


### Keys:
    "abc" -> Blue Laser
    "abf" -> Burst green Laser
    "abd" -> Yellow Big Rocket
    
*Enables dynamic weapon switching without a UI.*

---

## ⭐ Feature 8: Multiple Weapon Shooting Modes

Three weapon types exist:

* Laser
* Rocket
* Burst

### 🧩 Code Example (from `ShootingScript.cs`)

```csharp
switch (currentWeapon)
{
    case Weapon.Laser: ShootLaser(); break;
    case Weapon.Rocket: ShootRocket(); break;
    case Weapon.Burst: ShootBurst(); break;
}
```

*This gives variety and supports the combo-switch mechanic.*



## ✔ Summary

This assignment includes significant improvements to player experience:

* **Dynamic health management** (hearts + auto-regeneration based on score)
* **Temporary shield** system
* **Randomized power-up spawning**
* **Combo-based weapon switching**
* **Multi-weapon shooting mechanics**
* **Full damage**

Link to itch.io:("Game's Link")[https://amit-and-gal.itch.io/spaceshipprototype]


