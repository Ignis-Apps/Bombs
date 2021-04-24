using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] private GameObject progressBarBackground;
    [SerializeField] private GameObject progressbarFill;
    [SerializeField] private bool hideIfEmpty;
    [SerializeField] private bool startInvisible;

    private bool CurrentlyVisible = true;
    private float CurrentProgress;
    private float TargetProgress;


    void Start()
    {
        if (startInvisible)
        {
            SetProgress(0f);
            SetVisibility(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentProgress == TargetProgress) { return; }
       
        CurrentProgress = TargetProgress;
        Vector3 currentFillScale = progressbarFill.transform.localScale;
        currentFillScale.x = CurrentProgress;
        progressbarFill.transform.localScale = currentFillScale;


        if (!CurrentlyVisible &&  CurrentProgress > 0f)
        {
            SetVisibility(true);
        }

        if (CurrentProgress <= 0f && hideIfEmpty)
        {
            SetVisibility(false);
        }

    }

    public void SetProgress(float progress)
    {
        if (progress == TargetProgress) { return; }

        // Clamps values betwenn 0 and 1
        TargetProgress = Mathf.Max(0f, progress);
        TargetProgress = Mathf.Min(TargetProgress, progress);
    }

    
    private void SetVisibility(bool visible)
    {
        if (visible == CurrentlyVisible) { return; }

        CurrentlyVisible = visible;
        if (progressbarFill != null) { progressbarFill?.SetActive(visible); }
        if (progressBarBackground != null) { progressBarBackground?.SetActive(visible); }               
    }

}
