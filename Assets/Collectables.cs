using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public static int score = 0;
    public AudioClip MoneySFX;
    public TextMeshProUGUI gold;

    private void Update()
    {
        gold.text = score.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cash"))
        {
            score++;
            Destroy(collision.gameObject);
            AudioManager.Instance.PlayClipOneShot(MoneySFX);
        }
    }
}
