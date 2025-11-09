using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    public int maxHp;
    public int hpRegen;

    public int rapidFireDmg;
    public int spreadFireDmg;
    public int RailgunDmg;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void upgradeHP()
    {
        maxHp += 10;
    }

    void upgradeRegen()
    {
        hpRegen += 1;
    }
    void upgradeRapid()
    {
        rapidFireDmg += 2;
    }
    void upgradeSpread()
    {
        spreadFireDmg += 4;
    }
    void upgradeRail()
    {
        RailgunDmg += 7;
    }
}
