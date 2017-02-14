using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public int playerNum;

    Fighter fighter;

    void Awake()
    {
        fighter = GetComponentInChildren<Fighter>();
        if (!fighter)
        {
            Debug.LogError("Fighter Component not found in " + gameObject.name);
        }
        fighter.SetPlayerNum(playerNum);
    }

    // process player input
	void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal" + playerNum), Input.GetAxisRaw("Vertical" + playerNum));
        fighter.Movement(movement);

        if (Input.GetButtonDown("Punch" + playerNum))
        {
            // punch
            fighter.Attack(0);

        }
        if (Input.GetButtonDown("Projectile" + playerNum))
        {
            // fireball
            fighter.Attack(1);
        }
        if (Input.GetButton("Block" + playerNum))
        {
            // block
            fighter.Block(true);
        }
        else
        {
            fighter.Block(false);
        }

        if (Input.GetButtonDown("Jump" + playerNum))
        {
            fighter.Jump();
        }

        if (Input.GetButtonDown("Taunt" + playerNum))
        {
            fighter.Taunt();
        }
    }

    public void SetPlayerNum(int num)
    {
        playerNum = num;
    }
}
