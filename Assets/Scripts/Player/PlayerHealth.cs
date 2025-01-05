using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(1, 10)] // Bu, Inspector'da 'startingHealth' de�i�keninin de�erini 1 ile 10 aras�nda s�n�rlayan bir aral�kt�r.
    [SerializeField] int startingHealth = 5; // Oyuncunun ba�lang�� sa�l���, varsay�lan olarak 5 olarak ayarlan�r.
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera; // �l�m an�nda aktif olacak sanal kamera.
    [SerializeField] Transform weaponCamera; // Silah kameras�n�n Transform bile�eni.
    [SerializeField] Image[] shieldBars; // Oyuncunun sa�l�k durumunu g�sterecek UI barlar� (�rne�in, z�rh g�stergeleri).
    [SerializeField] GameObject gameOverContainer; // Oyun bitti�inde g�sterilecek UI ekran� (game over ekran�).

    int currentHealth; // Oyuncunun mevcut sa�l���.
    int gameOverVirtualCameraPriority = 20; // Game over ekran� i�in �l�m kameras�n�n �ncelik de�eri.

    void Awake()
    {
        currentHealth = startingHealth; // Oyuncunun mevcut sa�l���n� ba�lang�� sa�l���na ayarlar.
        AdjustShieldUI(); // Sa�l�k �ubu�unu, mevcut sa�l��a g�re ayarlar.
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount; // Sa�l�k, al�nan hasar kadar azal�r.
        AdjustShieldUI(); // UI'yi, g�ncellenmi� sa�l�k durumuna g�re ayarlar.

        if (currentHealth <= 0) // E�er sa�l�k 0 veya daha az olursa, oyuncu �l�r.
        {
            PlayerGameOver(); // Oyun sonu fonksiyonu �a�r�l�r.
        }
    }

    void PlayerGameOver()
    {
        weaponCamera.parent = null; // Silah kameras�, oyuncudan ayr�l�r (game over ekran� i�in).
        deathVirtualCamera.Priority = gameOverVirtualCameraPriority; // �l�m kameras�n�n �nceli�i, game over i�in art�r�l�r.
        gameOverContainer.SetActive(true); // Game over ekran� g�r�n�r hale gelir.
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>(); // Oyuncunun giri�lerini kontrol eden scripti bulur.
        starterAssetsInputs.SetCursorState(false); // Fareyi kilitler (cursor'� serbest b�rak�r).
        Destroy(this.gameObject); // Oyuncu objesini yok eder.
    }

    void AdjustShieldUI()
    {
        for (int i = 0; i < shieldBars.Length; i++) // Her bir sa�l�k bar� i�in.
        {
            if (i < currentHealth) // E�er bar indexi, mevcut sa�l���n alt�nda ise.
            {
                shieldBars[i].gameObject.SetActive(true); // Sa�l�k bar�n� aktif yapar.
            }
            else // Sa�l�k bar� aktif de�ilse.
            {
                shieldBars[i].gameObject.SetActive(false); // Sa�l�k bar�n� pasif yapar.
            }
        }
    }
}
