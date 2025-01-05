using UnityEngine;
using StarterAssets;
using Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeapon; // Baþlangýçta oyuncunun sahip olacaðý silah (Scriptable Object).
    [SerializeField] CinemachineVirtualCamera playerFollowCamera; // Oyuncuyu takip eden Cinemachine kamerasý.
    [SerializeField] Camera weaponCamera; // Silahýn baðlý olduðu kamera.
    [SerializeField] GameObject zoomVignette; // Zoom yapýldýðýnda ekranýn etrafýndaki "vinyet" efekti.
    [SerializeField] TMP_Text ammoText; // Mermi sayýsýný göstermek için kullanýlan UI metni.

    WeaponSO currentWeaponSO; // Þu anda aktif olan silahýn Scriptable Object versiyonu.
    Animator animator; // Karakterin animasyonlarýný yöneten Animator.
    StarterAssetsInputs starterAssetsInputs; // Kullanýcý giriþlerini (tuþlar, fare hareketi vb.) yöneten sýnýf.
    FirstPersonController firstPersonController; // Oyuncu kontrolünü (yürüme, koþma vb.) yöneten sýnýf.
    Weapon currentWeapon; // Þu anda oyuncunun sahip olduðu silah objesi.

    const string SHOOT_STRING = "Shoot"; // Ateþ etme animasyonunun adý.

    float timeSinceLastShot = 0f; // Son ateþ etme ile geçen zamaný takip eder.
    float defaultFOV; // Baþlangýçtaki kameranýn görüþ açýsý.
    float defaultRotationSpeed; // Baþlangýçtaki oyuncunun dönüþ hýzý.
    int currentAmmo; // Mevcut mermi sayýsý.

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>(); // Kullanýcý giriþlerini alýr.
        firstPersonController = GetComponentInParent<FirstPersonController>(); // Oyuncu hareketlerini alýr.
        animator = GetComponent<Animator>(); // Animator'u alýr.
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView; // Baþlangýçtaki görüþ açýsýný kaydeder.
        defaultRotationSpeed = firstPersonController.RotationSpeed; // Baþlangýçtaki dönüþ hýzýný kaydeder.
    }

    void Start()
    {
        SwitchWeapon(startingWeapon); // Baþlangýç silahýný aktif eder.
        AdjustAmmo(currentWeaponSO.MagazineSize); // Baþlangýçta mermi sayýsýný ayarlar.
    }

    void Update()
    {
        HandleShoot(); // Ateþ etme iþlemi için kontrol.
        HandleZoom(); // Zoom yapma iþlemi için kontrol.
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount; // Mermi sayýsýný arttýrýr veya azaltýr.

        if (currentAmmo > currentWeaponSO.MagazineSize) // Mermi sayýsý kapasiteyi geçerse.
        {
            currentAmmo = currentWeaponSO.MagazineSize; // Mermi sayýsýný kapasiteye sýnýrlar.
        }

        ammoText.text = currentAmmo.ToString("D2"); // UI'deki mermi sayýsýný günceller.
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon) // Eðer mevcut bir silah varsa.
        {
            Destroy(currentWeapon.gameObject); // Mevcut silahý yok eder.
        }

        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>(); // Yeni silahý oluþturur.
        currentWeapon = newWeapon; // Yeni silahý aktif eder.
        this.currentWeaponSO = weaponSO; // Yeni silahýn Scriptable Object'ini kaydeder.
        AdjustAmmo(currentWeaponSO.MagazineSize); // Yeni silahýn mermi kapasitesini ayarlar.
    }

    void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime; // Son ateþ etme ile geçen zamaný hesaplar.

        if (!starterAssetsInputs.shoot) return; // Eðer ateþ etme tuþuna basýlmamýþsa, iþlemi durdurur.

        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0) // Ateþ etme koþullarýný kontrol eder.
        {
            currentWeapon.Shoot(currentWeaponSO); // Silahý ateþletir.
            animator.Play(SHOOT_STRING, 0, 0f); // Ateþ etme animasyonunu oynatýr.
            timeSinceLastShot = 0f; // Son ateþ etme zamanýný sýfýrlar.
            AdjustAmmo(-1); // Mermi sayýsýný bir azaltýr.
        }

        if (!currentWeaponSO.isAutomatic) // Eðer silah otomatik deðilse.
        {
            starterAssetsInputs.ShootInput(false); // Ateþ etme giriþini durdurur.
        }
    }

    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return; // Eðer silah zoom yapamazsa, iþlemi durdurur.

        if (starterAssetsInputs.zoom) // Eðer zoom tuþuna basýlmýþsa.
        {
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount; // Kamera görüþ açýsýný zoom seviyesine ayarlar.
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount; // Silah kamerasýnýn görüþ açýsýný zoom seviyesine ayarlar.
            zoomVignette.SetActive(true); // Zoom efektini aktifleþtirir.
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed); // Zoom sýrasýnda oyuncunun dönüþ hýzýný ayarlar.
        }
        else
        {
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV; // Kamera görüþ açýsýný eski haline döndürür.
            weaponCamera.fieldOfView = defaultFOV; // Silah kamerasýnýn görüþ açýsýný eski haline döndürür.
            zoomVignette.SetActive(false); // Zoom efekti devre dýþý býrakýlýr.
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed); // Oyuncunun dönüþ hýzýný eski haline döndürür.
        }
    }
}
