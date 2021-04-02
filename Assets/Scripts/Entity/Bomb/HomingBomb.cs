using UnityEngine;

namespace Assets.Scripts.Entity.Bomb
{
    public class HomingBomb : Bomb
    {
        [SerializeField] private float rotationSpeed;

        private Transform targetTransform;        
        private float targetAngle;

        public override float GetStartSpeed()
        {
            return gameManager.CurrentWave.GetHomingBombInitialSpeed(gameManager.CurrentWaveProgress);
        }

        protected override void Start()
        {
            base.Start();
            targetTransform = gameManager.getPlayer().transform;

        }


        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            
            bombBody.velocity = -transform.up * bombBody.velocity.magnitude;

            Vector3 startPosition = transform.position;
            Vector3 endPosition = targetTransform.position;
            targetAngle = Vector3.Angle(endPosition - startPosition, new Vector3(0, -1f, 0));

            if (Vector3.Cross(endPosition - startPosition, new Vector3(0, -1f, 0)).z > 0)
            {
                targetAngle = -targetAngle;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), Time.deltaTime * rotationSpeed);


        }
    }
}
