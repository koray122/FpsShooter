using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash; // Silah ateþlendiðinde çýkan alevi temsil eden partikül sistemi.
    [SerializeField] LayerMask interactionLayers; // Raycast'in etkileþime gireceði katmanlar.

    CinemachineImpulseSource impulseSource; // Cinemachine için impuls kaynaðý.

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>(); // Impulse kaynaðýný, bu nesnenin bileþenlerinden alýr.
    }

    public void Shoot(WeaponSO weaponSO)
    {
        muzzleFlash.Play(); // Ateþ etme anýnda alev partikül sistemini baþlatýr.
        impulseSource.GenerateImpulse(); // Cinemachine kamera sarsýntýsýný oluþturur.

        RaycastHit hit; // Raycast ile çarpýþma bilgisini tutacak deðiþken.

        // Kameranýn bakýþ yönünde bir raycast gönderilir ve etkileþimde bulunan katmanlarla çarpýþma kontrol edilir.
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            // Eðer bir çarpýþma gerçekleþirse, çarpýþma noktasýnda etkiyi görsel olarak oluþturur.
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);

            // Çarpýlan nesnenin, varsa bir EnemyHealth bileþenini alýr ve düþman saðlýðýna zarar verir.
            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage); // Eðer EnemyHealth varsa, ona verilen hasarý uygular.
        }
    }
}
