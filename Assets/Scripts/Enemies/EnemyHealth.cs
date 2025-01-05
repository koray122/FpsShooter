using UnityEngine; // Unity API'sini kullanabilmek için gerekli kütüphane.

public class EnemyHealth : MonoBehaviour // EnemyHealth sýnýfý, düþmanlarýn saðlýðýný yöneten bir script'tir. MonoBehaviour'den türetilmiþtir, yani Unity'nin oyun objelerine baðlanabilir ve davranýþlarýný kontrol edebilir.
{
    [SerializeField] GameObject robotExplosionVFX; // Düþman öldüðünde patlama efekti yaratacak olan GameObject. Unity Editor'ünde bir patlama prefab'ý atanabilir.
    [SerializeField] int startingHealth = 3; // Düþmanýn baþlangýç saðlýðý. Bu deðer Unity Editor'ünden ayarlanabilir.

    int currentHealth; // Düþmanýn mevcut saðlýðý. Bu deðer, `startingHealth` ile baþlar ve düþman hasar aldýkça azalýr.

    GameManager gameManager; // Oyunun genel yönetiminden sorumlu olan GameManager. Bu sýnýf, oyundaki düþman sayýsýný kontrol edebilir.

    void Awake()
    {
        currentHealth = startingHealth; // Düþmanýn saðlýðý, oyun baþladýðýnda `startingHealth` deðerine ayarlanýr.
    }

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>(); // GameManager'ý bulur ve referansýný alýr. Bu, düþman sayýsýný yönetmek için kullanýlacaktýr.
        gameManager.AdjustEnemiesLeft(1); // Oyun baþlatýldýðýnda düþman sayýsý 1 artýrýlýr. Bu, GameManager'ýn envanterindeki düþman sayýsýný günceller.
    }

    public void TakeDamage(int amount) // Düþman hasar aldýðýnda çaðrýlan metod. Düþmanýn saðlýðýný azaltýr.
    {
        currentHealth -= amount; // Hasar miktarý kadar `currentHealth`'i azaltýr.

        if (currentHealth <= 0) // Eðer saðlýk sýfýr veya daha düþükse
        {
            gameManager.AdjustEnemiesLeft(-1); // GameManager'a bildirilir, bu düþmanýn öldüðünü belirtir ve düþman sayýsýný azaltýr.
            SelfDestruct(); // Düþman kendini yok eder (ölür).
        }
    }

    public void SelfDestruct() // Düþmanýn ölümünü simüle eder.
    {
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity); // Patlama efektini yaratýr. Efekt, düþmanýn bulunduðu konumda yaratýlýr.
        Destroy(this.gameObject); // Düþman objesini sahneden siler.
    }
}
