using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Register : MonoBehaviour
{
    string RegistURL = "http://localhost/nima/RegisterUser.php";// regist url
    [SerializeField] TMP_InputField UsernameInput;//使用者填入帳號
    [SerializeField] TMP_InputField PasswordInput;//密碼格
    [SerializeField] Button RegisterButton;

    private void Start()
    {
        RegisterButton.onClick.AddListener(() =>
        {
            RegisterUser();
        });
    }
    public void RegisterUser()
    {
        RegistrationData registrationData = new RegistrationData();
        // 註冊字串賦值
        registrationData.LoginUser = UsernameInput.text;
        registrationData.LoginPassword = PasswordInput.text;
      

        //  registrationData to JSON
        string json = JsonUtility.ToJson(registrationData);

        // Post request
        StartCoroutine(SendRegistrationRequest(json));
    }

    IEnumerator SendRegistrationRequest(string json)
    {

        // 建立POS請求
        UnityWebRequest request = new UnityWebRequest(RegistURL, "POST");
        //UTF-8編碼
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        //傳輸數據與Json的Header
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // send request
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("註冊失敗: " + request.error);
        }
        else
        {
            if (request.responseCode == 200) // 成功响应
            {
                Debug.Log("註冊成功: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Registration Error - Status Code: " + request.responseCode);
            }
        }
    }

}
[Serializable]
public class RegistrationData
{
    public string LoginUser;
    public string LoginPassword;
}






