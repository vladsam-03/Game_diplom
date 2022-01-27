using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{

    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetText());
        //StartCoroutine(Login("testuser", "123"));
        //StartCoroutine(RegisterUser("testuser4", "123"));

    }

    IEnumerator GetText()
    {
        using UnityWebRequest www = UnityWebRequest.Get("http://localhost/GetUsers.php");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var s = www.downloadHandler.text.Replace("<br>", "\n").Split('\n');
            foreach (var item in s)
                Debug.Log(item);
            byte[] results = www.downloadHandler.data;
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost/Login.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }

    public IEnumerator RegisterUser(string username, string password, string text)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost/RegisterUser.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}
