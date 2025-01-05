using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f; // Pickup objesinin dönüþ hýzýný belirler.

    const string PLAYER_STRING = "Player"; // "Player" etiketine sahip nesneleri kontrol eder.

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f); // Pickup objesini Y ekseninde döndürür.
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING)) // Eðer çarpan nesne "Player" etiketine sahipse.
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>(); // Oyuncunun aktif silahýný alýr.
            OnPickup(activeWeapon); // OnPickup metodunu çaðýrarak silah üzerinde bir iþlem yapar.
            Destroy(this.gameObject); // Pickup objesini yok eder.
        }
    }

    // "abstract" terimi burada önemli. Bu metod soyut bir metodtur.
    // "abstract" metodlar, sadece baþlýklarý tanýmlar, içeriði ise alt sýnýflarda yazýlmalýdýr.
    protected abstract void OnPickup(ActiveWeapon activeWeapon); // Pickup objesi alýndýðýnda yapýlacak iþlemi tanýmlar (soyut metod).
}
