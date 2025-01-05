using UnityEngine;
using StarterAssets;
using Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeapon; // Ba�lang��ta oyuncunun sahip olaca�� silah (Scriptable Object).
    [SerializeField] CinemachineVirtualCamera playerFollowCamera; // Oyuncuyu takip eden Cinemachine kameras�.
    [SerializeField] Camera weaponCamera; // Silah�n ba�l� oldu�u kamera.
    [SerializeField] GameObject zoomVignette; // Zoom yap�ld���nda ekran�n etraf�ndaki "vinyet" efekti.
    [SerializeField] TMP_Text ammoText; // Mermi say�s�n� g�stermek i�in kullan�lan UI metni.

    WeaponSO currentWeaponSO; // �u anda aktif olan silah�n Scriptable Object versiyonu.
    Animator animator; // Karakterin animasyonlar�n� y�neten Animator.
    StarterAssetsInputs starterAssetsInputs; // Kullan�c� giri�lerini (tu�lar, fare hareketi vb.) y�neten s�n�f.
    FirstPersonController firstPersonController; // Oyuncu kontrol�n� (y�r�me, ko�ma vb.) y�neten s�n�f.
    Weapon currentWeapon; // �u anda oyuncunun sahip oldu�u silah objesi.

    const string SHOOT_STRING = "Shoot"; // Ate� etme animasyonunun ad�.

    float timeSinceLastShot = 0f; // Son ate� etme ile ge�en zaman� takip eder.
    float defaultFOV; // Ba�lang��taki kameran�n g�r�� a��s�.
    float defaultRotationSpeed; // Ba�lang��taki oyuncunun d�n�� h�z�.
    int currentAmmo; // Mevcut mermi say�s�.

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>(); // Kullan�c� giri�lerini al�r.
        firstPersonController = GetComponentInParent<FirstPersonController>(); // Oyuncu hareketlerini al�r.
        animator = GetComponent<Animator>(); // Animator'u al�r.
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView; // Ba�lang��taki g�r�� a��s�n� kaydeder.
        defaultRotationSpeed = firstPersonController.RotationSpeed; // Ba�lang��taki d�n�� h�z�n� kaydeder.
    }

    void Start()
    {
        SwitchWeapon(startingWeapon); // Ba�lang�� silah�n� aktif eder.
        AdjustAmmo(currentWeaponSO.MagazineSize); // Ba�lang��ta mermi say�s�n� ayarlar.
    }

    void Update()
    {
        HandleShoot(); // Ate� etme i�lemi i�in kontrol.
        HandleZoom(); // Zoom yapma i�lemi i�in kontrol.
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount; // Mermi say�s�n� artt�r�r veya azalt�r.

        if (currentAmmo > currentWeaponSO.MagazineSize) // Mermi say�s� kapasiteyi ge�erse.
        {
            currentAmmo = currentWeaponSO.MagazineSize; // Mermi say�s�n� kapasiteye s�n�rlar.
        }

        ammoText.text = currentAmmo.ToString("D2"); // UI'deki mermi say�s�n� g�nceller.
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon) // E�er mevcut bir silah varsa.
        {
            Destroy(currentWeapon.gameObject); // Mevcut silah� yok eder.
        }

        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>(); // Yeni silah� olu�turur.
        currentWeapon = newWeapon; // Yeni silah� aktif eder.
        this.currentWeaponSO = weaponSO; // Yeni silah�n Scriptable Object'ini kaydeder.
        AdjustAmmo(currentWeaponSO.MagazineSize); // Yeni silah�n mermi kapasitesini ayarlar.
    }

    void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime; // Son ate� etme ile ge�en zaman� hesaplar.

        if (!starterAssetsInputs.shoot) return; // E�er ate� etme tu�una bas�lmam��sa, i�lemi durdurur.

        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0) // Ate� etme ko�ullar�n� kontrol eder.
        {
            currentWeapon.Shoot(currentWeaponSO); // Silah� ate�letir.
            animator.Play(SHOOT_STRING, 0, 0f); // Ate� etme animasyonunu oynat�r.
            timeSinceLastShot = 0f; // Son ate� etme zaman�n� s�f�rlar.
            AdjustAmmo(-1); // Mermi say�s�n� bir azalt�r.
        }

        if (!currentWeaponSO.isAutomatic) // E�er silah otomatik de�ilse.
        {
            starterAssetsInputs.ShootInput(false); // Ate� etme giri�ini durdurur.
        }
    }

    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return; // E�er silah zoom yapamazsa, i�lemi durdurur.

        if (starterAssetsInputs.zoom) // E�er zoom tu�una bas�lm��sa.
        {
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount; // Kamera g�r�� a��s�n� zoom seviyesine ayarlar.
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount; // Silah kameras�n�n g�r�� a��s�n� zoom seviyesine ayarlar.
            zoomVignette.SetActive(true); // Zoom efektini aktifle�tirir.
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed); // Zoom s�ras�nda oyuncunun d�n�� h�z�n� ayarlar.
        }
        else
        {
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV; // Kamera g�r�� a��s�n� eski haline d�nd�r�r.
            weaponCamera.fieldOfView = defaultFOV; // Silah kameras�n�n g�r�� a��s�n� eski haline d�nd�r�r.
            zoomVignette.SetActive(false); // Zoom efekti devre d��� b�rak�l�r.
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed); // Oyuncunun d�n�� h�z�n� eski haline d�nd�r�r.
        }
    }
}
