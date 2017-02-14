using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    

    MusicManager musicManager;

    public int player1Selection = 0;
    public int player2Selection = 0;

    public int player1Score = 0;
    public int player2Score = 0;

    GameObject titlePanel;
    GameObject characterSelectPanel;

    public bool splashScreen = true;

    // singleton
    public static GameManager instance;

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

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        musicManager.PlayPlayerSelectMusic();

        titlePanel = FindObjectOfType<MenuController>().gameObject;
        characterSelectPanel = FindObjectOfType<CharacterSelectController>().gameObject;

        splashScreen = true;
    }

    void Update()
    {
        // control menu screen flow
        if (splashScreen)
        {
            if (titlePanel == null || characterSelectPanel == null)
            {
                titlePanel = FindObjectOfType<MenuController>().gameObject;
                characterSelectPanel = FindObjectOfType<CharacterSelectController>().gameObject;
            }
            titlePanel.SetActive(true);
            characterSelectPanel.SetActive(false);

            if (Input.GetButtonDown("Jump1") || Input.GetButtonDown("Jump2"))
            {
                splashScreen = false;
                titlePanel.SetActive(false);
                characterSelectPanel.SetActive(true);
            }
        }
            
    }

    // handle character selection input
    public void SetPlayerSelection(int player, int selection)
    {
        switch (player)
        {
            case 1:
                player1Selection = selection;
                break;
            case 2:
                player2Selection = selection;
                break;
        }
    }

    // check if both players have selected a character
    public bool SelectionsMade()
    {
        if (player1Selection != 0 && player2Selection != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // start the fight portion of the game
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/mainScene");
        musicManager.PlayGameMusic();
    }
}
