using System.Collections.Generic;
using UnityEngine;

public class Clicky : MonoBehaviour
{
    [SerializeField] MeshRenderer[] renderers;
    [SerializeField] GameObject scanPrefab;
    [SerializeField] float scanScaleMultiplier = 2f;
    
    private static List<Material> materials = new List<Material>();
    private static bool _isActive = false;
    private static float _lerpPos = .85f;
    private static float illuminationFactor;
    //private static GameObject scanVFX;
    private static List<GameObject> scanVFX = new List<GameObject>();
    private static float scanStartScale1 = 0.08f;
    private static float scanStartScale2 = 0.08f;
    private static float scanStartScale3 = 0.08f;
    private static float[] scanStartScale = new float[] {.08f, 1.08f, 2.08f};
    
    void Start()
    {
        //Get all the materials in scene
        foreach(MeshRenderer r in renderers)
        {
            materials.Add(r.material);
            r.material.SetFloat("_Atten", 1.25f);
        }
        for (int i = 0; i < 3; i++)
        {
            var scanObject = Instantiate(scanPrefab, Vector3.zero, Quaternion.identity);
            scanObject.SetActive(false);
            scanObject.transform.localScale = new Vector3(scanStartScale[i], scanStartScale[i], scanStartScale[i]);
            scanVFX.Add(scanObject);
        }
    }

    void Update()
    {
        //The effect is active so change attenuation over time
        if (_isActive)
        {
            foreach (var scanObj in scanVFX)
            {
                if (scanObj.transform.localScale.x < 10f)
                {
                    scanObj.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime) * scanScaleMultiplier;
                }
                else
                {
                    scanObj.SetActive(false);                
                }
            }

            if (_lerpPos < 1.25f)
            {
                _lerpPos += Time.deltaTime / 5;
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
        for (int i = 0; i < 3; i++)
        {
            scanVFX[i].transform.position = hitPos;
            scanVFX[i].transform.localScale = new Vector3(scanStartScale[i], scanStartScale[i], scanStartScale[i]);
            scanVFX[i].SetActive(true);
        }
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
