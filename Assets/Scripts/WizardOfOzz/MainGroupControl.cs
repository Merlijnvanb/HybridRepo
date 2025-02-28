using UnityEngine;

public class MainGroupControl : MonoBehaviour
{
    public LiftController liftController;

    public Transform LiftMult;
    
    public float MoveSpeed;
    
    public Vector3 GameStartTarget;
    public Vector3 LiftCheckPoint;
    public Vector3 LiftTarget;
    public Vector3 MusicCheckpoint;
    public Vector3 MusicTarget;
    
    private Vector3 currentPosition;
    private Vector3 currentTarget;

    private bool movingTowardsLift;
    private bool movingTowardsMusic;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        liftController.OnLiftDown += OnLiftDown;
        liftController.OnLiftUp += OnLiftUp;
        OzManager.Instance.OnGameStart += OnOzStart;

        currentPosition = transform.localPosition;
        currentTarget = currentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (OzManager.Instance.CurrentOzState != OzManager.OzState.PLAYING)
            return;

        if (OzManager.Instance.PickedUpKernels)
        {
            var liftY = liftController.transform.localPosition.y;
            LiftMult.localPosition = new Vector3(LiftMult.localPosition.x, liftY, LiftMult.localPosition.z);
        }

        //Debug.Log(currentTarget);

        if (currentPosition != currentTarget)
        {
            Vector3 direction = (currentTarget - currentPosition).normalized;
            float distanceToMove = MoveSpeed * Time.deltaTime;
            
            if (Vector3.Distance(currentPosition, currentTarget) <= distanceToMove)
            {
                transform.localPosition = currentTarget;
                currentPosition = currentTarget;
            }
            else
            {
                currentPosition += direction * distanceToMove;
                transform.localPosition = currentPosition;
            }

            return;
        }
        
        if (movingTowardsLift)
        {
            Debug.Log("reached liftcheckpoint");
            currentTarget = LiftTarget;
            
        }

        if (currentTarget == LiftTarget)
        {
            OzManager.Instance.PickedUpKernels = true;
            movingTowardsLift = false;
        }

        if (movingTowardsMusic)
        {
            Debug.Log("reached music checkpoint");
            currentTarget = MusicTarget;
        }

        if (currentTarget == MusicTarget)
        {
            OzManager.Instance.AtMusic = true;
            movingTowardsMusic = false;
        }
    }

    private void OnOzStart()
    {
        currentTarget = GameStartTarget;
    }

    private void OnLiftDown()
    {
        if (movingTowardsLift || OzManager.Instance.PickedUpKernels)
            return;
        
        movingTowardsLift = true;
        currentTarget = LiftCheckPoint;
    }

    private void OnLiftUp()
    {
        if (!OzManager.Instance.PickedUpKernels || movingTowardsMusic || OzManager.Instance.DeliveredKernels)
            return;
        
        movingTowardsMusic = true;
        OzManager.Instance.DeliveredKernels = true;
        currentTarget = MusicCheckpoint;
    }
}
