using Assets.Scripts.Game.Session;
using Assets.Scripts.UI.Screens.Tutorial;
using System.Collections;
using TMPro;
using UnityEngine;

public class MovementTutorial : AbstractTutorial
{
    private enum STATE {NO_MARKER_REACHED, LEFT_MARKER_REACHED, RIGHT_MARKER_REACHED}

    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;

    [SerializeField] private Animator swipeAnimator;
    [SerializeField] private AnimationClip swipeLeftAnimation;
    [SerializeField] private AnimationClip swipeRightAnimation;

    [SerializeField] private GameObject animatedSwipePoint;

    [SerializeField] private float distance = .2f;

    private GameObject tutorialMarker;
    private STATE state;

    public override void Init()
    {
        GameSessionEventHandler.targetMarkerReachedDelegate += OnMarkerReached;
        GameSessionEventHandler.inputStart += OnInputStarted;
        GameSessionEventHandler.inputStopped += OnInputStopped;
    }

    private void SetLeftMarker()
    {      
        moveLeftText.enabled = true;
        moveRightText.enabled = false;
        
        Vector3 position = gameManager.getPlayer().transform.position;
        position.x -= distance;
        position.y = gameManager.GroundTransform.position.y;
        
        tutorialMarker = Instantiate(tutorialMarkerPrefab);
        tutorialMarker.transform.position = position;
                
        swipeAnimator.Play(swipeLeftAnimation.name);
        
    }

    private void SetRightMarker()
    {             
        moveLeftText.enabled = false;
        moveRightText.enabled = true;

        Vector3 position = gameManager.getPlayer().transform.position;
        position.x += distance;
        position.y = gameManager.GroundTransform.position.y;
        
        tutorialMarker = Instantiate(tutorialMarkerPrefab);
        tutorialMarker.transform.position = position;
               
        swipeAnimator.Play(swipeRightAnimation.name);
    }

    private void OnMarkerReached()
    {                        
        switch (state)
        {
            case STATE.NO_MARKER_REACHED:
                state = STATE.LEFT_MARKER_REACHED;
                break;

            case STATE.LEFT_MARKER_REACHED:
                state = STATE.RIGHT_MARKER_REACHED;
                break;
        }
    }

    IEnumerator RunTutorial()
    {
        state = STATE.NO_MARKER_REACHED;
        SetLeftMarker();
        
        yield return new WaitUntil(() => state == STATE.LEFT_MARKER_REACHED);
        Destroy(tutorialMarker);

        yield return new WaitForSeconds(.1f);

        SetRightMarker();
        yield return new WaitUntil(() => state == STATE.RIGHT_MARKER_REACHED);
        Destroy(tutorialMarker);

        CompleteTutorial();

    }

    private void OnInputStarted()
    {
        animatedSwipePoint.SetActive(false);
    }

    private void OnInputStopped()
    {
        animatedSwipePoint.SetActive(true);
    }

    public override void Dispose()
    {        
        GameSessionEventHandler.targetMarkerReachedDelegate -= OnMarkerReached;
        GameSessionEventHandler.inputStart -= OnInputStarted;
        GameSessionEventHandler.inputStopped -= OnInputStopped;
    }

    protected override void OnTutorialStart()
    {        
        StartCoroutine(RunTutorial());
    }

    protected override void OnTutorialComplete()
    {
        
    }
}
