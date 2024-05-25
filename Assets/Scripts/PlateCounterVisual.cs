using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] PlateCounter plateCounter;
    [SerializeField] Transform counterTopPosition;
    [SerializeField] Transform plateVisualPrefab;


    private List<GameObject> plateGameObjectList;

    private void Awake()
    {
        plateGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        plateCounter.OnSpawnedPlate += PlateCounter_OnSpawnedPlate;
        plateCounter.OnPickedUpPlate += PlateCounter_OnPickedUpPlate;
    }

    private void PlateCounter_OnPickedUpPlate(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = plateGameObjectList[plateGameObjectList.Count - 1];
        plateGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlateCounter_OnSpawnedPlate(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPosition);

        float plateOffsetY = .1f;

        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateGameObjectList.Count, 0);
        plateGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
