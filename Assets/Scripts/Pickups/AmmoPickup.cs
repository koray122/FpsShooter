using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] int ammoAmount = 100; // Pickup edilen m�himmat miktar�.

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        if (ammoAmount > 0) // E�er m�himmat miktar� s�f�rdan b�y�kse.
        {
            activeWeapon.AdjustAmmo(ammoAmount); // Oyuncunun silah�ndaki m�himmat miktar�n�, pickup edilen miktarla art�r�r.
        }
    }
}
