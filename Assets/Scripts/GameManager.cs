using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Transform spawnPoint;

    Player player;
    DeathUI deathUI;
    CinemachineVirtualCamera vCam;
    const float respawnTime = 3f;

    void Awake()
    {
        if (instance != null)
		{
			Destroy(this);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        deathUI = FindObjectOfType<DeathUI>();
        vCam = FindObjectOfType<CinemachineVirtualCamera>();
    }

    public void KillPlayer()
    {
        player.Die();
        vCam.enabled = false;
        deathUI.enabled = true;
        Invoke("SpawnPlayer", respawnTime);
    }

    public void SpawnPlayer()
    {
        vCam.enabled = true;
        vCam.ForceCameraPosition(spawnPoint.position - new Vector3(0, 0, 10), spawnPoint.rotation);
        player.Spawn(spawnPoint);
    }
}
