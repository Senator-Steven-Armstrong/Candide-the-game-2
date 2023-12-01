using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEntityRepScript : MonoBehaviour
{
    public Sprite sprite;
    public GameObject representedPrefab;
    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
