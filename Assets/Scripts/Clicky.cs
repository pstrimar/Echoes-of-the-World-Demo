using System.Collections.Generic;
using UnityEngine;

public class Clicky : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] renderers;
    
    private static List<Material> materials = new List<Material>();
    private static bool _isActive = false;
    private static float _lerpPos = .85f;
    private static float illuminationFactor;
    
    void Start()
    {
        //Get all the materials in scene
        foreach(MeshRenderer r in renderers)
        {
            materials.Add(r.material);
            r.material.SetFloat("_Atten", 1.25f);
        }
    }

    void Update()
    {
        //The effect is active so change attenuation over time
        if (_isActive)
        {
            if (_lerpPos < 1.25f)
            {
                _lerpPos += Time.deltaTime / 10;
                foreach (Material m in materials)
                {
                    m.SetFloat("_Atten", _lerpPos * illuminationFactor);
                }
            }
            else
            {
                _isActive = false;
            }
        }
    }

    public static void Illuminate(Vector3 hitPos, Vector3 hitNormal, float multiplier = 1f)
    {
        illuminationFactor = multiplier;
        hitPos += hitNormal * 1f;
        foreach (Material m in materials)
        {
            m.SetVector("_TapPosition", hitPos);
            m.SetFloat("_Atten", 0.125f * illuminationFactor);
            _isActive = true;
            _lerpPos = 0.125f;
        }
    }
}
