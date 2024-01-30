using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviornmentGenerationsScript : MonoBehaviour
{
    public GameObject rock;
    public GameObject house;
    public GameObject cube;
    [Range(0, 1)] public float xValue;
    [Range(0, 1)] public float yValue;

    private float lastY;
    private float lastX;

    // Start is called before the first frame update
    void Start()
    {
        TestGeneration3();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TestGeneration1()
    {
        for (float i = 0; i < 100; i += 5)
        {
            for (float j = 0; j < 100; j += 5)
            {
                float noise = Mathf.PerlinNoise((float)i / 100f, (float)j / 100f);
                Debug.Log("i = " + i + " | j = " + j + " | Gives noise = " + noise);
                Instantiate(rock, new Vector3(i, noise*20, j), Quaternion.identity);
            }
        }
    }

    private void TestGeneration3()
    {
        for (float i = 0; i < 1; i += 1/100f)
        {
            for (float j = 0; j < 1; j += 1/100f)
            {
                float noise = Mathf.PerlinNoise((float)i, (float)j);
                noise *= 1.3f;
                if (noise < 0.3)
                {
                    Instantiate(cube, new Vector3(i, 0, j), Quaternion.identity);
                } else if (noise < 0.6)
                {
                    Instantiate(rock, new Vector3(i, 0, j), Quaternion.identity);
                }
                else
                {
                    Instantiate(house, new Vector3(i, 0, j), Quaternion.identity);
                }

            }
        }
    }

    private void TestGeneration2()
    {
        if (lastY != yValue || lastX != xValue)
        {
            Debug.Log(Mathf.PerlinNoise((float)xValue, (float)yValue));
        }

        lastY = yValue;
        lastX = xValue;
    }

    class MapObject
    {
        GameObject prefab;
        Vector2 bounds;
    }
}
