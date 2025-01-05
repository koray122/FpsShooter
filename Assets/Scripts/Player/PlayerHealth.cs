using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(1, 10)] // Bu, Inspector'da 'startingHealth' deðiþkeninin deðerini 1 ile 10 arasýnda sýnýrlayan bir aralýktýr.
    [SerializeField] int startingHealth = 5; // Oyuncunun baþlangýç saðlýðý, varsayýlan olarak 5 olarak ayarlanýr.
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera; // Ölüm anýnda aktif olacak sanal kamera.
    [SerializeField] Transform weaponCamera; // Silah kamerasýnýn Transform bileþeni.
    [SerializeField] Image[] shieldBars; // Oyuncunun saðlýk durumunu gösterecek UI barlarý (örneðin, zýrh göstergeleri).
    [SerializeField] GameObject gameOverContainer; // Oyun bittiðinde gösterilecek UI ekraný (game over ekraný).

    int currentHealth; // Oyuncunun mevcut saðlýðý.
    int gameOverVirtualCameraPriority = 20; // Game over ekraný için ölüm kamerasýnýn öncelik deðeri.

    void Awake()
    {
        currentHealth = startingHealth; // Oyuncunun mevcut saðlýðýný baþlangýç saðlýðýna ayarlar.
        AdjustShieldUI(); // Saðlýk çubuðunu, mevcut saðlýða göre ayarlar.
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount; // Saðlýk, alýnan hasar kadar azalýr.
        AdjustShieldUI(); // UI'yi, güncellenmiþ saðlýk durumuna göre ayarlar.

        if (currentHealth <= 0) // Eðer saðlýk 0 veya daha az olursa, oyuncu ölür.
        {
            PlayerGameOver(); // Oyun sonu fonksiyonu çaðrýlýr.
        }
    }

    void PlayerGameOver()
    {
        weaponCamera.parent = null; // Silah kamerasý, oyuncudan ayrýlýr (game over ekraný için).
        deathVirtualCamera.Priority = gameOverVirtualCameraPriority; // Ölüm kamerasýnýn önceliði, game over için artýrýlýr.
        gameOverContainer.SetActive(true); // Game over ekraný görünür hale gelir.
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>(); // Oyuncunun giriþlerini kontrol eden scripti bulur.
        starterAssetsInputs.SetCursorState(false); // Fareyi kilitler (cursor'ý serbest býrakýr).
        Destroy(this.gameObject); // Oyuncu objesini yok eder.
    }

    void AdjustShieldUI()
    {
        for (int i = 0; i < shieldBars.Length; i++) // Her bir saðlýk barý için.
        {
            if (i < currentHealth) // Eðer bar indexi, mevcut saðlýðýn altýnda ise.
            {
                shieldBars[i].gameObject.SetActive(true); // Saðlýk barýný aktif yapar.
            }
            else // Saðlýk barý aktif deðilse.
            {
                shieldBars[i].gameObject.SetActive(false); // Saðlýk barýný pasif yapar.
            }
        }
    }
}
