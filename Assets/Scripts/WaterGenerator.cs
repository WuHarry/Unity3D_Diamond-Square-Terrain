using UnityEngine;
using System.Collections;

public class WaterGenerator : MonoBehaviour {

    public Shader shader;
    public PointLight pointlight;    

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 0.6f, 0.8f, 0.5f);
    }
}
