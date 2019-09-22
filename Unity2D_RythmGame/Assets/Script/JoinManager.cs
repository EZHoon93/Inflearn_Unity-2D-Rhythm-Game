using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;

public class JoinManager : MonoBehaviour
{
    //파이어베이스 인증 기능 객체
    private FirebaseAuth auth;

    public InputField emailInputField;
    public InputField passwordInputField;

    //회원가입 결과 UI
    public Text messageUI;

    // Start is called before the first frame update
    void Start()
    {
        //파이어베이스 인증 객체를 초기화합니다
        auth = FirebaseAuth.DefaultInstance;
        messageUI.text = "";
    }

    bool InputCheck()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;
        if(email.Length <8)
        {
            messageUI.text = "이메일을 8글자 이상으로 해주세요";
            return false;
        }
        else if(password.Length < 8)
        {
            messageUI.text = "비밀번호를 8글자 이상으로 해주세요";

            return false;
        }

        messageUI.text = "";
        return true;

    }

    public void Check()
    {
        InputCheck();
    }

    public void Join()
    {
        if (!InputCheck())
        {
            return;
        }
        string email = emailInputField.text;
        string password = passwordInputField.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(
            task =>
            {
                if(!task.IsCanceled && !task.IsFaulted)
                {
                    messageUI.text = "회원가입이 완료 되었습니다";
                    Debug.Log("회원가입완료");
                }
                else
                {
                    messageUI.text = "이미 사용중이거나 형식이 바르지 않습니다.";
                    Debug.Log("이미사용중인 아이디");

                }
            }
            );
    }
        
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
