using UnityEngine;
using System.Collections;

public class LoadingController : MonoBehaviour {
    [SerializeField]
    private UIProgressBar progressBar;
    [SerializeField]
    private UILabel progressLab;
    
    public void ShowLoading()
    {
        this.gameObject.SetActive(true);
    }

    public void HideLoading()
    {
        this.gameObject.SetActive(false);
    }

    public void SetProgress(float progress)
    {
        progressBar.value = progress;
        progressLab.text = (progress*100).ToString();
    }
}
