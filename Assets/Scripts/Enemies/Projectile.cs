using UnityEngine; // Unity API'sini kullanabilmek i�in gerekli k�t�phane.

public class Projectile : MonoBehaviour // Projectile s�n�f�, bir mermi veya projeyi temsil eder. MonoBehaviour s�n�f�ndan t�retilmi�tir, yani Unity taraf�ndan oyun objelerine ba�lanabilir ve davran��lar�n� kontrol edebilir.
{
    [SerializeField] float speed = 30f; // Merminin h�z�n� belirler. Unity Editor �zerinden ayarlanabilir. Varsay�lan de�er: 30f.
    [SerializeField] GameObject projectileHitVFX; // Merminin hedefe �arpt���nda olu�turulacak patlama efektini tutan bir GameObject. Unity Editor �zerinden ayarlanabilir.

    Rigidbody rb; // Rigidbody bile�eni, merminin fiziksel hareketini kontrol eder. �arp��malar, yer�ekimi ve h�z gibi fiziksel etmenleri y�netir.

    int damage; // Merminin verece�i hasar� tutan de�i�ken.

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Bu metod, scriptin ba�l� oldu�u GameObject �zerinde bir Rigidbody bile�eni arar ve onu `rb` de�i�kenine atar.
    }

    void Start()
    {
        rb.linearVelocity = transform.forward * speed; // Bu metod, merminin y�n�n� ve h�z�n� ayarlayarak mermiyi hareket ettirir. `transform.forward`, GameObject'in ileri y�n�n� (z-ekseni) temsil eder. Merminin ba�lang�� h�z�n� ayarlamak i�in `speed` de�eri kullan�l�r.
    }

    public void Init(int damage)
    {
        this.damage = damage; // Merminin verece�i hasar de�eri, `Init()` metodu arac�l���yla d��ar�dan ayarlanabilir.
    }

    void OnTriggerEnter(Collider other)
    {
        // Bu metod, mermi bir Collider ile �arp��t���nda �a�r�l�r.
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>(); // �arp��an objede bir `PlayerHealth` bile�eni olup olmad���n� kontrol eder. E�er varsa, oyuncunun sa�l���n� al�r.

        playerHealth?.TakeDamage(damage); // E�er `PlayerHealth` bile�eni varsa, oyuncuya merminin hasar�n� verir. `?.` operat�r�, `playerHealth` null (bo�) oldu�unda hata vermemesini sa�lar.

        Instantiate(projectileHitVFX, transform.position, Quaternion.identity); // Mermi hedefe �arpt���nda patlama veya etki efekti olu�turur. `projectileHitVFX` GameObject'i, merminin �arpt��� konumda (transform.position) olu�turulur.

        Destroy(this.gameObject); // Mermi, hedefe �arpt���nda yok edilir. Bu, merminin oyundan silinmesini sa�lar.
    }
}
