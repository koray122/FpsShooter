using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class InventorySo : ScriptableObject
{
    public List<Slot> inventorySlots = new List<Slot>(); // Envanterdeki silahlar
    int stackLimit = 4; // Silah y���n� limit

    public void AddItem(WeaponSO weapon)
    {
        // Silah ekleme mant���
        foreach (Slot slot in inventorySlots)
        {
            if (slot.weapon == weapon && slot.itemCount < stackLimit)
            {
                slot.itemCount++;
                return;
            }
        }

        foreach (Slot slot in inventorySlots)
        {
            if (!slot.isFull)
            {
                slot.AddItemToSlot(weapon);
                return;
            }
        }
    }
}

[System.Serializable]
public class Slot
{
    public bool isFull; // Slot dolu mu?
    public int itemCount; // Slot i�indeki silah say�s�
    public WeaponSO weapon; // Silah t�r�

    public void AddItemToSlot(WeaponSO weapon)
    {
        this.weapon = weapon;
        itemCount++;
        if (!weapon.canStackable || itemCount >= 1)
        {
            isFull = true;
        }
    }
}
