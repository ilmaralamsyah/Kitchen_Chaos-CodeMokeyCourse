using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveBurnWarningUI : MonoBehaviour
{

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private Image warningImage;

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;


        Hide();
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        bool showWarning = e.progressChanged >= 0.5f && stoveCounter.IsFried();
        if (showWarning)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        warningImage.gameObject.SetActive(true);
    }

    private void Hide()
    {
        warningImage.gameObject.SetActive(false);
    }
}
