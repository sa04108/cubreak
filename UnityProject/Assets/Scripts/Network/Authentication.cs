using Cubreak;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Authentication : MonoBehaviour
{
    [SerializeField] private TMP_Text alarmText;

    [Header("Sign In Panel")]
    [SerializeField] private TMP_InputField signInEmailInput;
    [SerializeField] private TMP_InputField signInPasswordInput;
    [SerializeField] private TMP_InputField signUpEmailInput;
    [SerializeField] private Button signInButton;

    [Header("Sign Up Panel")]
    [SerializeField] private TMP_InputField signUpPasswordInput;
    [SerializeField] private TMP_InputField signUpPasswordConfirmInput;
    [SerializeField] private Button signUpButton;

    [Header("New Password Panel")]
    [SerializeField] private TMP_InputField verificationEmailInput;
    [SerializeField] private Button sendEmailButton;

    private string baseUrl = "http://localhost:3000/auth"; // 서버 주소 (배포 시 도메인/아이피로 교체)

    private void Start()
    {
        alarmText.text = "";

        signInButton.onClick.AddListener(() =>
        {
            StartCoroutine(CoLogin(signInEmailInput.text, signInPasswordInput.text));
        });

        signUpButton.onClick.AddListener(() =>
        {
            if (signUpPasswordInput.text != signUpPasswordConfirmInput.text)
            {
                alarmText.text = "Passwords don't match";
                return;
            }

            StartCoroutine(CoRegister(signUpEmailInput.text, signUpPasswordInput.text));
        });

        sendEmailButton.onClick.AddListener(() =>
        {
            StartCoroutine(CoSendTemporaryPassword(verificationEmailInput.text));
        });
    }

    // 회원가입 요청
    private IEnumerator CoRegister(string email, string password)
    {
        var json = $"{{\"email\":\"{email}\",\"username\":\"{email}\",\"password\":\"{password}\"}}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest req = new UnityWebRequest(baseUrl + "/register", "POST"))
        {
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                alarmText.text = $"Register success";
            }
            else
            {
                alarmText.text = $"Register failed: " + req.downloadHandler.text;
            }
        }
    }

    // 로그인 요청
    private IEnumerator CoLogin(string emailOrUsername, string password)
    {
        var json = $"{{\"emailOrUsername\":\"{emailOrUsername}\",\"password\":\"{password}\"}}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest req = new UnityWebRequest(baseUrl + "/login", "POST"))
        {
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                var reqTxt = req.downloadHandler.text;
                Debug.Log("Log in success");

                // 서버에서 받은 JWT 토큰을 저장
                JObject jobj = JObject.Parse(reqTxt);
                string token = jobj["token"].ToString();

                if (string.IsNullOrEmpty(token))
                {
                    Debug.LogError("Responsed token is null or empty");
                    yield break;
                }

                CustomPlayerPrefs.SetString(ENUM_PLAYERPREFS.JWT_Token, token);

                alarmText.gameObject.SetActive(false);
                UIManager.Instance.GoTitle();
            }
            else
            {
                alarmText.text = "Log in failed. " + req.downloadHandler.text;
            }
        }
    }

    // 임시 비밀번호 요청
    private IEnumerator CoSendTemporaryPassword(string email)
    {
        var json = $"{{\"email\":\"{email}\"}}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest req = new UnityWebRequest(baseUrl + "/reset-password", "POST"))
        {
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                alarmText.text = "Success. Please check your email to find the temporary password";
                Debug.Log("Sent success");
            }
            else
            {
                alarmText.text = "Failed. Check the email address you entered. " + req.downloadHandler.text;
            }
        }
    }
}
