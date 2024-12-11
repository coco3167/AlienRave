using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
    public Light directionalLight; // Référence à la Directional Light
    public float transitionDuration = 1.0f; // Temps de transition en secondes

    // Variable publique pour stocker une couleur définie dans l'inspecteur
    public Color preciseColorParameter;

    // Méthode appelée par l’Animation Event pour appliquer une couleur précise
    public void ApplyPreciseColor()
    {
        ChangeLightColor(preciseColorParameter);
    }

    // Méthode publique pour changer la couleur de manière fluide
    public void ChangeLightColor(Color targetColor)
    {
        if (directionalLight != null)
        {
            StartCoroutine(ChangeColorRoutine(targetColor));
        }
        else
        {
            Debug.LogWarning("Directional Light is not assigned!");
        }
    }

    // Coroutine pour une transition fluide vers la nouvelle couleur
    private IEnumerator ChangeColorRoutine(Color targetColor)
    {
        Color startColor = directionalLight.color; // Couleur de départ
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            // Interpoler entre la couleur actuelle et la cible
            directionalLight.color = Color.Lerp(startColor, targetColor, t);

            yield return null; // Attendre la prochaine frame
        }

        // Assurer que la couleur finale est exactement celle cible
        directionalLight.color = targetColor;
    }
}
