using UnityEngine;
using System.Collections;

public class WaterGenerator : MonoBehaviour {

    public Shader shader;
    public PointLight pointlight;
    public Terrain currentearth;
    

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 0.6f, 0.8f, 0.5f);
    }

    // Update is called once per frame
    //void update()
    //{
    //    int HeightMapResolation;
    //    int basicheight;
    //    basicheight = currentearth.terrainData.heightmapHeight;
    //    HeightMapResolation = currentearth.terrainData.heightmapResolution;
    //    float[,] height = new float[HeightMapResolation, HeightMapResolation];
    //    height = currentearth.terrainData.GetHeights(0, 0, HeightMapResolation, HeightMapResolation);

    //    //Calculate the average height of the terrain
    //    double sum = 0;
    //    double avg = 0;
    //    for (int i = 0; i < HeightMapResolation; i++)
    //    {
    //        for (int j = 0; j < HeightMapResolation; j++)
    //        {
    //            sum += height[i, j];
    //        }
    //    }
    //    avg = sum / (HeightMapResolation * HeightMapResolation);

    //    //tramsform the water plane to average height;
    //    this.transform.position = new Vector3(256, (float)avg * 250, 256);
    //}
}
