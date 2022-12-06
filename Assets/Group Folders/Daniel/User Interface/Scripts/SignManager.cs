using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignManager : MonoBehaviour
{
    [SerializeField] private float m_messageDisplayDuration;
    [SerializeField] private Button signInButton;
    private Network network;
    private InputFieldData[] _inputFieldData;
    private InputFieldData _usernameInputField;
    private InputFieldData _passwordInputField;
    private GameObject[] _userInterfaces;
    private bool clicked;

    private void Start()
    {
        network = FindObjectOfType<Network>();
        _userInterfaces = Resources.LoadAll<GameObject>("UserInterfaces");
        _inputFieldData = GetComponentsInChildren<InputFieldData>();
        foreach (var inputFieldData in _inputFieldData)
        {
            switch (inputFieldData.m_inputFieldType)
            {
                case InputFieldType.Username:
                    _usernameInputField = inputFieldData;
                    break;
                case InputFieldType.Password:
                    _passwordInputField = inputFieldData;
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"The inputfieldtype, {inputFieldData.m_inputFieldType} is an invalid type");
            }
        }
    }

    private void OnEnable()
    {
        signInButton.onClick.AddListener(Login);
    }

    private void OnDisable()
    {
        signInButton.onClick.RemoveListener(Login);
    }

    private void Login()
    {
        network._HandleNetworkError(NetworkFeedback.LOGIN_SUCCEEDED);
    }

    public void ShowNotLoggedInMessage()
    {
        // Show not logged in message as a separate UI

    }

    public void ShowIncorrectUsernameMessage()
    {
        _usernameInputField.m_errorMessage.text = $"<color=red>Incorrect username</color>";
        _usernameInputField.m_inputField.image.color = Color.red;
        // Show incorrect username message directly in the UI

    }

    public void ShowIncorrectPasswordMessage()
    {
        _passwordInputField.m_errorMessage.text = $"<color=red>Incorrect password</color>";
        _passwordInputField.m_inputField.image.color = Color.red;
        // Show incorrect password message directly in the UI

    }

    public void ShowLoggedInSuccessMessage()
    {
        // Show as a separate UI after the Sign in button has been clicked
        GameObject loggedInUI = GetUserInterface("LoginSuccess");
        StartCoroutine(ShowMessageTemporarily(loggedInUI));
        transform.parent.gameObject.SetActive(false);
    }

    private GameObject GetUserInterface(string interfaceName)
    {
        return _userInterfaces.FirstOrDefault(userInterface => userInterface.name == interfaceName);
    }

    public void ClearAllErrorMessages()
    {
        _usernameInputField.m_errorMessage.text = "";
        _usernameInputField.m_inputField.image.color = _usernameInputField.m_inputFieldNormalColor;
        _passwordInputField.m_errorMessage.text = "";
        _passwordInputField.m_inputField.image.color = _passwordInputField.m_inputFieldNormalColor;
    }

    private IEnumerator ShowMessageTemporarily(GameObject userInterface)
    {
        GameObject ui = Instantiate(userInterface, transform.parent);
        yield return new WaitForSeconds(m_messageDisplayDuration);
        Destroy(ui);
    }
}
