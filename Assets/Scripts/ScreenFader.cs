using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // Reference to the Image component
    [SerializeField] private float fadeDuration = 1.0f; // Duration of the fade

    private bool isFading = false; // To prevent overlapping fade coroutines

    private void Awake()
    {
        if (fadeImage != null)
        {
            SetAlpha(0); // Start with fully transparent image
        }
    }

    public IEnumerator FadeToBlack()
    {
        if (isFading) yield break; // Prevent starting a fade if one is already in progress
        isFading = true;

        yield return Fade(1); // Fade to full black
        isFading = false;
    }

    public IEnumerator FadeToClear()
    {
        if (isFading) yield break; // Prevent starting a fade if one is already in progress
        isFading = true;

        // Fade to clear
        yield return Fade(0); // Fade back to transparent
        isFading = false; // Reset fading status to allow next fade

        // Force reset alpha to 0 immediately after fading to clear
        SetAlpha(0);
        ForceCanvasRepaint();

        // Log fade completion to confirm
        Debug.Log("Fade complete: Forced alpha reset to 0.");
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float elapsed = 0;

        SetAlpha(startAlpha);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            SetAlpha(alpha);

            Debug.Log($"Fading: Target Alpha = {targetAlpha}, Current Alpha = {fadeImage.color.a}");

            yield return null;
        }

        SetAlpha(targetAlpha);
        ForceCanvasRepaint(); // Force a repaint to immediately show the changes
        Debug.Log($"Fade finished: Final Alpha = {fadeImage.color.a}");
    }

    private void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
        }
    }

    private void ForceCanvasRepaint()
    {
        if (fadeImage != null)
        {
            // Force the canvas to update the image immediately
            Canvas.ForceUpdateCanvases();
        }
    }
}







