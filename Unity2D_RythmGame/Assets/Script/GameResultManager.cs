using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
public class GameResultManager : MonoBehaviour
{
    public Text musicTitleUI;
    public Text scoreUI;
    public Text maxComboUI;
    public Image RankUI;

    public Text rank1UI;
    public Text rank2UI;
    public Text rank3UI;

    private bool isSucces;
    List<string> rankList = new List<string>();
    List<string> emailList = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        isSucces = false;
        musicTitleUI.text = PlayerInformation.musicTitle;
        scoreUI.text = "점수: "+ (int)PlayerInformation.socre;
        maxComboUI.text = "최대 콤보 : "+PlayerInformation.maxCombo;

        //리소스에서 비트 텍스트파일을 불러옵니다
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.selectedMusic);
        StringReader reader = new StringReader(textAsset.text);
        //첫번째 줄과 두번재줄을 무시한다
        reader.ReadLine();
        reader.ReadLine();
        //세번째 줄에적힌 비트정보 (S랭크,A랭크,B랭크) 를읽는다
        string beatInformation = reader.ReadLine();
        int scoreS = Convert.ToInt32(beatInformation.Split(' ')[3]);
        int scoreA = Convert.ToInt32(beatInformation.Split(' ')[4]);
        int scoreB = Convert.ToInt32(beatInformation.Split(' ')[5]);

        if(PlayerInformation.socre >= scoreS)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank S");
        }

        else if (PlayerInformation.socre >= scoreA)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank A");
        }

        else if (PlayerInformation.socre >= scoreB)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank B");
        }

        else
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank C");
        }

        rank1UI.text = "데이터를 불러오는 중입니다...";
        rank2UI.text = "데이터를 불러오는 중입니다...";
        rank3UI.text = "데이터를 불러오는 중입니다...";

       DatabaseReference reference = PlayerInformation.GetDatabaseReference().Child("ranks")
            .Child(PlayerInformation.selectedMusic); //랭크스에 접속해서 선택된곡의 번호의 데이터를불러온다
        Debug.Log("데이터 갖고오는중 ");
        //데이터 셋의 모든 데이터를 JSon 형태로 가져온다
        reference.OrderByChild("score").GetValueAsync().ContinueWith(task =>
        {
            //성공적으로 데이터를 자겨온경우
            if (task.IsCompleted)
            {
                
                DataSnapshot snapshot = task.Result;
                //JSON 데이터의 각원소에 접근합니다
                foreach(DataSnapshot data in snapshot.Children)
                {
                    IDictionary rank = (IDictionary)data.Value;
                    //Add는 들어온 순서대로 담기니간 정렬이 필요 => 데이터가 오름차순으로되어있음
                    emailList.Add(rank["email"].ToString());
                    rankList.Add(rank["score"].ToString());

                }
                //정렬 이후 순서를 뒤집어 내림차순으로 정렬
                emailList.Reverse();
                rankList.Reverse();
                isSucces = true;
                //최대 상위 3명의 순위를 차례대로 화면에 출력합니다
                rank1UI.text = "플레이 한 사용자가 없습니다.";
                rank2UI.text = "플레이 한 사용자가 없습니다.";
                rank3UI.text = "플레이 한 사용자가 없습니다.";
               

            }
        });


    }

    public void RePlay()
    {
        SceneManager.LoadScene("SongSelect");
    }
    // Update is called once per frame
    void Update()
    {
        if (isSucces)
        {
            isSucces = false;
            Show();
        }
    }

    void Show()
    {
        List<Text> textList = new List<Text>();
        textList.Add(rank1UI);
        textList.Add(rank2UI);
        textList.Add(rank3UI);
        int count = 1;
        for (int i = 0; i < rankList.Count && i < 3; i++)
        {
            textList[i].text = count + "위: " + emailList[i] + " ( " + rankList[i] + "점 )";
            count = count + 1;
        }
    }
}

