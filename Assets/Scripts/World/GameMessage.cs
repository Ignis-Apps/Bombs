using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Game;

public class GameMessage : MonoBehaviour
{
    private GameManager gameManager;
    private Animator animator;
    

    private void Start()
    {
        gameManager = GameManager.GetInstance();
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {

        switch (gameManager.getCurrentMessage())
        {
            case GameUIMessageTypes.WAVE_COMPLETE:
                showMessage();                
                break;
        }

    }

    private void showMessage()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        //yield return new WaitForSeconds(2f);
        //this.gameObject.SetActive(true);
        animator.Play("Base Layer.GameMessage_In");
        yield return new WaitForSeconds(2.5f);
        animator.Play("Base Layer.GameMessage_Out");
        yield return new WaitForSeconds(3f);
       // this.gameObject.SetActive(false);
        yield return null;
    }

}
