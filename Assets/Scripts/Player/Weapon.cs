using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash; // Silah ate�lendi�inde ��kan alevi temsil eden partik�l sistemi.
    [SerializeField] LayerMask interactionLayers; // Raycast'in etkile�ime girece�i katmanlar.

    CinemachineImpulseSource impulseSource; // Cinemachine i�in impuls kayna��.

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>(); // Impulse kayna��n�, bu nesnenin bile�enlerinden al�r.
    }

    public void Shoot(WeaponSO weaponSO)
    {
        muzzleFlash.Play(); // Ate� etme an�nda alev partik�l sistemini ba�lat�r.
        impulseSource.GenerateImpulse(); // Cinemachine kamera sars�nt�s�n� olu�turur.

        RaycastHit hit; // Raycast ile �arp��ma bilgisini tutacak de�i�ken.

        // Kameran�n bak�� y�n�nde bir raycast g�nderilir ve etkile�imde bulunan katmanlarla �arp��ma kontrol edilir.
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            // E�er bir �arp��ma ger�ekle�irse, �arp��ma noktas�nda etkiyi g�rsel olarak olu�turur.
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);

            // �arp�lan nesnenin, varsa bir EnemyHealth bile�enini al�r ve d��man sa�l���na zarar verir.
            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage); // E�er EnemyHealth varsa, ona verilen hasar� uygular.
        }
    }
}
