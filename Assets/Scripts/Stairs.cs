using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField] string layerName1;
    [SerializeField] string layerName2;

    int layer1, layer2;

    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        layer1 = LayerMask.NameToLayer(layerName1);
        layer2 = LayerMask.NameToLayer(layerName2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            int currentFloorLayer = other.gameObject.layer;

            if (currentFloorLayer == layer1 || currentFloorLayer == layer2)
            {
                ToggleLayer(LayerMask.LayerToName(layer1));
                ToggleLayer(LayerMask.LayerToName(layer2));


                SetLayerRecursive(other.gameObject, (currentFloorLayer == layer1) ? layer2 : layer1);
            }
        }
    }

    void ToggleLayer(string layerName)
    {
        mainCamera.cullingMask ^= 1 << LayerMask.NameToLayer(layerName);
    }

    void SetLayerRecursive(GameObject go, int layer)
    {
        go.layer = layer;

        foreach(Transform child in go.transform)
        {
            SetLayerRecursive(child.gameObject, layer);
        }
    }
}
