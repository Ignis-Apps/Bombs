using Assets.Scripts.UI.Screens.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupTutorial : AbstractTutorial
{

    [SerializeField] private Button OkButton;
    private float previousTimeScale;

    public override void Dispose()
    {
        
    }

    public override void Init()
    {
        OkButton.onClick.AddListener(CompleteTutorial);
    }

    protected override void OnTutorialComplete()
    {
        Time.timeScale = previousTimeScale;
    }

    protected override void OnTutorialStart()
    {
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }
}
