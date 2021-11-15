using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnEffect : MonoBehaviour
{
    [SerializeField] private float effectTime;
    [SerializeField] private GameObject spawnParticles;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn particle effect
        Destroy(Instantiate(spawnParticles, transform.position, Quaternion.identity), effectTime);

        GetComponent<PlayerInput>().DisableInput();
        Invoke("AllowInput", effectTime);
    }

    private void AllowInput() 
    { 
        GetComponent<PlayerInput>().EnableInput(); 
    }
}
