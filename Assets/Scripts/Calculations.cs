using UnityEngine;

public class Calculations : MonoBehaviour
{
    
}


public static class CombatMath {
    public static float CalcDamage(float attack, float defense) {
        return Mathf.Max(0f, attack - defense * 0.5f);
    }
}

public class Inventory {
    public int maxSlots = 5;
    private int _count = 0;
    public bool AddItem() {
        if (_count >= maxSlots) return false;
        _count++;
        return true;
    }
    public int Count => _count;
}


public struct PlayerStats {
    public int strength;
    public int agility;
    public int level;
    public int TotalPower => (strength + agility) * level;

}


public class WeaponData : ScriptableObject {
    public float baseDamage;
    public float critMultiplier = 1.5f;
    public float GetCritDamage() => baseDamage * critMultiplier;
    public bool IsValid() => baseDamage > 0 && critMultiplier >= 1f;
}