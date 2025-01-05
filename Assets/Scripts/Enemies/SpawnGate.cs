using System.Collections; // IEnumerator ve coroutines kullanabilmek için gerekli kütüphane.
using UnityEngine; // Unity API'sini kullanabilmek için gerekli kütüphane.

public class SpawnGate : MonoBehaviour // SpawnGate sýnýfý, robotlarýn üretileceði alaný kontrol eder.
{
    [SerializeField] GameObject robotPrefab; // Robot prefab'ý, spawn edilecek robot objesini temsil eder.
    [SerializeField] float spawnTime = 5f; // Robotlarýn ne kadar sýklýkla spawn olacaðýný belirler (saniye cinsinden).
    [SerializeField] Transform spawnPoint; // Robotlarýn spawn olacaðý noktayý belirler.

    PlayerHealth player; // Oyuncunun saðlýk bilgilerini tutacak deðiþken.

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>(); // Oyuncu saðlýk bileþenini bulur.
        StartCoroutine(SpawnRoutine()); // Spawn iþlemini baþlatan Coroutine'i çalýþtýrýr.
    }

    IEnumerator SpawnRoutine() // Coroutine: robotlarý belirli aralýklarla spawn etmek için kullanýlýr.
    {
        while (player) // Eðer oyuncu varsa (yani oyun devam ediyorsa):
        {
            Instantiate(robotPrefab, spawnPoint.position, transform.rotation); // Robotu spawn noktasýnda oluþturur.
            yield return new WaitForSeconds(spawnTime); // Belirtilen süre kadar bekler (robotlarýn aralýkla spawn edilmesi için).
        }
    }
}
