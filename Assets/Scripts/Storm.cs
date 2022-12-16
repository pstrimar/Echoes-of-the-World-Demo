using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour, ISoundMaker
{
    public float illuminationMultiplier => 3f;
    [SerializeField] float posMultiplier = 30f;
    [SerializeField] int impactsPerFrame = 1;
    [SerializeField] LayerMask groundLayer;
    AudioSource source;
    Vector2 randomPosVector;
    Vector3 randomPos;

    void Update()
    {
        for (int i = 0; i < impactsPerFrame; i++)
        {
            Vector2 randomPosVector = Random.insideUnitCircle * posMultiplier;
            var randomPos = transform.position + new Vector3(randomPosVector.x, 0, randomPosVector.y);
            if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hitInfo, 15f, groundLayer))
            {
                Clicky.Illuminate(hitInfo.point, hitInfo.normal, illuminationMultiplier);
            }
        }
    }
}
