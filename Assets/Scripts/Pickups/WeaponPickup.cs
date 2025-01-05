using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField] WeaponSO weaponSO; // Alýnacak silahýn ScriptableObject türü.

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        if (weaponSO != null) // Eðer weaponSO null deðilse, yani geçerli bir silah varsa.
        {
            activeWeapon.SwitchWeapon(weaponSO); // Yeni silahý aktif silah olarak deðiþtirir.

            Inventory inventory = activeWeapon.GetComponentInParent<Inventory>(); // Oyuncunun envanterini almak için ActiveWeapon bileþeninin üstündeki Inventory bileþenini alýr.
            if (inventory != null && inventory.PlayerInventory != null) // Eðer envanter var ve oyuncunun envanteri mevcutsa.
            {
                inventory.PlayerInventory.AddItem(weaponSO); // Yeni silahý oyuncunun envanterine ekler.
            }
        }
    }
}
