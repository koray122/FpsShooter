using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] int ammoAmount = 100; // Pickup edilen mühimmat miktarý.

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        if (ammoAmount > 0) // Eðer mühimmat miktarý sýfýrdan büyükse.
        {
            activeWeapon.AdjustAmmo(ammoAmount); // Oyuncunun silahýndaki mühimmat miktarýný, pickup edilen miktarla artýrýr.
        }
    }
}
