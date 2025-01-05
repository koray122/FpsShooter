using StarterAssets; // StarterAssets kütüphanesini kullanýr, oyuncu kontrolleri için.
using UnityEngine; // Unity API'sini kullanabilmek için gerekli kütüphane.
using UnityEngine.AI; // NavMeshAgent kullanýmý için gerekli kütüphane.

public class Robot : MonoBehaviour // Robot sýnýfý, bir robotun davranýþlarýný kontrol eder.
{
    FirstPersonController player; // Oyuncu karakterini tutacak deðiþken.
    NavMeshAgent agent; // Robotun hareketini yönlendirecek NavMeshAgent bileþeni.

    const string PLAYER_STRING = "Player"; // Oyuncu objesinin etiketini tutan sabit.

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); // Robotun NavMeshAgent bileþenini alýr.
    }

    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>(); // Oyuncu karakterini bulur.
    }

    void Update()
    {
        if (!player) return; // Eðer oyuncu bulunamazsa, iþlem yapýlmaz.

        agent.SetDestination(player.transform.position); // Robotu, oyuncunun bulunduðu pozisyona yönlendirir.
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING)) // Eðer robot oyuncuya çarparsa:
        {
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>(); // Robotun saðlýk bileþenini alýr.
            enemyHealth.SelfDestruct(); // Kendini yok eder (self-destruct).
        }
    }
}
