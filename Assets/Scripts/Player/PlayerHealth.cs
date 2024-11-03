using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image HealthBar;
    public float HealthAmount;
    private float targetHealthAmount;
    public const float maxHealth = 100f;
    public float lerpSpeed = 5f;

    Animator animator;
    Rigidbody2D rb;
    SceneController sceneController;
    public bool isDead = false;

    SpriteRenderer playerSprite;

    public CameraShake VMcamera;
    public AudioClip hurtSFX;
    void Start()
    {
        VMcamera = GameObject.Find("Virtual Camera").GetComponent<CameraShake>();
        playerSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        HealthBar = GameObject.Find("Health").GetComponent<Image>();
        HealthAmount = maxHealth;
        targetHealthAmount = maxHealth;
    }

    void Update()
    {
        HealthAmount = Mathf.Lerp(HealthAmount, targetHealthAmount, Time.deltaTime * lerpSpeed);
        HealthBar.fillAmount = HealthAmount / maxHealth;
        if (targetHealthAmount <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        AudioManager.Instance.PlayClipOneShot(hurtSFX);
        StartCoroutine(FlashRed());
        VMcamera.ShakeCamera(3f, 0.2F);
        targetHealthAmount -= damage;
        targetHealthAmount = Mathf.Clamp(targetHealthAmount, 0, maxHealth);
    }

    private IEnumerator FlashRed()
    {
        playerSprite.color = new Color(255,0,0);
        yield return new WaitForSeconds(0.1f);
        playerSprite.color = new Color(255, 255, 255);
    }

    public void Heal(float hp)
    {
        targetHealthAmount += hp;
        targetHealthAmount = Mathf.Clamp(targetHealthAmount, 0, maxHealth);
    }

    public void Die()
    {
        animator.SetTrigger("Dead");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject, 2f);
        Invoke("displayLosePanel", 1f);
    }

    void displayLosePanel()
    {
        sceneController.Lose();
    }

    #region Damage&Healing
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyWarriorAttack"))
        {
            TakeDamage(5);
        }
        if (collision.gameObject.CompareTag("Heal"))
        {
            Heal(5);
            Destroy(collision.gameObject);            
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            sceneController.Win();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Destroy(collision.gameObject);
            TakeDamage(5);
        }
    }
    #endregion
}
