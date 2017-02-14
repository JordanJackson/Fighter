using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float speed;
    public float damage;
    public float lifetime;
    public float lifetimeAfterHit;
    float age;

    private Fighter owner;

    Vector3 direction;

    bool move = false;

    public void SetOwner(Fighter f)
    {
        owner = f;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
        move = true;
    }

    // move projectile, check for destroy
    void Update()
    {
        age += Time.deltaTime;
        if (age > lifetime)
        {
            Destroy(this.gameObject);
        }

        if (move)
        {
            this.transform.Translate(direction * speed * Time.deltaTime);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0.0f);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Projectile>())
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
        Fighter f = other.gameObject.GetComponent<Fighter>();
        if (f)
        {
            Destroy(this.gameObject, lifetimeAfterHit);
            FindObjectOfType<MusicManager>().PlayImpact();
            f.TakeDamage(damage, this.gameObject);
        }
    }
}
