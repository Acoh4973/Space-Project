using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuEnd;
    [SerializeField] GameObject menuUpgrade;

    public GameObject player;
    public PlayerController playerScript;
    public Image playerHPBar;
    public TMP_Text ScoreShown;

    public bool isPaused;

    float timeScaleOrig;

    public int score;
    public int XP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuActive == null)
            {
                statePause();
                menuActive = menuPause;
                menuActive.SetActive(true);
            }
            else if (menuActive == menuPause)
            {
                stateUnpause();
            }
        }
        upgradeCheck();
        updateScore();
    }

    public void statePause()
    {
        isPaused = !isPaused;
        Time.timeScale = 0;
    }

    public void stateUnpause()
    {
        isPaused = !isPaused;
        Time.timeScale = timeScaleOrig;
        menuActive.SetActive(false);
        menuActive = null;
    }

    public void endGame()
    {
        statePause();
        menuActive = menuEnd;
        menuActive.SetActive(true);
    }

    void upgradeCheck()
    {
        if (XP >= 10)
        {
            XP -= 10;
            openUpgrade();
        }
    }

    public void openUpgrade()
    {
        statePause();
        menuActive = menuUpgrade;
        menuActive.SetActive(true);
        UpgradeManager.instance.UpgradeRandomizer();
    }

    public void closeUpgrade()
    {
        stateUnpause();
    }

    void updateScore()
    {
        ScoreShown.text = score.ToString();
    }
}
