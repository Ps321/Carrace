using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Image fill;

    public Text resultText;

    private float fillSpeed = 0.05f; // Adjust this value to control the fill speed
    private float targetFillAmount = 1.0f;
    private float currentFillAmount = 0.0f;

    private void Start()
    {
        // Initialize the progress bar
        fill.fillAmount = currentFillAmount;
    }

    private void Update()
    {
        // Update the progress bar fill amount
        if (currentFillAmount < targetFillAmount)
        {
            currentFillAmount += fillSpeed * Time.deltaTime;
            fill.fillAmount += fillSpeed * Time.deltaTime;
            int intFillAmount = Mathf.FloorToInt(currentFillAmount * 100);
            resultText.text = intFillAmount.ToString() + " %";
        }
        else
        {
            SceneManager.LoadScene(11);
        }
    }
}
