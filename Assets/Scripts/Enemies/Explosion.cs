using UnityEngine; // Unity API'sini kullanabilmek için gerekli kütüphane.

public class Explosion : MonoBehaviour // Explosion sýnýfý, patlama alanýndaki oyunculara hasar vermek için kullanýlýr. MonoBehaviour sýnýfýndan türetilmiþtir, yani Unity tarafýndan oyun objelerine baðlanabilir ve davranýþlarýný kontrol edebilir.
{
    [SerializeField] float radius = 1.5f; // Patlama yarýçapýný tanýmlar. Unity Editor üzerinden ayarlanabilir. Varsayýlan deðer: 1.5.
    [SerializeField] int damage = 3; // Patlamanýn vereceði hasarý belirtir. Unity Editor'den ayarlanabilir. Varsayýlan deðer: 3.

    void Start() // Unity'nin yaþam döngüsünde Start metodu, sahne yüklendiðinde veya script aktif olduðunda çaðrýlýr.
    {
        Explode(); // Patlama fonksiyonunu baþlatýr.
    }

    void OnDrawGizmos() // Bu metod, editor'de oyun sahnesi görüntülenirken yardýmcý görsel nesneler çizmek için kullanýlýr. Bu örnekte, patlama alanýný görselleþtirir.
    {
        Gizmos.color = Color.red; // Çizim rengi kýrmýzý olarak ayarlanýr.
        Gizmos.DrawWireSphere(transform.position, radius); // Bu metod, patlama yarýçapý çevresinde bir küre çizer. Yalnýzca sahnede görünür, oyun sýrasýnda görünmez.
    }

    void Explode() // Patlama iþlemi yapýlýr. Bu metod, patlama alanýndaki objeleri kontrol eder ve yakýnlardaki oyunculara hasar verir.
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius); // Bu metod, patlamanýn merkezinden belirli bir mesafede (radius) olan tüm çarpanlarý (Collider) döndürür. 

        foreach (Collider hitCollider in hitColliders) // Tüm çarpanlarý kontrol eder.
        {
            PlayerHealth playerhealth = hitCollider.GetComponent<PlayerHealth>(); // Eðer çarpan bir oyuncu objesi ise, oyuncunun saðlýk durumunu alýr.

            if (!playerhealth) continue; // Eðer oyuncunun saðlýðý yoksa, bu çarpan geçilir. Yani, patlama oyuncularla etkileþime girer.

            playerhealth.TakeDamage(damage); // Patlama, oyuncuya belirtilen hasarý verir. 

            break; // Ýlk oyuncuya hasar verdikten sonra patlama iþini sonlandýrýr. Bu, patlamanýn yalnýzca ilk oyuncuya etki etmesini saðlar (birden fazla oyuncu varsa, sadece ilk oyuncuya etki eder).
        }
    }
}
