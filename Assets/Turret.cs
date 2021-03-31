using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Turret
    [SerializeField] private float turretTurnAngle;    
    
    // Projectile
    [SerializeField] private GameObject projectile; 
    [SerializeField] private float initialVelocity; // Velocity of projectiles
    [SerializeField] private float projectileRPS;   // Rounds per secound
    [SerializeField] private float spread;

    // Pipe
    [SerializeField] private GameObject pipe;       // Pipe object
    [SerializeField] private Transform pipeBase;   // Rotation pivot
    [SerializeField] private Transform pipeEnd;
    private Vector3 pipeLocalPosition;
    private Quaternion pipeLocalRotation;

    private ControllerState controllerState;

    private float timeToShoot = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        controllerState = ControllerState.GetInstance();
        controllerState.currentMode = ControllerMode.TURRET;
        pipeLocalPosition = pipe.transform.localPosition;
        pipeLocalRotation = pipe.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(controllerState.currentMode == ControllerMode.TURRET)
        {
       
            float angle;
            Vector2 v1 = new Vector2(0, -1);
            Vector2 v2 = new Vector2(controllerState.stickPositionXRaw, controllerState.stickPositionYRaw);
            float sign = Mathf.Sign(v1.x * v2.y - v1.y * v2.x);
            angle = Vector2.Angle(v1, v2) * sign;
            
            SetAngle(angle);
        }
    
    }

    private void FixedUpdate()
    {
        timeToShoot -= Time.deltaTime;
        if(timeToShoot < 0)
        {
            Shoot();
            timeToShoot = 1 / projectileRPS;
        }
    }

    void SetAngle(float value)
    {
        if(Mathf.Abs(value) > turretTurnAngle) { return; }
        pipe.transform.localPosition = pipeLocalPosition;
        pipe.transform.localRotation = pipeLocalRotation;
        pipe.transform.RotateAround(pipeBase.position, new Vector3(0, 0, 1), value);
    }

    void Shoot()
    {
        GameObject pt = Instantiate(projectile, pipeEnd.position, pipe.transform.rotation);
        Vector2 direction = pipeEnd.position - pipeBase.position;

        direction.x += Random.Range(-spread, spread);
        direction.y += Random.Range(-spread, spread);

        pt.GetComponent<Rigidbody2D>().velocity = direction.normalized * initialVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            Rigidbody2D body = GetComponent<Rigidbody2D>();
            body.velocity = Vector2.zero;
            body.bodyType = RigidbodyType2D.Static;
        }
    }
}
