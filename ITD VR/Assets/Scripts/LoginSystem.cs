using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    [Header("Error UI")]
    public TextMeshProUGUI errorMessage;

    [Header("Login Credentials")]
    public string correctUsername = "admin";
    public string correctPassword = "1234";

    public void OnLoginPressed()
    {
        string user = usernameInput.text.Trim();
        string pass = passwordInput.text.Trim();

        if (user == correctUsername && pass == correctPassword)
        {
            errorMessage.gameObject.SetActive(false);

            SceneManager.LoadScene("Scene01"); 
        }
        else
        {
            errorMessage.gameObject.SetActive(true);
            CancelInvoke(nameof(HideError));
            Invoke(nameof(HideError), 2f);
        }
    }

    private void HideError()
    {
        errorMessage.gameObject.SetActive(false);
    }
}
