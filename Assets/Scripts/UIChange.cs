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
            mapManager.biome = Enums.Biomes.Plains;
        if (biome.text == "Snow")
            mapManager.biome = Enums.Biomes.Snow;
        if (biome.text == "Sand")
            mapManager.biome = Enums.Biomes.Sand;
        if (biome.text == "Forest")
            mapManager.biome = Enums.Biomes.Forest;
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
