﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    //하나의 노트에 대한 정보를 담는 노트(note) 클래스를 정의

    class Note
    {
        public int noteType { get; set; }
        public int order { get; set; }
        public Note(int  noteType, int order)
        {
            this.noteType = noteType;
            this.order = order;
        }

    }

    public GameObject[] Notes;

    private ObjectPooler noteObjectPooler;
    private List<Note> notes = new List<Note>();
    private float x, z, startY = 8.0f;
    //private float beatInterval = 1.0f;

    //노래에 대한 정보들.
    private string musicTitle;
    private string musicArtist;
    private int bpm;
    private int divider;
    private float startingPoint;
    private float beatCount;
    private float beatInterval; //비트 간격

    void MakeNote(Note note)
    {
        //오브젝트 풀에서 하나를 꺼내고 해당 노트위 위치에 이동.
        GameObject obj = noteObjectPooler.getObject(note.noteType);
        //설정된 시작 라인으로 노트를 이동시킨다.
        x = obj.transform.position.x;
        z = obj.transform.position.z;
        obj.transform.position = new Vector3(x, startY, z);
        //노트판정을 NONE으로 초기화.
        obj.GetComponent<NoteBehavior>().Initialize();
        //노트를 보여준다.
        obj.SetActive(true);
    }

    IEnumerator AwaitMakeNote(Note note)
    {
        //해당 노트를 만들어주고 노트타입설정 및 시간설정
        int noteType = note.noteType;
        int order = note.order;
        //시간만큼 기다린다
        yield return new WaitForSeconds(order * beatInterval);
        //노트 출발
        MakeNote(note);
    }
    void Start()
    {
        noteObjectPooler =gameObject.GetComponent<ObjectPooler>();

        //리소스에서 비트 텍스트 파일을 불러옵니다

        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + GameManager.instance.music);
        //alt + enter 로 iO임포트.
        StringReader reader = new StringReader(textAsset.text);
        //첫번째 줄을 읽는다
        musicTitle = reader.ReadLine();
        //두 번째 줄에 적힌 아티스트 이름을 읽습니다.
        musicArtist = reader.ReadLine();
        // 세번쩨 줄에 적힌 비트 정보 (BPM,Divider,시작시간)을 읽습니다
        //beatInformation : " 160,30,3.5"
        string beatInformation = reader.ReadLine();
        bpm = Convert.ToInt32(beatInformation.Split(' ')[0]);
        divider = Convert.ToInt32(beatInformation.Split(' ')[1]);
        startingPoint = (float)Convert.ToDouble(beatInformation.Split(' ')[2]);
        //1초마다 떨어지는 비트 개수
        beatCount = (float)bpm / divider;
        //비트가 떨어지는 간격 시간
        beatInterval = 1 / beatCount;

        //각비트들이 떨어지는 위치 및 시간 정보를 읽습니다
        string line;
        while((line = reader.ReadLine()) != null)
        {
            //노트를 담는다 +1은 배열은 0,1,2,3 이나 라인은 1,2,3,4로 구분하기 때문.
            Note note = new Note(
                Convert.ToInt32(line.Split(' ')[0]) + 1,
                Convert.ToInt32(line.Split(' ')[1])
                );
            //받아온 놑정보를 리스트에 담는다.
            notes.Add(note);
        }

        //리스트에 담은 모든 노트들을 출발 시킨다.
        //모든 노트를 정해진 시간에 출발하도록 설정
        for (int i = 0; i <notes.Count; i++)
        {
            StartCoroutine(AwaitMakeNote(notes[i]));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
