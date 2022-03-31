using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    AudioSource appleAS;
    readonly float range = 4.5f;

    private void Awake()
    {
        appleAS = GetComponent<AudioSource>();
    }

    public void MoveApple()
    {
        var posX = Random.Range(-range, range);
        var posZ = Random.Range(-range, range);
        transform.position = new Vector3(posX, 0.25f, posZ);
    }

    public void PlaySound()
    {
        appleAS.Play();
    }
}
