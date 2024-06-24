using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyPlateCounterVisual : MonoBehaviour
{
    [SerializeField] private DirtyPlateCounter dirtyPlateCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform dirtyPlateVisual;

    private List<GameObject> dirtyPlateVisualList;

    private void Awake()
    {
        dirtyPlateVisualList = new List<GameObject>();
    }

    private void Start()
    {
        dirtyPlateCounter.OnSpawnedDirtyPlate += DirtyPlateCounter_OnSpawnedPlate;
        dirtyPlateCounter.OnPickedUpDirtyPlate += DirtyPlateCounter_OnPickedUpPlate;
    }

    private void DirtyPlateCounter_OnPickedUpPlate(object sender, System.EventArgs e)
    {
        GameObject plateVisualGameObject = dirtyPlateVisualList[dirtyPlateVisualList.Count - 1];
        dirtyPlateVisualList.Remove(plateVisualGameObject);
        Destroy(plateVisualGameObject);
    }

    private void DirtyPlateCounter_OnSpawnedPlate(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(dirtyPlateVisual, counterTopPoint);

        float plateYOffset = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateYOffset * dirtyPlateVisualList.Count, 0);
        dirtyPlateVisualList.Add(plateVisualTransform.gameObject);

    }
}
