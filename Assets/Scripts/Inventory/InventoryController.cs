using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public InventorySo inventory; // Envanter verisi
    private int currentWeaponIndex = 0; // Baþlangýçta gösterilecek silah
    public GameObject[] weaponModels; // Silah modelleri (Prefab'lar)
    private GameObject currentWeaponModel;

    void Start()
    {
        // Baþlangýçta ilk silahý göster
        SwitchWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // Scroll yukarý (yeni silah seç)
        if (Input.mouseScrollDelta.y > 0)
        {
            currentWeaponIndex++;
            if (currentWeaponIndex >= inventory.inventorySlots.Count)
                currentWeaponIndex = 0; // Silahlar arasýnda döngü
            SwitchWeapon(currentWeaponIndex);
        }
        // Scroll aþaðý (önceki silah seç)
        else if (Input.mouseScrollDelta.y < 0)
        {
            currentWeaponIndex--;
            if (currentWeaponIndex < 0)
                currentWeaponIndex = inventory.inventorySlots.Count - 1; // Döngü baþýna dön
            SwitchWeapon(currentWeaponIndex);
        }
    }

    void SwitchWeapon(int index)
    {
        // Eðer aktif bir silah varsa devre dýþý býrak
        if (currentWeaponModel != null)
        {
            currentWeaponModel.SetActive(false);  // Eski silahý gizle
        }

        // Yeni silahý aktif et
        if (index < weaponModels.Length && weaponModels[index] != null)
        {
            currentWeaponModel = weaponModels[index];  // Yeni silah modelini seç
            currentWeaponModel.SetActive(true);         // Yeni silahý göster
        }

        // Eðer silah modelinizin animasyonu varsa, buraya animasyon oynatmayý da ekleyebilirsiniz
        // Örnek: currentWeaponModel.GetComponent<Animator>().Play("DrawWeapon");
    }
}
