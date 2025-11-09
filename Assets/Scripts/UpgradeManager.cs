using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    public int maxHp;
    public int hpRegen;

    public int rapidFireDmg;
    public int spreadFireDmg;
    public int RailgunDmg;
    public int weaponUpgrades;

    [SerializeField] GameObject[] Upgrades;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void upgradeHP()
    {
        maxHp += 10;
    }

    public void upgradeRegen()
    {
        hpRegen += 1;
    }
    public void upgradeRapid()
    {
        rapidFireDmg += 2;
        weaponUpgrades++;
    }
    public void upgradeSpread()
    {
        spreadFireDmg += 4;
        weaponUpgrades++;
    }
    public void upgradeRail()
    {
        RailgunDmg += 7;
        weaponUpgrades++;
    }

    public void UpgradeRandomizer()
    {
        int toggleoff1 = Random.Range(0, 6);
        int toggleoff2 = Random.Range(0, 6);
        if (toggleoff2 == toggleoff1 && toggleoff2 == 0) toggleoff2++;
        if (toggleoff2 == toggleoff1 && toggleoff2 == 5) toggleoff2--;

        for (int i = 0; i < Upgrades.Length; i++)
        {
            Upgrades[i].SetActive(true);
        }
        Upgrades[toggleoff1].SetActive(false);
        Upgrades[toggleoff2].SetActive(false);
    }
}
