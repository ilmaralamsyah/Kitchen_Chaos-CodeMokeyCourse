using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{

    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisual;

    private List<GameObject> plateVisualList;

    private void Awake()
    {
        plateVisualList = new List<GameObject>();
    }

    private void Start()
    {
        plateCounter.OnSpawnedPlate += PlateCounter_OnSpawnedPlate;
        plateCounter.OnPickedUpPlate += PlateCounter_OnPickedUpPlate;
    }

    private void PlateCounter_OnPickedUpPlate(object sender, System.EventArgs e)
    {
        GameObject plateVisualGameObject = plateVisualList[plateVisualList.Count - 1];
        plateVisualList.Remove(plateVisualGameObject);
        Destroy(plateVisualGameObject);
    }

    private void PlateCounter_OnSpawnedPlate(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisual, counterTopPoint);

        float plateYOffset = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateYOffset * plateVisualList.Count, 0);
        plateVisualList.Add(plateVisualTransform.gameObject);
        
    }
}
