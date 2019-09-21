using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;
public class GameResultManager : MonoBehaviour
{
    public Text musicTitleUI;
    public Text scoreUI;
    public Text maxComboUI;
    public Image RankUI;
    // Start is called before the first frame update
    void Start()
    {
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

    }

    public void RePlay()
    {
        SceneManager.LoadScene("SongSelect");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

