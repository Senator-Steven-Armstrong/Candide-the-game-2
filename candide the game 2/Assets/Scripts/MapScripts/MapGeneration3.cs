using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration3 : MonoBehaviour
{
    private List<List<string>> mapFrame = new List<List<string>>();

    private string _Tempty = "E";
    private string _Troad = "R";

    [Header("Generation Variables")]
    public Vector2 mapSize;

    [Header("Prefabs")]
    public GameObject roomPrefab;
    public GameObject roomParent;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ClearMapFrame();
            FillMapFrame();
            LogMap();
            ConvertMapFrame();
        }
    }

    void ClearMapFrame()
    {
        if(mapFrame != null)
        {
            mapFrame.Clear();
        }
    }

    void ClearMapObjects()
    {
        for (int i = 0; i < roomParent.transform.childCount; i++)
        {
            Destroy(roomParent.transform.GetChild(0));
        }
    }

    void FillMapFrame()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            mapFrame.Add(new List<string>());
            for (int y = 0; y < mapSize.y; y++)
            {
                mapFrame[x].Add(_Tempty);
            }
        }
    }

    private void ConvertMapFrame()
    {
        for (int x = 0; x < mapFrame.Count; x++)
        {
            Vector3 spawnPos = Vector3.zero;
            spawnPos.x = x * 10;

            for (int y = 0; y < mapFrame[x].Count; y++)
            {
                spawnPos.z = y * 10;
                if (mapFrame[x][y] == _Tempty)
                {
                    GameObject tile = Instantiate(roomPrefab, spawnPos, Quaternion.identity);
                    tile.transform.SetParent(roomParent.transform, true);
                }
            }
        }
    }





    void LogMap()
    {
        if(mapFrame != null)
        {
            string log = "";
            for (int y = 0; y < mapFrame.Count; y++)
            {
                for (int x = 0; x < mapFrame[y].Count; x++)
                {
                    log += mapFrame[x][y];
                }
                log += "\n";
            }
            Debug.Log(log); 
        } 
    }
}
