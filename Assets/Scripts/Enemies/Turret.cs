using System.Collections; // IEnumerator ve coroutines kullanabilmek i�in gerekli k�t�phane.
using UnityEngine; // Unity API'sini kullanabilmek i�in gerekli k�t�phane.

public class Turret : MonoBehaviour // Turret s�n�f�, bir top (turret) nesnesinin davran��lar�n� kontrol eder.
{
    [SerializeField] GameObject projectilePrefab; // Ate�lenecek olan proje nesnesinin prefab'�.
    [SerializeField] Transform turretHead; // Turret'in ba��n�n (vuraca�� yeri g�sterecek k�sm�) Transform bile�eni.
    [SerializeField] Transform playerTargetPoint; // Oyuncunun hedef noktas�n� belirtir.
    [SerializeField] Transform projectileSpawnPoint; // Projelerin spawn (do�aca��) noktas�.
    [SerializeField] float fireRate = 2f; // Ate�leme h�z�, yani topun her ne kadar aral�klarla ate� edece�i.
    [SerializeField] int damage = 2; // Projelerin verece�i hasar.

    PlayerHealth player; // Oyuncunun sa�l�k bilgilerini tutacak de�i�ken.

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>(); // Oyuncunun sa�l�k bile�enini bulur.
        StartCoroutine(FireRoutine()); // Ate�leme i�lemi i�in coroutine ba�lat�l�r.
    }

    void Update()
    {
        turretHead.LookAt(playerTargetPoint); // Turret'in ba�� oyuncuyu hedef alacak �ekilde d�ner.
    }

    IEnumerator FireRoutine() // Projeleri belirli aral�klarla ate�lemek i�in kullan�lan coroutine fonksiyonu.
    {
        while (player) // E�er oyuncu sa�lamsa (yani oyun devam ediyorsa):
        {
            yield return new WaitForSeconds(fireRate); // Ate� etmeden �nce fireRate kadar bekler.
            Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.transform.LookAt(playerTargetPoint); // Yeni mermi, hedefe do�ru y�nlendirilir.
            newProjectile.Init(damage); // Yeni projeye hasar de�eri atan�r.
        }
    }
}
