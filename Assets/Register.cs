using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public InputField ConfirmPasswordInput;
    public Button RegisterButton;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        PasswordInput.onValueChange.AddListener(delegate {
            {
                if (PasswordInput.text == ConfirmPasswordInput.text)
                {
                    RegisterButton.interactable = true;
                }
                else
                {
                    RegisterButton.interactable = false;
                }
            };
        });

        RegisterButton.onClick.AddListener(() =>
        {
            StartCoroutine(Main.Instance.Web.RegisterUser(UsernameInput.text, PasswordInput.text, ConfirmPasswordInput.text));
        });

    }
}
