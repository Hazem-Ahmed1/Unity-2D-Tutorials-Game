using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGuide : MonoBehaviour
{
    private bool gotLight = false;
    public GameObject FollowingOrb;
    public GameObject FollowingPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Guide") && !gotLight)
        {
            Instantiate(FollowingOrb,FollowingPoint.transform.position,FollowingPoint.transform.rotation);
            gotLight = true;
        }
    }
}
