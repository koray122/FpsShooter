using StarterAssets; // StarterAssets k�t�phanesini kullan�r, oyuncu kontrolleri i�in.
using UnityEngine; // Unity API'sini kullanabilmek i�in gerekli k�t�phane.
using UnityEngine.AI; // NavMeshAgent kullan�m� i�in gerekli k�t�phane.

public class Robot : MonoBehaviour // Robot s�n�f�, bir robotun davran��lar�n� kontrol eder.
{
    FirstPersonController player; // Oyuncu karakterini tutacak de�i�ken.
    NavMeshAgent agent; // Robotun hareketini y�nlendirecek NavMeshAgent bile�eni.

    const string PLAYER_STRING = "Player"; // Oyuncu objesinin etiketini tutan sabit.

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); // Robotun NavMeshAgent bile�enini al�r.
    }

    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>(); // Oyuncu karakterini bulur.
    }

    void Update()
    {
        if (!player) return; // E�er oyuncu bulunamazsa, i�lem yap�lmaz.

        agent.SetDestination(player.transform.position); // Robotu, oyuncunun bulundu�u pozisyona y�nlendirir.
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING)) // E�er robot oyuncuya �arparsa:
        {
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>(); // Robotun sa�l�k bile�enini al�r.
            enemyHealth.SelfDestruct(); // Kendini yok eder (self-destruct).
        }
    }
}
