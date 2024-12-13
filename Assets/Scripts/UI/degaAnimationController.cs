using UnityEngine;

public class degaAnimationTrigger : MonoBehaviour
{
    public string targetGameObjectName;
    private Animator targetAnimator;

    void Start()
    {
        GameObject targetGameObject = GameObject.Find(targetGameObjectName);
        if (targetGameObject != null)
        {
            targetAnimator = targetGameObject.GetComponent<Animator>();
        }
    }

    public void TriggerDegaAnimation()
    {
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger("dega");
        }
    }
}
