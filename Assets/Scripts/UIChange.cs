using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChange : MonoBehaviour
{
    private MapManager mapManager;
    // Start is called before the first frame update
    void Start()
    {
        mapManager = GetComponent<MapManager>();
    }

    public void setBiome(Text biome)
    {
        if (biome.text == "Plains")
            mapManager.biome = MapManager.Biomes.Plains;
        if (biome.text == "Snow")
            mapManager.biome = MapManager.Biomes.Snow;
        if (biome.text == "Sand")
            mapManager.biome = MapManager.Biomes.Sand;
    }

    public void setBrushSize(Slider slider)
    {
        mapManager.brushsize = (int)slider.value;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
