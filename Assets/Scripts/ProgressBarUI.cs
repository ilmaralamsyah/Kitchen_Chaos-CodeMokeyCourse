using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] IHasProgress hasProgress;
    [SerializeField] Image progressBar;

    [SerializeField] GameObject progressBarInterface;

    private void Awake()
    {
        hasProgress = progressBarInterface.GetComponent<IHasProgress>();
        if (hasProgress == null )
        {
            Debug.LogError("GameObject isnt implement IHasProgress");
        }
    }

    private void Start()
    {
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        progressBar.fillAmount = 0;

        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        progressBar.fillAmount = e.progressChanged;

        if (progressBar.fillAmount == 0 || progressBar.fillAmount == 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
