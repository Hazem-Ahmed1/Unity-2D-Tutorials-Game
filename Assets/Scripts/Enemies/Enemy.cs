using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    Animator animator;
    public GameObject vfxPrefab;  // Assign your VFX prefab in the inspector
    public GameObject damageVfxPrefab;  // Assign your damage VFX prefab in the inspector

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

public void TakeDamage(int damage)
{
    currentHealth -= damage;
    animator.SetTrigger("Hurt");

    Vector3 offsetPosition = transform.position + new Vector3(0, -0.5f, 0);  // Adjust y offset as needed
    GameObject damageVfxInstance = Instantiate(damageVfxPrefab, offsetPosition, transform.rotation);
    Destroy(damageVfxInstance, 1f);

    if (currentHealth <= 0)
    {
        Die();
    }
}

public void Die()
{
    animator.SetBool("isDead", true);
    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    GetComponent<Collider2D>().enabled = false;

    // Disable all scripts attached to this GameObject
    MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
    foreach (MonoBehaviour script in scripts)
    {
        script.enabled = false;
    }
    this.enabled = false;

    // Instantiate the VFX at the current position and rotation
    GameObject vfxInstance = Instantiate(vfxPrefab, transform.position, transform.rotation);

    // Destroy the VFX instance after 2 seconds
    Destroy(vfxInstance, 2f);

    // Destroy this GameObject after a delay
    Destroy(gameObject, 2f);
}

}
