using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField] WeaponSO weaponSO; // Al�nacak silah�n ScriptableObject t�r�.

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        if (weaponSO != null) // E�er weaponSO null de�ilse, yani ge�erli bir silah varsa.
        {
            activeWeapon.SwitchWeapon(weaponSO); // Yeni silah� aktif silah olarak de�i�tirir.

            Inventory inventory = activeWeapon.GetComponentInParent<Inventory>(); // Oyuncunun envanterini almak i�in ActiveWeapon bile�eninin �st�ndeki Inventory bile�enini al�r.
            if (inventory != null && inventory.PlayerInventory != null) // E�er envanter var ve oyuncunun envanteri mevcutsa.
            {
                inventory.PlayerInventory.AddItem(weaponSO); // Yeni silah� oyuncunun envanterine ekler.
            }
        }
    }
}
