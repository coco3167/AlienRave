using UnityEngine;

public class degaAnimationController : MonoBehaviour
{
    [SerializeField]private Animator targetAnimator;

    void Start()
    {
        // Recherchez le GameObject par son tag
        GameObject targetObject = GameObject.FindWithTag("TargetAnimator");
        if (targetObject != null)
        {
            targetAnimator = targetObject.GetComponent<Animator>();
        }
    }

    public void TriggerDegat()
    {
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger("dega");
        }
    }
}
