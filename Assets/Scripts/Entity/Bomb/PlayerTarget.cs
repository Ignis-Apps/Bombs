using UnityEngine;

public class PlayerTarget : MonoBehaviour
{

    [SerializeField] private GameObject laser;
    [SerializeField] private bool showLaser;
    [SerializeField] private float rotationSpeed;

    private GameObject player;
    private LineRenderer lineRenderer;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float targetAngle;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = laser.GetComponent<LineRenderer>();
        player = GameManager.GetInstance().Player;
    }

    // Update is called once per frame
    void Update()
    {
        startPosition = laser.transform.position;
        endPosition = player.transform.position;
        targetAngle = Vector3.Angle(endPosition - startPosition, new Vector3(0, -1f, 0));

        if (Vector3.Cross(endPosition - startPosition, new Vector3(0, -1f, 0)).z > 0)
        {
            targetAngle = -targetAngle;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), Time.deltaTime * rotationSpeed);

        if (showLaser)
        {
            lineRenderer.SetPositions(new Vector3[]
                {
                    startPosition,
                    endPosition
                });

        }
    }
}
