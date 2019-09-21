using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    public float noteSpeed;


    public enum judges {  NONE = 0 , BAD, GOOD,PERFECT , MISS}

    public GameObject[] trails;
    private SpriteRenderer[] trailSpriteRenderes;

    public GameObject scoreUI;
    public float score;
    private Text scoreText;

    public GameObject comboUI;
    private int combo;
    public int maxCombo;
    private Text comboText;
    private Animator comboAnimator;

    public GameObject judgeUI;
    private Sprite[] judgeSrpites;
    private Image judgeSpriteRenderer;
    private Animator judgeSpriteAnimator;

    private AudioSource audioSource;
    //public string music = "1";  //음악 파일 이름, 텍스트파일 이름이랑 같게
    //자동 판정 모드
    public bool autoPerfect;

    //리소스폴더에 음악 을 갖고오고 실행. 
    void MusicStart()
    {
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + PlayerInformation.selectedMusic);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }


    private void Start()
    {
        //2초뒤 음악을 갖고오고 실행
        Invoke("MusicStart", 2);


        trailSpriteRenderes = new SpriteRenderer[trails.Length];
        judgeSpriteRenderer = judgeUI.GetComponent<Image>();
        judgeSpriteAnimator = judgeUI.GetComponent<Animator>();
        scoreText = scoreUI.GetComponent<Text>();
        comboText = comboUI.GetComponent<Text>();
        comboAnimator = comboUI.GetComponent<Animator>();

        //판정 결과를 보여주는 스프라이트 이미지를 미리 초기화합니다 jpg든 다 상관없이 스프라이트로 가져온다.
        judgeSrpites = new Sprite[4];
        judgeSrpites[0] = Resources.Load<Sprite>("Sprites/Bad");
        judgeSrpites[1] = Resources.Load<Sprite>("Sprites/Good");
        judgeSrpites[2] = Resources.Load<Sprite>("Sprites/Miss");
        judgeSrpites[3] = Resources.Load<Sprite>("Sprites/Perfect");

        for (int i = 0; i < trails.Length; i++)
        {
            trailSpriteRenderes[i] = trails[i].GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        //사용자가 입력한 키에 해당하는 라인을 빛나게 합니다
        if (Input.GetKey(KeyCode.D)) ShineTrail(0);
        if (Input.GetKey(KeyCode.F)) ShineTrail(1);
        if (Input.GetKey(KeyCode.J)) ShineTrail(2);
        if (Input.GetKey(KeyCode.K)) ShineTrail(3);

        //한번 빛난 라인은 반복적으로 다시 어둡게 처리
        for(int i = 0; i < trailSpriteRenderes.Length; i++)
        {
            Color color = trailSpriteRenderes[i].color;
            color.a -= 0.01f;
            trailSpriteRenderes[i].color = color;
        }

    }

    //해당 키를 누르면 그부분 빛나게한다. 
    public void ShineTrail(int index)
    {
        Color color = trailSpriteRenderes[index].color;
        color.a = 0.32f;
        trailSpriteRenderes[index].color = color;

    }

    //노트 판정이후 판정 결과를 화면에 보여줍니다
    void ShowJudgement()
    {
        //점수 이미지를 보여줍니다
        string scoreFormat = "000000";
        scoreText.text = score.ToString(scoreFormat);
        //판정 이미지를 보여줍니다
        judgeSpriteAnimator.SetTrigger("Show");
        //콤보가 2이상일 때만 콤보 이미지를 보여줍니다
        if (combo >= 2)
        {
            comboText.text = "COMBO " + combo.ToString();
            comboAnimator.SetTrigger("Show");
        }
        if(maxCombo < combo)
        {
            maxCombo = combo; 
        }
    }

    //노트 판정을 진해합니다
    public void processJudge(judges judge, int noteType)
    {
        if (judge == judges.NONE) return;
        //Miss판정을 받은 경우 콤보를 종료하고, 점수를 많이 깍습니다

        if(judge == judges.MISS)
        {
            judgeSpriteRenderer.sprite = judgeSrpites[2];
            combo = 0;
            if (score >= 15) score -= 15;
        }

        //Bad판정을 받은경우 콤보를 종료하고, 점수를 조금 깍습니다
        else if (judge == judges.BAD)
        {
            judgeSpriteRenderer.sprite = judgeSrpites[0];
            combo = 0;
            if (score >= 5) score -= 5;
        }

        //Perfect good을 받은경우 콤보 및 점수를 올립니다
        else
        {
            if(judge == judges.PERFECT)
            {
                judgeSpriteRenderer.sprite = judgeSrpites[3];
                score += 20;
            }
            else if (judge == judges.GOOD)
            {
                judgeSpriteRenderer.sprite = judgeSrpites[1];
                score += 15;
            }
            combo += 1;
            score += (float)combo * 0.1f;
        }
        //UI, 콤보 및 점수를 반영된것을 보여준다.
        ShowJudgement();

    }
}
