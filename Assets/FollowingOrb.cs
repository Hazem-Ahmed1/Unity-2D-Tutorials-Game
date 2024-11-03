using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingOrb : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float lineOfSite = 2f;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("LightingPoint").transform;
    }

    private void Update()
    {
        float distanceFromTarget = Vector2.Distance(target.position, transform.position);
        if (distanceFromTarget > lineOfSite)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
