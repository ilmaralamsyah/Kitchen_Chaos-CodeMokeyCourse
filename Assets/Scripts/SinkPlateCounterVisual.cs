using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkPlateCounterVisual : MonoBehaviour
{

    [SerializeField] private Transform cleanedPlateTopPoint;
    [SerializeField] private Transform plateVisual;

    private List<GameObject> plateVisualList;

    private void Awake()
    {
        plateVisualList = new List<GameObject>();
    }

    private void Start()
    {
        SinkCounter.Instance.OnCleanedPlate += SinkCounter_OnCleanedPlate;
        SinkCounter.Instance.OnPickedUpCleanPlate += SinkCounter_OnPickedUpCleanPlate;
    }

    private void SinkCounter_OnPickedUpCleanPlate(object sender, System.EventArgs e)
    {
        GameObject plateVisualGameObject = plateVisualList[plateVisualList.Count - 1];
        plateVisualList.Remove(plateVisualGameObject);
        Destroy(plateVisualGameObject);
    }

    private void SinkCounter_OnCleanedPlate(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisual, cleanedPlateTopPoint);

        float plateYOffset = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateYOffset * plateVisualList.Count, 0);
        plateVisualList.Add(plateVisualTransform.gameObject);
    }
}
