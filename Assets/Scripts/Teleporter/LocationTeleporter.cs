using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTeleporter : MonoBehaviour
{
    [SerializeField]
    private Transform location;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Teleporting");
        if (other.CompareTag("Player"))
        {
            other.transform.position = location.position;
        }
    }
}
