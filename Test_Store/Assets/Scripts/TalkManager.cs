using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    private void GenerateData()
    {
        talkData.Add(1000, new string[] { "�ȳ�? ó������ ���ε�.", "�� ã�� �����̶� �־�?" });

        talkData.Add(2000, new string[] { "�� ���� ������ ���̴� �������� �� �����ϴ�." });

        talkData.Add(100, new string[] { "����� �������ڴ�." });
    }

    public string GetTalk(int id, int talkindex)
    {
        if(talkindex == talkData[id].Length)
        {
            return null;
        }
        else
            return talkData[id][talkindex];
    }
}
