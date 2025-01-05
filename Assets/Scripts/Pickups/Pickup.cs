using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f; // Pickup objesinin d�n�� h�z�n� belirler.

    const string PLAYER_STRING = "Player"; // "Player" etiketine sahip nesneleri kontrol eder.

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f); // Pickup objesini Y ekseninde d�nd�r�r.
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING)) // E�er �arpan nesne "Player" etiketine sahipse.
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>(); // Oyuncunun aktif silah�n� al�r.
            OnPickup(activeWeapon); // OnPickup metodunu �a��rarak silah �zerinde bir i�lem yapar.
            Destroy(this.gameObject); // Pickup objesini yok eder.
        }
    }

    // "abstract" terimi burada �nemli. Bu metod soyut bir metodtur.
    // "abstract" metodlar, sadece ba�l�klar� tan�mlar, i�eri�i ise alt s�n�flarda yaz�lmal�d�r.
    protected abstract void OnPickup(ActiveWeapon activeWeapon); // Pickup objesi al�nd���nda yap�lacak i�lemi tan�mlar (soyut metod).
}
