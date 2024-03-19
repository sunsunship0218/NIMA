using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
  [SerializeField]  TMP_InputField UsernameInput;//使用者填入帳號
  [SerializeField] TMP_InputField PasswordInput;//密碼格
  [SerializeField]  Button  LoginButton;//確認
  [SerializeField] TextMeshProUGUI Message;//顯示訊息
    string LoginURL = "http://localhost/nima/Login.php";//login url

/*
    //Save UserInfo var
    public string InfoJson;
    public string Player;
*/
    void Start()
    {
        Message.text = " ";
        LoginButton.onClick.AddListener(() =>
        {
            LoginUser(UsernameInput.text, PasswordInput.text);
        });

    }
    public void LoginUser(string username, string password)
    {
        UserLoginData userLogin = new UserLoginData();
        userLogin.nickname = username;
        userLogin.password = password;
        string jsonData = JsonUtility.ToJson(userLogin);
        StartCoroutine(LoginStart(jsonData));
    }
    public IEnumerator LoginStart(string jsonData)
    {
        UnityWebRequest www = new UnityWebRequest(LoginURL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);

            Message.text = www.error;
        }
        else
        {
            Debug.Log("Loign Sucess");
            Debug.Log(www.downloadHandler.text);

            //User Json
            UserLoginData response = JsonUtility.FromJson<UserLoginData>(www.downloadHandler.text);
            //Save UserInfoFile
            PlayerData playerData = new PlayerData
            {
                P_id = response.id,
                P_nickname = response.nickname,             
            };
            string playerDataJson = JsonUtility.ToJson(playerData);
            PlayerPrefs.SetString("PlayerData", playerDataJson);
            Debug.Log(playerDataJson);

            /*PlayerPrefs.SetString("PlayerID", response.id);
            PlayerPrefs.SetString("Playername", response.nickname);
            PlayerPrefs.SetString("PlayerAge", response.age);
            PlayerPrefs.SetString("PlayerHeight", response.heightCm);
            PlayerPrefs.SetString("PlayerKg", response.weightKg);
            PlayerPrefs.SetString("PlayerBMI", response.BMI);
            PlayerPrefs.SetString("PlayerStandard_id:", response.standard_id); 
            */
            SceneManager.LoadScene(1);
        }

    }




}

//Backend json class
[Serializable]
public class UserLoginData
{
    public string id;
    public string nickname;
    public string password;
}

//strorePlayerData
[Serializable]
public class PlayerData
{
    public string P_id;
    public string P_nickname;
    public string P_password;
}