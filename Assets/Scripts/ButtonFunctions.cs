using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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

    public void InGameSettings()
    {
        //fill in for discussion and beta
        GameManager.instance.openSettings();
    }
    public void InGameSettingClose()
    {
        GameManager.instance.closeSettings();
    }
    public void InGameSettingBack()
    {
        GameManager.instance.backSettings();
    }

    public void SettingsOpen(GameObject SettingMenu)
    {
        SettingMenu.SetActive(true);
    }
    public void SettingsClose(GameObject SettingMenu)
    {
        SettingMenu.SetActive(false);
    }

}
