using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchSystem : MonoBehaviour
{
    private Dictionary<string, ShootingScript.Weapon> keyToWeapon =
        new Dictionary<string, ShootingScript.Weapon>()
    {
        { "abc", ShootingScript.Weapon.Laser },
        { "abd", ShootingScript.Weapon.Rocket },
        { "abf", ShootingScript.Weapon.Burst }
    };

    private List<Key> keysPressed = new List<Key>();

    // ShootingScript will call this every frame
    public bool TryGetCombo(out ShootingScript.Weapon weapon)
    {
        weapon = default;

        // listen only to keys we care about
        Key[] watchedKeys = { Key.A, Key.B, Key.C, Key.D, Key.F };

        foreach (Key key in watchedKeys)
        {
            var k = Keyboard.current[key];
            if (k != null && k.wasPressedThisFrame)
            {
                keysPressed.Add(key);
            }
        }

        // keep only last 3 keys
        while (keysPressed.Count > 3)
            keysPressed.RemoveAt(0);

        if (keysPressed.Count < 3)
            return false;

        // build combo string like "abc"
        string combo = "";
        for (int i = 0; i < keysPressed.Count; i++)
        {
            combo += keysPressed[i].ToString().ToLower(); // "a","b","c"
        }

        if (keyToWeapon.TryGetValue(combo, out weapon))
        {
            Debug.Log("Combo matched: " + combo + " → " + weapon);
            keysPressed.Clear(); // reset after match
            return true;
        }

        return false;
    }
}
