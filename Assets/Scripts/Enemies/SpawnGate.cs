using System.Collections; // IEnumerator ve coroutines kullanabilmek i�in gerekli k�t�phane.
using UnityEngine; // Unity API'sini kullanabilmek i�in gerekli k�t�phane.

public class SpawnGate : MonoBehaviour // SpawnGate s�n�f�, robotlar�n �retilece�i alan� kontrol eder.
{
    [SerializeField] GameObject robotPrefab; // Robot prefab'�, spawn edilecek robot objesini temsil eder.
    [SerializeField] float spawnTime = 5f; // Robotlar�n ne kadar s�kl�kla spawn olaca��n� belirler (saniye cinsinden).
    [SerializeField] Transform spawnPoint; // Robotlar�n spawn olaca�� noktay� belirler.

    PlayerHealth player; // Oyuncunun sa�l�k bilgilerini tutacak de�i�ken.

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>(); // Oyuncu sa�l�k bile�enini bulur.
        StartCoroutine(SpawnRoutine()); // Spawn i�lemini ba�latan Coroutine'i �al��t�r�r.
    }

    IEnumerator SpawnRoutine() // Coroutine: robotlar� belirli aral�klarla spawn etmek i�in kullan�l�r.
    {
        while (player) // E�er oyuncu varsa (yani oyun devam ediyorsa):
        {
            Instantiate(robotPrefab, spawnPoint.position, transform.rotation); // Robotu spawn noktas�nda olu�turur.
            yield return new WaitForSeconds(spawnTime); // Belirtilen s�re kadar bekler (robotlar�n aral�kla spawn edilmesi i�in).
        }
    }
}
