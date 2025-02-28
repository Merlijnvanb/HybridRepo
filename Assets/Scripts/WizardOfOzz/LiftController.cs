using UnityEngine;


public class LiftController : MonoBehaviour
{
    public event System.Action OnLiftDown;
    public event System.Action OnLiftUp;
    
    public Vector2 LiftMinMax;
    public float LiftSpeed;

    private int liftSign;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OzManager.Instance.CurrentOzState != OzManager.OzState.PLAYING)
            return;
        
        CheckSign();
        if ((!OzManager.Instance.LiftDown || OzManager.Instance.PickedUpKernels) && !(OzManager.Instance.LiftUp && OzManager.Instance.DeliveredKernels))
            MoveLift();
        CheckDown();
        CheckUp();
    }

    private void CheckSign()
    {
        var upInput = Input.GetKey(KeyCode.UpArrow);
        var downInput = Input.GetKey(KeyCode.DownArrow);

        if (upInput && downInput)
        {
            liftSign = 0;
            return;
        }
        
        if (upInput)
            liftSign = 1;
        else if (downInput)
            liftSign = -1;
        else 
            liftSign = 0;
    }

    private void MoveLift()
    {
        var newY = Mathf.Clamp(transform.localPosition.y + LiftSpeed * liftSign * Time.deltaTime, LiftMinMax.x, LiftMinMax.y);
        
        transform.localPosition = new Vector3(transform.localPosition.x, 
                                              newY, 
                                              transform.localPosition.z);
    }

    private void CheckDown()
    {
        if (transform.localPosition.y <= LiftMinMax.x)
        {
            OzManager.Instance.LiftDown = true;
            OnLiftDown?.Invoke();
        }
        else
        {
            OzManager.Instance.LiftDown = false;
        }
    }

    private void CheckUp()
    {
        if (transform.localPosition.y >= LiftMinMax.y)
        {
            OzManager.Instance.LiftUp = true;
            OnLiftUp?.Invoke();
        }
        else
        {
            OzManager.Instance.LiftUp = false;
        }
    }
}
