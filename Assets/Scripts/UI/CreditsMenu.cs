using UnityEngine;
using UnityEngine.EventSystems;

public class CreditsMenu : MonoBehaviour
{
	[SerializeField] private GameObject firstSelectedGameobject;
	[SerializeField] private GameObject onDisableSelectedGameObject;

	private void OnEnable()
	{
		EventSystem.current.SetSelectedGameObject(firstSelectedGameobject);
	}

	private void OnDisable()
	{
		EventSystem.current.SetSelectedGameObject(onDisableSelectedGameObject);
	}

	public void Back()
    {
        gameObject.SetActive(false);
    }
}
