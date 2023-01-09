using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowStone : MonoBehaviour
{
    [SerializeField] Stone stonePrefab;
    [SerializeField] Transform spawnPoint;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Throw");            
        }
    }

    public void Throw()
    {
        var stone = Instantiate(stonePrefab, spawnPoint.position, Quaternion.identity, null);
        stone.Launch();
    }
}
