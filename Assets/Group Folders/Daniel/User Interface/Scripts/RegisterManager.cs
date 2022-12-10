using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RegisterManager : MonoBehaviour
{
    [SerializeField] private float m_messageDisplayDuration;
    [SerializeField] private Button m_registerInButton;
    private Network _network;
    private InputFieldData[] _inputFieldData;
    private InputFieldData _emailInputFieldData;
    private InputFieldData _usernameInputField;
    private InputFieldData _passwordInputField;
    private GameObject[] _userInterfaces;
    private void Start()
    {
        _network = FindObjectOfType<Network>();
        _userInterfaces = Resources.LoadAll<GameObject>("UserInterfaces");
        _inputFieldData = GetComponentsInChildren<InputFieldData>();
        foreach (var inputFieldData in _inputFieldData)
        {
            switch (inputFieldData.m_inputFieldType)
            {
                case InputFieldType.Username:
                    _usernameInputField = inputFieldData;
                    break;
                case InputFieldType.Email:
                    _emailInputFieldData = inputFieldData;
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
        m_registerInButton.onClick.AddListener(Register);
    }

    private void OnDisable()
    {
        m_registerInButton.onClick.RemoveListener(Register);
    }

    private void Register()
    {
        _network.Signup("ToastToo", "ToastuserNo122353", "peePeePoopoo3331234");
        /*
        _network.Signup(_usernameInputField.m_inputField.text,
                            _usernameInputField.m_inputField.text,
                            _passwordInputField.m_inputField.text,
                            _emailInputFieldData.m_inputField.text);
                            */
    }

    public void ShowUsernameAlreadyExistsMessage()
    {
        _usernameInputField.m_errorMessage.text = "<color=red>Username already exists!</color>";
        _usernameInputField.m_inputField.image.color = Color.red;
    }

    public void ShowMissingUsernameMessage()
    {
        // Show missing password message directly on the existing UI.
        _usernameInputField.m_errorMessage.text = "<color=red>Username missing!</color>";
        _usernameInputField.m_inputField.image.color = Color.red;
    }

    public void ShowMissingPasswordMessage()
    {
        // Show missing password message directly on the existing UI.
        _passwordInputField.m_errorMessage.text = "<color=red>Password missing!</color>";
        _passwordInputField.m_inputField.image.color = Color.red;
    }

    public void ShowTooShortPasswordMessage()
    {
        // Show missing password message directly on the existing UI.
        _passwordInputField.m_errorMessage.text = "<color=red>Password too short!</color>";
        _passwordInputField.m_inputField.image.color = Color.red;
    }

    public void ShowTooShortUsernameMessage()
    {
        // Show missing password message directly on the existing UI.
        _usernameInputField.m_errorMessage.text = "<color=red>Username too short!</color>";
        _usernameInputField.m_inputField.image.color = Color.red;
    }

    public void ShowRegisteredAccountSuccess()
    {
        // Show after the user clicks on the register button
        GameObject registerSuccess = GetUserInterface("RegisterSuccess");
        StartCoroutine(ShowMessageTemporarily(registerSuccess));
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
        _emailInputFieldData.m_errorMessage.text = "";
        _emailInputFieldData.m_inputField.image.color = _emailInputFieldData.m_inputFieldNormalColor;
    }

    private IEnumerator ShowMessageTemporarily(GameObject userInterface)
    {
        GameObject ui = Instantiate(userInterface, transform.parent);
        yield return new WaitForSeconds(m_messageDisplayDuration);
        Destroy(ui);
        transform.parent.gameObject.SetActive(false);
    }
}
