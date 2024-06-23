using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalPointUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalPointText;


    private void Start()
    {
        totalPointText.text = "0";
    }

    private void Update()
    {
        totalPointText.text = GameManager.Instance.GetTotalPoint().ToString();
    }
}
