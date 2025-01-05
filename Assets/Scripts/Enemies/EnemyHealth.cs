using UnityEngine; // Unity API'sini kullanabilmek i�in gerekli k�t�phane.

public class EnemyHealth : MonoBehaviour // EnemyHealth s�n�f�, d��manlar�n sa�l���n� y�neten bir script'tir. MonoBehaviour'den t�retilmi�tir, yani Unity'nin oyun objelerine ba�lanabilir ve davran��lar�n� kontrol edebilir.
{
    [SerializeField] GameObject robotExplosionVFX; // D��man �ld���nde patlama efekti yaratacak olan GameObject. Unity Editor'�nde bir patlama prefab'� atanabilir.
    [SerializeField] int startingHealth = 3; // D��man�n ba�lang�� sa�l���. Bu de�er Unity Editor'�nden ayarlanabilir.

    int currentHealth; // D��man�n mevcut sa�l���. Bu de�er, `startingHealth` ile ba�lar ve d��man hasar ald�k�a azal�r.

    GameManager gameManager; // Oyunun genel y�netiminden sorumlu olan GameManager. Bu s�n�f, oyundaki d��man say�s�n� kontrol edebilir.

    void Awake()
    {
        currentHealth = startingHealth; // D��man�n sa�l���, oyun ba�lad���nda `startingHealth` de�erine ayarlan�r.
    }

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>(); // GameManager'� bulur ve referans�n� al�r. Bu, d��man say�s�n� y�netmek i�in kullan�lacakt�r.
        gameManager.AdjustEnemiesLeft(1); // Oyun ba�lat�ld���nda d��man say�s� 1 art�r�l�r. Bu, GameManager'�n envanterindeki d��man say�s�n� g�nceller.
    }

    public void TakeDamage(int amount) // D��man hasar ald���nda �a�r�lan metod. D��man�n sa�l���n� azalt�r.
    {
        currentHealth -= amount; // Hasar miktar� kadar `currentHealth`'i azalt�r.

        if (currentHealth <= 0) // E�er sa�l�k s�f�r veya daha d���kse
        {
            gameManager.AdjustEnemiesLeft(-1); // GameManager'a bildirilir, bu d��man�n �ld���n� belirtir ve d��man say�s�n� azalt�r.
            SelfDestruct(); // D��man kendini yok eder (�l�r).
        }
    }

    public void SelfDestruct() // D��man�n �l�m�n� sim�le eder.
    {
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity); // Patlama efektini yarat�r. Efekt, d��man�n bulundu�u konumda yarat�l�r.
        Destroy(this.gameObject); // D��man objesini sahneden siler.
    }
}
