using UnityEngine; // Unity API'sini kullanabilmek i�in gerekli k�t�phane.

public class Explosion : MonoBehaviour // Explosion s�n�f�, patlama alan�ndaki oyunculara hasar vermek i�in kullan�l�r. MonoBehaviour s�n�f�ndan t�retilmi�tir, yani Unity taraf�ndan oyun objelerine ba�lanabilir ve davran��lar�n� kontrol edebilir.
{
    [SerializeField] float radius = 1.5f; // Patlama yar��ap�n� tan�mlar. Unity Editor �zerinden ayarlanabilir. Varsay�lan de�er: 1.5.
    [SerializeField] int damage = 3; // Patlaman�n verece�i hasar� belirtir. Unity Editor'den ayarlanabilir. Varsay�lan de�er: 3.

    void Start() // Unity'nin ya�am d�ng�s�nde Start metodu, sahne y�klendi�inde veya script aktif oldu�unda �a�r�l�r.
    {
        Explode(); // Patlama fonksiyonunu ba�lat�r.
    }

    void OnDrawGizmos() // Bu metod, editor'de oyun sahnesi g�r�nt�lenirken yard�mc� g�rsel nesneler �izmek i�in kullan�l�r. Bu �rnekte, patlama alan�n� g�rselle�tirir.
    {
        Gizmos.color = Color.red; // �izim rengi k�rm�z� olarak ayarlan�r.
        Gizmos.DrawWireSphere(transform.position, radius); // Bu metod, patlama yar��ap� �evresinde bir k�re �izer. Yaln�zca sahnede g�r�n�r, oyun s�ras�nda g�r�nmez.
    }

    void Explode() // Patlama i�lemi yap�l�r. Bu metod, patlama alan�ndaki objeleri kontrol eder ve yak�nlardaki oyunculara hasar verir.
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius); // Bu metod, patlaman�n merkezinden belirli bir mesafede (radius) olan t�m �arpanlar� (Collider) d�nd�r�r. 

        foreach (Collider hitCollider in hitColliders) // T�m �arpanlar� kontrol eder.
        {
            PlayerHealth playerhealth = hitCollider.GetComponent<PlayerHealth>(); // E�er �arpan bir oyuncu objesi ise, oyuncunun sa�l�k durumunu al�r.

            if (!playerhealth) continue; // E�er oyuncunun sa�l��� yoksa, bu �arpan ge�ilir. Yani, patlama oyuncularla etkile�ime girer.

            playerhealth.TakeDamage(damage); // Patlama, oyuncuya belirtilen hasar� verir. 

            break; // �lk oyuncuya hasar verdikten sonra patlama i�ini sonland�r�r. Bu, patlaman�n yaln�zca ilk oyuncuya etki etmesini sa�lar (birden fazla oyuncu varsa, sadece ilk oyuncuya etki eder).
        }
    }
}
