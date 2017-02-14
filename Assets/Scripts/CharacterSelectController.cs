using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelectController : MonoBehaviour
{
    int player1Selection;
    int player2Selection;

    public GameObject p1c1;
    public GameObject p1c2;
    public GameObject p2c1;
    public GameObject p2c2;

    bool p1Set = false;
    bool p2Set = false;

    GameManager gameManager;

    void Start ()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.LogError("GameManager not found.");
        }

        player1Selection = 1;
        player2Selection = 2;
        p1c1.SetActive(true);
        p1c2.SetActive(false);
        p2c1.SetActive(false);
        p2c2.SetActive(true);

	}
	
    // handle character select screen input
    // this method used due to UI not handling multiple player inputs well
	void Update ()
    {
        // get player1 input
	    if (Input.GetAxis("Horizontal1") == 1.0f && !p1Set)
        {
            player1Selection = 2;
            p1c1.SetActive(false);
            p1c2.SetActive(true);
        }
        else if (Input.GetAxis("Horizontal1") == -1.0f && !p1Set)
        {
            player1Selection = 1;
            p1c1.SetActive(true);
            p1c2.SetActive(false);
        }
        // get player2 input
        if (Input.GetAxis("Horizontal2") == 1.0f && !p2Set)
        {
            player2Selection = 2;
            p2c1.SetActive(false);
            p2c2.SetActive(true);
        }
        else if (Input.GetAxis("Horizontal2") == -1.0f && !p2Set)
        {
            player2Selection = 1;
            p2c1.SetActive(true);
            p2c2.SetActive(false);
        }

        if (Input.GetButtonDown("Jump1"))
        {
            p1c1.GetComponent<Image>().color = Color.white;
            p1c2.GetComponent<Image>().color = Color.white;
            gameManager.SetPlayerSelection(1, player1Selection);
            p1Set = true;
            if (gameManager.SelectionsMade())
            {
                gameManager.StartGame();
            }
        }

        if (Input.GetButtonDown("Jump2"))
        {
            p2c1.GetComponent<Image>().color = Color.white;
            p2c2.GetComponent<Image>().color = Color.white;
            gameManager.SetPlayerSelection(2, player2Selection);
            p2Set = true;
            if (gameManager.SelectionsMade())
            {
                gameManager.StartGame();
            }
        }
    }

}
