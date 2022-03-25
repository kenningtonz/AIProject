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
        mapManager = FindObjectOfType<MapManager>();
      
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

    public void setAction(Text action)
    {
        if (action.text == "Paint")
            mapManager.action = Enums.Action.Paint;
        if (action.text == "Spawn")
            mapManager.action = Enums.Action.Spawn;

    }

    public void setGameSpeed(Slider slider)
    {
        mapManager.gameSpeed = (int)slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mapManager.action = Enums.Action.Paint;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mapManager.action = Enums.Action.Spawn;
        }
    }
}
