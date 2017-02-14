using UnityEngine;
using System.Collections;

public class AttackCollider : MonoBehaviour {

    
    public string limbName;

    GameObject parentFighter;

    // set references and parent transform
    void Start()
    {
        limbName = transform.parent.name;
        parentFighter = this.transform.root.gameObject;
        if (!parentFighter)
        {
            Debug.LogError("Fighter not found.");
        }
    }

    // on hit
	void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit " + other.gameObject.name + " with " + limbName);
        Fighter f = other.GetComponentInChildren<Fighter>();
        if (f && other.gameObject != parentFighter)
        {
            f.TakeDamage(10, this.gameObject);
            FindObjectOfType<MusicManager>().PlayImpact();
            this.gameObject.SetActive(false);
        }
    }
}
