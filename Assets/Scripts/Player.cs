using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject playerVisuals;

    public void Spawn(Transform spawnPoint)
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        playerVisuals.SetActive(true);
    }

    public void Die()
    {
        playerVisuals.SetActive(false);
    }
}
