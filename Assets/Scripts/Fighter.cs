using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Fighter : MonoBehaviour {

    public enum FighterState
    {
        DEFAULT,
        BLOCK,
        JUMP,
        CROUCH,
        DEAD
    }

    public GameObject fireballPrefab;
    public float fireSpeed = 10f;
    GameObject currentFireball;

    public float jumpHeight;
    public string characterName;
    public float health;
    float maxHealth;

    Text playerNameText;
    Slider playerHealthSlider;
    int playerNum;

    Animator animator;

    FighterState currentState;

    public AttackCollider rightFist;
    public AttackCollider leftFist;
    public AttackCollider rightLeg;
    public AttackCollider leftLeg;

    // set referenecs
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator Component not found in " + gameObject.name);
        }
        SetLeftFistActive(false);
        SetRightFistActive(false);
        SetLeftLegActive(false);
        SetRightLegActive(false);

        currentState = FighterState.DEFAULT;

        playerNameText = GameObject.Find("Player" + playerNum + "NameText").GetComponent<Text>();
        playerHealthSlider = GameObject.Find("Player" + playerNum + "HealthSlider").GetComponent<Slider>();
    }

    void Start()
    {
        maxHealth = health;
    }

    public void SetPlayerNum(int num)
    {
        playerNum = num;
        playerNameText = GameObject.Find("Player" + playerNum + "NameText").GetComponent<Text>();
        playerHealthSlider = GameObject.Find("Player" + playerNum + "HealthSlider").GetComponent<Slider>();
    }

    public int GetPlayerNum()
    {
        return playerNum;
    }

    public void SetCharacterName(string s)
    {
        characterName = s;
        playerNameText.text = characterName;
    }

    // animation event to launch fireball at frame of Fireball animation
    public void LaunchFireball()
    {
        GameObject go = Instantiate(fireballPrefab, rightFist.transform.position, Quaternion.identity) as GameObject;
        go.transform.SetParent(null);
        FindObjectOfType<MusicManager>().PlayProjectile();
        go.GetComponent<Projectile>().SetDirection(this.transform.forward);
        go.GetComponent<Projectile>().SetOwner(this);
    }

    // animation event on landing frame of Jump animation
    public void Landing()
    {
        currentState = FighterState.DEFAULT;
        FindObjectOfType<MusicManager>().PlayLanding();
    }

    // handle movement inputs and animation calls
    public void Movement(Vector2 movement)
    {
        if (playerNum == 1)
        {
            animator.SetFloat("horizontal", movement.x);
        }
        else if (playerNum == 2)
        {
            animator.SetFloat("horizontal", -movement.x);
        }
        animator.SetFloat("vertical", movement.y);
        if (movement.x != 0.0f)
        {
            animator.SetBool("stepping", true);
        }
        else
        {
            animator.SetBool("stepping", false);
        }
        if (movement.y <= -0.01f)
        {
            
        }
    }

    // process attack inputs
    public void Attack(int id)
    {
        switch (id)
        {
            case 0:
                {
                    animator.SetTrigger("punch");
                    break;
                }
            case 1:
                {
                    animator.SetTrigger("fireball");
                    break;
                }
            default:
                break;
        }
    }

    // constrict player position to z = 0
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
    }

    // handle block inputs and state
    public void Block(bool block)
    {
        if (block)
        {
            currentState = FighterState.BLOCK;
        }
        else
        {
            currentState = FighterState.DEFAULT;
        }
        animator.SetBool("blocking", block);
    }

    // player taunt
    public void Taunt()
    {
        animator.SetTrigger("taunt");
    }

    // player jump
    public void Jump()
    {
        animator.SetTrigger("jump");
    }

    // handle block check damage
    public void TakeDamage(float damage, GameObject other)
    {
        animator.SetTrigger("hitReact");
        if (currentState == FighterState.BLOCK)
        {
            health -= (damage)/2;
        }
        else
        {
            health -= damage;
            FindObjectOfType<MusicManager>().PlayHurt();
        }
        
        playerHealthSlider.value = health;
        if (CheckIfDead())
        {
            currentState = FighterState.DEAD;
            FindObjectOfType<MusicManager>().PlayDeath();
            animator.SetBool("dead", true);
        }
    }

    // check if player is dead
    bool CheckIfDead()
    {
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetState(FighterState state)
    {
        currentState = state;
    }

    public void SetRightFistActive(bool active)
    {
        rightFist.gameObject.SetActive(active);
    }

    public void SetLeftFistActive(bool active)
    {
        leftFist.gameObject.SetActive(active);
    }

    public void SetRightLegActive(bool active)
    {
        rightLeg.gameObject.SetActive(active);
    }

    public void SetLeftLegActive(bool active)
    {
        leftLeg.gameObject.SetActive(active);
    }
}
