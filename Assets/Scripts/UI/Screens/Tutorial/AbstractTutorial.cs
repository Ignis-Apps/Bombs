using UnityEngine;

namespace Assets.Scripts.UI.Screens.Tutorial
{
    

    public abstract class AbstractTutorial : MonoBehaviour
    {
        public GameObject tutorialMarkerPrefab;
        public bool IsTutorialComplete = false;
        
        protected GameManager gameManager;
        protected FirebaseController firebaseController;

        public void Awake()
        {
            gameManager = GameManager.GetInstance();
            firebaseController = FirebaseController.GetInstance();
        }
                
        public abstract void Init();
        public void StartTutorial()
        {           
            IsTutorialComplete = false;
            OnTutorialStart();
        }

        protected abstract void OnTutorialStart();

        protected void CompleteTutorial() {
            IsTutorialComplete = true;    
            OnTutorialComplete();
        }
        protected abstract void OnTutorialComplete();
               
        public abstract void Dispose();


    }
}
