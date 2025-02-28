using UnityEngine;

public class KernelController : MonoBehaviour
{
    public enum KernelGroup
    {
        MAIN,
        BLOWING,
        INGREDIENTS
    }
    
    public KernelGroup Group;
    
    public bool StartSleeping;
    public bool StartFacingLeft;

    private float cycleOffset;
    private Animator animator;
    private Vector3 currentPosition;
    private Vector3 startScale;
    
    void Start()
    {
        OzManager.Instance.OnMusicCompleted += WakeUp;
        
        if (StartFacingLeft)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        
        cycleOffset = Random.Range(0f, 1f);
        animator = GetComponent<Animator>();
        currentPosition = transform.position;
        startScale = transform.localScale;
        
        animator.SetFloat("AnimOffset", cycleOffset);
        if (StartSleeping)
            animator.SetBool("Sleeping", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPosition.x != transform.position.x)
        {
            animator.SetBool("Moving", true);
            if (transform.position.x > currentPosition.x)
                transform.localScale = new Vector3(-startScale.x, startScale.y, startScale.z);
            else
                transform.localScale = new Vector3(startScale.x, startScale.y, startScale.z);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        
        currentPosition = transform.position;

        if (Group == KernelGroup.MAIN && OzManager.Instance.AtMusic && Input.GetKey(KeyCode.M))
        {
            animator.SetBool("Working", true);
        }
        else 
            animator.SetBool("Working", false);
    }

    void WakeUp()
    {
        animator.SetBool("Sleeping", false);
    }
}
