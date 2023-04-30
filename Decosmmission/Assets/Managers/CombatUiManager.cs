using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatUiManager : MonoBehaviour
{
    public TextMeshProUGUI WeaponName;
    public static CombatUiManager instance;
    public Slider slider;
    public TextMeshProUGUI MaxHP;
    public TextMeshProUGUI HP;
    public GameObject MagAmmoBox;
    public GameObject MagBox;
    public GameObject AmmoBox;
    public GameObject MTBox;
    public TextMeshProUGUI[] Mag;
    public TextMeshProUGUI[] Ammo;
    public TextMeshProUGUI[] MaxMag;
    public TextMeshProUGUI[] MaxAmmo;
    public int type;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public static void UpdateHP(int HP)
    {
        instance.slider.value = Mathf.Max(HP,0);
        instance.HP.text = Mathf.Max(HP, 0).ToString();
    }
    public static void UpdateMaxHP(int MHP)
    {
        instance.slider.maxValue = MHP;
        instance.MaxHP.text = MHP.ToString();
    }

    public static void UpdateWeapon(Weapon weapon)
    {
        instance.MagAmmoBox.SetActive(weapon.UseMagazine && weapon.UseAmmo);
        instance.MagBox.SetActive(weapon.UseMagazine && !weapon.UseAmmo);
        instance.AmmoBox.SetActive(!weapon.UseMagazine && weapon.UseAmmo);
        instance.MTBox.SetActive(!weapon.UseMagazine && !weapon.UseAmmo);
        instance.WeaponName.text = weapon.TrueName;
        UpdateMunitions(weapon);
    }

    public static void UpdateMunitions(Weapon weapon)
    {
        if (weapon.UseMagazine)
        {
            if (weapon.UseAmmo)
            {
                instance.Mag[0].text = weapon.Magazine.ToString();
                instance.MaxMag[0].text = weapon.MaxMagazine.ToString();
                instance.Ammo[0].text = weapon.Ammo.ToString();
                instance.MaxAmmo[0].text=weapon.MaxAmmo.ToString();
            }
            else
            {
                instance.Mag[1].text = weapon.Magazine.ToString();
                instance.MaxMag[1].text = weapon.MaxMagazine.ToString();
            }
        }
        else if (weapon.UseAmmo)
        {

            instance.Ammo[1].text = weapon.Ammo.ToString();
            instance.MaxAmmo[1].text = weapon.MaxAmmo.ToString();
        }
    }

    public static void UpdateMagazine(Weapon weapon)
    {
        if (weapon.UseAmmo)
        {
            instance.Mag[0].text = weapon.Magazine.ToString();
        }
        else
            instance.Mag[1].text = weapon.Magazine.ToString();
    }

    public static void UpdateMaxMagazine(Weapon weapon)
    {
        if (weapon.UseAmmo)
        {
            instance.MaxMag[0].text = weapon.Magazine.ToString();
        }
        else
            instance.MaxMag[1].text = weapon.Magazine.ToString();
    }
    public static void UpdateAmmo(Weapon weapon)
    {
        if (weapon.UseMagazine)
        {
            instance.Ammo[0].text = weapon.Magazine.ToString();
        }
        else
            instance.Ammo[1].text = weapon.Magazine.ToString();
    }

    public static void UpdateMaxAmmo(Weapon weapon)
    {
        if (weapon.UseMagazine)
        {
            instance.MaxAmmo[0].text = weapon.Magazine.ToString();
        }
        else
            instance.MaxAmmo[1].text = weapon.Magazine.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
