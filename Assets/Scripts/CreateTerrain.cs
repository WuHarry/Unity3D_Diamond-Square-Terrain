using UnityEngine;
using System.Collections;

public class CreateTerrain : MonoBehaviour
{
    public Shader shader;
    public PointLight pointLight;
    public int HEIGHTMAPRESOLUTION = 513;
    public int basicHeight = 250;
    private TerrainData terrainData;
    public Terrain CurrentTerrainData;
    
    // Use this for initialization
    void Start()
    {
        GameObject earth;
        earth = new GameObject();
        TerrainData terrainData;
        //Generate TerrainData
        terrainData = this.CreateTerrainData();
        earth = Terrain.CreateTerrainGameObject(terrainData);
        earth.name = "Earth";

        //Set Position
        Vector3 position = new Vector3(0f, 0f, 0f);

        //Create Terrain
        Instantiate(earth, position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        //Store current terrain data for future use.
        CurrentTerrainData.terrainData = terrainData;
    }

    TerrainData CreateTerrainData()
    {
        //Generate TerrainData
        int size = HEIGHTMAPRESOLUTION - 1;

        float[,] HeightMap = new float[HEIGHTMAPRESOLUTION, HEIGHTMAPRESOLUTION];
        //initialize Heightmap
        for (int i = 0; i < HEIGHTMAPRESOLUTION; i++)
        {
            for (int j = 0; j < HEIGHTMAPRESOLUTION; j++)
            {
                HeightMap[i, j] = 0.5f;
            }
        }
        TerrainData t = new TerrainData();
        t.heightmapResolution = HEIGHTMAPRESOLUTION;
        t.alphamapResolution = HEIGHTMAPRESOLUTION - 1;
        t.size = new Vector3((float)size, basicHeight, (float)size);
        //Generate HeightMap using Diamond-square algorithm
        //Normalize HeightMap With Linar Function
        DiamondSquare(HeightMap);
        Normalize(HeightMap);
        t.SetHeights(0, 0, HeightMap);
        return t;
    }

    void DiamondSquare(float[,] array)
    {
        //set conners 0 for mountains 1 for valleys
        array[0, 0] = 0.5f;
        array[HEIGHTMAPRESOLUTION - 1, 0] = 0.5f;
        array[0, HEIGHTMAPRESOLUTION - 1] = 0.5f;
        array[HEIGHTMAPRESOLUTION - 1, HEIGHTMAPRESOLUTION - 1] = 0.5f;

        int sidelength;
        int halfside;
        float value;
        float ramdomChange = 0.5f;

        for (sidelength = HEIGHTMAPRESOLUTION - 1; sidelength >= 2; sidelength /= 2)
        {
            halfside = sidelength / 2;

            //Diamond present
            for (int x = 0; x < HEIGHTMAPRESOLUTION - 1; x += sidelength)
            {
                for (int y = 0; y < HEIGHTMAPRESOLUTION - 1; y += sidelength)
                {
                    //average from the cornor
                    value = array[x, y] + array[x + sidelength, y] + array[x, y + sidelength] + array[x + sidelength, y + sidelength];
                    value /= 4.0f;
                    //random value
                    value += Random.Range(-ramdomChange, ramdomChange);
                    array[x + halfside, y + halfside] = value;
                }
            }

            //Square present           
            for (int x = 0; x < HEIGHTMAPRESOLUTION - 1; x += halfside)
            {
                for (int y = (x + halfside) % sidelength; y < HEIGHTMAPRESOLUTION - 1; y += sidelength)
                {
                    //calculate the average
                    value = array[(x - halfside + HEIGHTMAPRESOLUTION - 1) % (HEIGHTMAPRESOLUTION - 1), y]
                        + array[(x + halfside) % (HEIGHTMAPRESOLUTION - 1), y]
                        + array[x, (y - halfside + HEIGHTMAPRESOLUTION - 1) % (HEIGHTMAPRESOLUTION - 1)]
                        + array[x, (y + halfside) % (HEIGHTMAPRESOLUTION - 1)];
                    value /= 4.0f;
                    //add random value
                    value += Random.Range(-ramdomChange, ramdomChange);
                    array[x, y] = value;

                    //when (x,y) repersents the edge, simply copy the value to the other side
                    //can reduce the calculation
                    if (x == 0) array[HEIGHTMAPRESOLUTION - 1, y] = value;
                    if (y == 0) array[x, HEIGHTMAPRESOLUTION - 1] = value;
                }
            }
            ramdomChange *= 0.5f;
        }
    }

    //Normalize HeightMap With Linar Function
    void Normalize(float[,] array)
    {
        float min = 1;
        float max = 0;
        //Find out the biggest value and smallest value
        for (int i = 0; i < HEIGHTMAPRESOLUTION; i++)
        {
            for (int j = 0; j < HEIGHTMAPRESOLUTION; j++)
            {
                if (array[i, j] > max) max = array[i, j];
                if (array[i, j] < min) min = array[i, j];
            }
        }

        //maxout is the biggest value we expected 
        //minout is the smallest value we expected
        float maxout = 1;
        float minout = 0.5f;
        for (int i = 0; i < HEIGHTMAPRESOLUTION; i++)
        {
            for (int j = 0; j < HEIGHTMAPRESOLUTION; j++)
            {
                array[i, j] = (array[i, j] - min) / (max - min) * (maxout - minout) + minout;
            }
        }
    }
}
