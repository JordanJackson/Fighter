using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class RoundManager : MonoBehaviour {

    public GameState gameState;

    public static RoundManager instance;

    GameManager gameManager;

    int player1Score;
    int player2Score;

    public GameObject yBotPrefab;
    public GameObject xBotPrefab;

    Fighter p1;
    Fighter p2;

    Text roundTimerText;
    GameObject roundEndPanel;
    Text roundEndText;

    float timeLeft = 100;

    public enum GameState
    {
        ACTIVE,
        ROUND_OVER,
        GAME_OVER
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        gameManager = FindObjectOfType<GameManager>();
        roundTimerText = GameObject.Find("TimerText").GetComponent<Text>();
        roundEndPanel = GameObject.Find("RoundEndPanel");
        roundEndText = GameObject.Find("RoundEndText").GetComponent<Text>();
        roundEndPanel.SetActive(false);
        gameState = GameState.ACTIVE;

        // spawn player characters
        Vector3 player1Offset = new Vector3(-2.85f, 0.55f, 0.0f);
        Vector3 player2Offset = new Vector3(2.85f, 0.55f, 0.0f);

        if (gameManager.player1Selection == 1)
        {
            GameObject go = Instantiate(yBotPrefab, player1Offset, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
            Fighter f = go.GetComponentInChildren<Fighter>();
            f.transform.position = player1Offset;
            go.GetComponentInChildren<PlayerInput>().SetPlayerNum(1);
            f.SetPlayerNum(1);
            go.GetComponentInChildren<Fighter>().SetCharacterName("Y Bot");
        }
        else if (gameManager.player1Selection == 2)
        {
            GameObject go = Instantiate(xBotPrefab, player1Offset, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
            Fighter f = go.GetComponentInChildren<Fighter>();
            f.transform.position = player1Offset;
            go.GetComponentInChildren<PlayerInput>().SetPlayerNum(1);
            f.SetPlayerNum(1);
            go.GetComponentInChildren<Fighter>().SetCharacterName("X Bot");
        }

        if (gameManager.player2Selection == 1)
        {
            GameObject go = Instantiate(yBotPrefab, player1Offset, Quaternion.Euler(0.0f, -90.0f, 0.0f)) as GameObject;
            if (gameManager.player1Selection == 1)
            {
                go.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
            }
            Fighter f = go.GetComponentInChildren<Fighter>();
            f.transform.position = player2Offset;
            go.GetComponentInChildren<PlayerInput>().SetPlayerNum(2);
            f.SetPlayerNum(2);
            go.GetComponentInChildren<Fighter>().SetCharacterName("Y Bot");
        }
        else if (gameManager.player2Selection == 2)
        {
            GameObject go = Instantiate(xBotPrefab, player1Offset, Quaternion.Euler(0.0f, -90.0f, 0.0f)) as GameObject;
            if (gameManager.player1Selection == 2)
            {
                go.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
            }
            Fighter f = go.GetComponentInChildren<Fighter>();
            f.transform.position = player2Offset;
            go.GetComponentInChildren<PlayerInput>().SetPlayerNum(2);
            f.SetPlayerNum(2);
            go.GetComponentInChildren<Fighter>().SetCharacterName("X Bot");
        }


        Fighter[] fighters = FindObjectsOfType<Fighter>();
        foreach (Fighter f in fighters)
        {
            if (f.GetPlayerNum() == 1)
            {
                p1 = f;
            }
            if (f.GetPlayerNum() == 2)
            {
                p2 = f;
            }
        }
        

        player1Score = gameManager.player1Score;
        player2Score = gameManager.player2Score;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeLeft -= Time.deltaTime;

        // set player references
        if (p1 == null || p2 == null)
        {
            Fighter[] fighters = FindObjectsOfType<Fighter>();
            foreach (Fighter f in fighters)
            {
                if (f.GetPlayerNum() == 1)
                {
                    p1 = f;
                }
                if (f.GetPlayerNum() == 2)
                {
                    p2 = f;
                }
            }
        }

        // debug commands
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            p1.TakeDamage(100, null);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            p2.TakeDamage(100, null);
        }

        // while game active check for round over
        if (gameState == GameState.ACTIVE)
        {
            // time over
            if (timeLeft <= 0.0f)
            {
                gameState = GameState.ROUND_OVER;
                if (p1.health == p2.health)
                {
                    // draw
                    player1Score++;
                    player2Score++;
                    UpdateAndDisplayRoundEnd("Draw");
                    UpdateGameManagerScores();
                }
                else if (p1.health > p2.health)
                {
                    // p1 win
                    player1Score++;
                    UpdateAndDisplayRoundEnd("Player 1 Wins");
                    UpdateGameManagerScores();
                }
                else
                {
                    // p2 win
                    player2Score++;
                    UpdateAndDisplayRoundEnd("Player 2 Wins");
                    UpdateGameManagerScores();
                }
                Debug.Log(player1Score + ", " + player2Score);
            }
            // knockout
            if (p1.health <= 0.0f || p2.health <= 0.0f)
            {
                gameState = GameState.ROUND_OVER;
                Debug.Log("Player1 Health: " + p1.health);
                Debug.Log("Player2 Health: " + p2.health);
                if (p1.health <= 0.0f && p2.health <= 0.0f)
                {
                    // draw
                    Debug.Log("Health: " + p1.health + ", " + p2.health);
                    player1Score++;
                    player2Score++;
                    UpdateAndDisplayRoundEnd("Draw");
                    UpdateGameManagerScores();
                    Debug.Log("State1: " + player1Score + ", " + player2Score);
                }
                else if (p1.health > p2.health)
                {
                    // p1 win
                    player1Score++;
                    UpdateAndDisplayRoundEnd("Player 1 Wins");
                    UpdateGameManagerScores();
                    Debug.Log("State2: " + player1Score + ", " + player2Score);
                }
                else if (p2.health > p1.health)
                {
                    // p2 win
                    player2Score++;
                    UpdateAndDisplayRoundEnd("Player 2 Wins");
                    UpdateGameManagerScores();
                    Debug.Log("State3: " + player1Score + ", " + player2Score);
                }
                Debug.Log(player1Score + ", " + player2Score);
            }
        }

        // update clock
        if (gameState == GameState.ACTIVE)
        {
            roundTimerText.text = ((int)(timeLeft)).ToString();
        }
        
        // when round over reload round or gameover
        if (gameState == GameState.ROUND_OVER)
        {
            
            if (player1Score >=2 || player2Score >= 2)
            {
                if (player1Score > player2Score)
                {
                    UpdateAndDisplayRoundEnd("Game Over:\nPlayer 1 Wins");
                }
                else if (player2Score > player1Score)
                {
                    UpdateAndDisplayRoundEnd("Game Over:\nPlayer 2 Wins");
                }
                else
                {
                    UpdateAndDisplayRoundEnd("Game Over:\nDraw");
                }
                StartCoroutine(GameOver());
            }
            else
            {
                StartCoroutine(ReloadLevel());
            }
            
        }
	}

    void UpdateGameManagerScores()
    {
        gameManager.player1Score = player1Score;
        gameManager.player2Score = player2Score;
    }

    void UpdateAndDisplayRoundEnd(string s)
    {
        roundEndPanel.SetActive(true);
        roundEndText.text = s;
        
    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(2);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        gameManager.player1Score = 0;
        gameManager.player2Score = 0;
        gameManager.player1Selection = 0;
        gameManager.player2Selection = 0;
        gameManager.splashScreen = true;
        FindObjectOfType<MusicManager>().PlayPlayerSelectMusic();
        SceneManager.LoadScene("MenuScene");
    }
}
