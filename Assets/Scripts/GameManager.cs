using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuEnd;
    [SerializeField] GameObject menuUpgrade;

    public GameObject player;
    public PlayerController playerScript;

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
        menuActive.SetActive(true);
        menuActive = menuEnd;
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
    }

    public void closeUpgrade()
    {
        stateUnpause();
        menuActive = null;
        menuActive.SetActive(false);
    }
}
