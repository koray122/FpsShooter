using System.Collections; // IEnumerator ve coroutines kullanabilmek için gerekli kütüphane.
using UnityEngine; // Unity API'sini kullanabilmek için gerekli kütüphane.

public class Turret : MonoBehaviour // Turret sýnýfý, bir top (turret) nesnesinin davranýþlarýný kontrol eder.
{
    [SerializeField] GameObject projectilePrefab; // Ateþlenecek olan proje nesnesinin prefab'ý.
    [SerializeField] Transform turretHead; // Turret'in baþýnýn (vuracaðý yeri gösterecek kýsmý) Transform bileþeni.
    [SerializeField] Transform playerTargetPoint; // Oyuncunun hedef noktasýný belirtir.
    [SerializeField] Transform projectileSpawnPoint; // Projelerin spawn (doðacaðý) noktasý.
    [SerializeField] float fireRate = 2f; // Ateþleme hýzý, yani topun her ne kadar aralýklarla ateþ edeceði.
    [SerializeField] int damage = 2; // Projelerin vereceði hasar.

    PlayerHealth player; // Oyuncunun saðlýk bilgilerini tutacak deðiþken.

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>(); // Oyuncunun saðlýk bileþenini bulur.
        StartCoroutine(FireRoutine()); // Ateþleme iþlemi için coroutine baþlatýlýr.
    }

    void Update()
    {
        turretHead.LookAt(playerTargetPoint); // Turret'in baþý oyuncuyu hedef alacak þekilde döner.
    }

    IEnumerator FireRoutine() // Projeleri belirli aralýklarla ateþlemek için kullanýlan coroutine fonksiyonu.
    {
        while (player) // Eðer oyuncu saðlamsa (yani oyun devam ediyorsa):
        {
            yield return new WaitForSeconds(fireRate); // Ateþ etmeden önce fireRate kadar bekler.
            Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.transform.LookAt(playerTargetPoint); // Yeni mermi, hedefe doðru yönlendirilir.
            newProjectile.Init(damage); // Yeni projeye hasar deðeri atanýr.
        }
    }
}
