using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void resume()
    {
        GameManager.instance.stateUnpause();
    }

    public void restart()
    {
        GameManager.instance.stateUnpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void returnToTitle(int lvl)
    {
        GameManager.instance.stateUnpause();
        SceneManager.LoadScene(lvl);
    }
    public void loadLevel(int lvl)
    {
        SceneManager.LoadScene(lvl);
    }

    public void HealthUpgrade()
    {
        UpgradeManager.instance.upgradeHP();
        GameManager.instance.closeUpgrade();
    }

    public void RegenUpgrade()
    {
        UpgradeManager.instance.upgradeRegen();
        GameManager.instance.closeUpgrade();
    }

    public void RapidUpgrade()
    {
        UpgradeManager.instance.upgradeRapid();
        GameManager.instance.closeUpgrade();
    }

    public void SpreadUpgrade()
    {
        UpgradeManager.instance.upgradeSpread();
        GameManager.instance.closeUpgrade();
    }

    public void RailGunUpgrade()
    {
        UpgradeManager.instance.upgradeRail();
        GameManager.instance.closeUpgrade();
    }

    public void Settings()
    {
        //fill in for discussion and beta
    }
}
