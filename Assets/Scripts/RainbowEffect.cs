using System.Collections;
using UnityEngine;

public class RainbowEffect : MonoBehaviour
{
    // Resource(s) Used: https://www.youtube.com/watch?v=I4PVGffuoB8

    public Material mt;
    public Color32[] colors;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mt = transform.GetComponent<MeshRenderer>().material;
        colors = new Color32[7]
        {
            new Color32(255, 0, 0, 255),
            new Color32(255, 165, 0, 255),
            new Color32(255, 255, 0, 255),
            new Color32(0, 255, 0, 255),
            new Color32(0, 0, 255, 255),
            new Color32(75, 0, 130, 255),
            new Color32(238, 130, 238, 255),
        };
        
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle()
    {
        int i = 0;
        
        while (true)
        {
            for (float interpolant = 0f; interpolant < 1f; interpolant += 0.01f)
            {
                mt.color = Color.Lerp(colors[i % 7], colors[(i + 1) % 7], interpolant);
                yield return null;
            }
            i++;
        }
    }
}
