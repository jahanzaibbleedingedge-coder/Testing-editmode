using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.TestTools;

public class TestCaluculations
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestCaluculationsComboMath()
    {
        //Regular test case
        float damage = CombatMath.CalcDamage(10f, 4f);
        Assert.AreEqual(8f, damage);
        // Use the Assert class to test conditions


        // Check results not getting into negative value
        damage = CombatMath.CalcDamage(10f, 20f);
        Assert.AreEqual(0f, damage);
    }


    [Test]
    public void TestCaluculationsInventory()
    {
        
        Inventory inventory = new Inventory();
        for (int i = 0; i < 5; i++)        {
            bool added = inventory.AddItem();
            Assert.IsTrue(added);
        }
        // Check that adding more than max slots fails
        bool addedExtra = inventory.AddItem();
        Assert.IsFalse(addedExtra);
        // Check that count is correct
        Assert.AreEqual(5, inventory.Count);

    }
    
    [Test]
    public void TestCaluculationsPlayerStats()
    {
        PlayerStats stats = new PlayerStats { strength = 10, agility = 5, level = 2 };
        int totalPower = stats.TotalPower;
        Assert.AreEqual(30, totalPower);   
    } 

   [Test]
    public void WeaponData_ValidConfig_GetCritDamageAndIsValidWork()
    {
        // Create ScriptableObject instance
        WeaponData weapon = ScriptableObject.CreateInstance<WeaponData>();
        
        // Setup valid configuration
        weapon.baseDamage = 100f;
        weapon.critMultiplier = 2f;
        
        // Test GetCritDamage
        Assert.AreEqual(200f, weapon.GetCritDamage(), "Crit damage calculation is incorrect");
        
        // Test IsValid
        Assert.IsTrue(weapon.IsValid(), "Weapon with valid config should return true");
        
        // Clean up
        ScriptableObject.DestroyImmediate(weapon);
        
    }

[Test]
    public void WeaponData_InvalidConfig_IsValidReturnsFalse()
    {
        // Create ScriptableObject instance
        WeaponData weapon = ScriptableObject.CreateInstance<WeaponData>();
        
        // Test zero base damage
        weapon.baseDamage = 0f;
        weapon.critMultiplier = 1.5f;
        Assert.IsFalse(weapon.IsValid(), "Weapon with zero base damage should be invalid");
        
        // Test negative base damage
        weapon.baseDamage = -50f;
        weapon.critMultiplier = 1.5f;
        Assert.IsFalse(weapon.IsValid(), "Weapon with negative base damage should be invalid");
        
        // Test crit multiplier less than 1
        weapon.baseDamage = 100f;
        weapon.critMultiplier = 0.5f;
        Assert.IsFalse(weapon.IsValid(), "Weapon with crit multiplier < 1 should be invalid");
        
        // Clean up
        ScriptableObject.DestroyImmediate(weapon);
    }

}
