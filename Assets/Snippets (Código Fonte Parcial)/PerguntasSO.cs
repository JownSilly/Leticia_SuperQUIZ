using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Quiz/New-Question",
    fileName = "Question -"
    )]
public class PerguntasSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] private string statements;
    [SerializeField] private string[] alternatives;
    [SerializeField] private int rightAnswer;
    [SerializeField] private string id;

    public string GetStatements()
    {
        return statements;
    }
    public string GetId()
    {
        return id;
    }
    public string[] GetAlternatives()
    {
        return alternatives;
    }
    public int getRightAnswer()
    {
        return rightAnswer;
    }

}
