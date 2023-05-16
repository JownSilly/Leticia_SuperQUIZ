using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] private PerguntasSO currentQuestion;
    [SerializeField] private PerguntasSO[] allQuestionScriptableObj;
    [SerializeField] private TextMeshProUGUI statementText;
    [SerializeField] private GameObject[] alternativeTextTMP;
    [Header ("Sprites")]
    [SerializeField] private Sprite SpriteStandardAlternative;
    [SerializeField] private Sprite SpriteRightAlternative;
    [SerializeField] private Sprite SpriteWrongAlternative;
    [Header("Controller")]
    private Timer timer;
    private int questionPosition;
    private int correctly_Answers;
    [SerializeField] private TextMeshProUGUI EndGameMessageTXT;
    [SerializeField] private GameObject[] screens;
    void Start()
    {
        screens[0].SetActive(true);
        screens[1].SetActive(false);
        timer = GetComponent<Timer>();
        timer.RegistrarParada(OnParadaTimer);
        
        questionPosition = 0;
        correctly_Answers = 0;

        statementText.SetText(currentQuestion.GetStatements());
        string[] alternatives = currentQuestion.GetAlternatives();
        for(int i = 0; i < alternatives.Length; i++)
        {
            // Coloca os texto das alternativas nos TextMeshPro que sao filho de cada um dos botoes de alternativas.
            alternativeTextTMP[i].GetComponentInChildren<TextMeshProUGUI>().SetText(alternatives[i]);
        }
    }
    //Gerencia as opçoes selecionadas
    public void HandleOption(int selectedAlternative)
    {
        DisableOptionButtons();
        PararTimer();
        int rightAnswer = currentQuestion.GetRightAnswer();
        Image imageAlt = alternativeTextTMP[selectedAlternative].GetComponent<Image>();
        if (selectedAlternative == rightAnswer)
        {
            ChangeButtonSprite(imageAlt, SpriteRightAlternative);
            correctly_Answers++;
            Debug.Log("ganhou");
        }
        else
        {
            Image imageAltRight = alternativeTextTMP[rightAnswer].GetComponent<Image>();
            ChangeButtonSprite(imageAlt, SpriteWrongAlternative);
            ChangeButtonSprite(imageAltRight, SpriteRightAlternative);
            Debug.Log("perdeu");
        }
    }
    // desabilita os botoes
    public void DisableOptionButtons()
    {
        for(int i = 0; i< alternativeTextTMP.Length; i++)
        {
            alternativeTextTMP[i].GetComponent<Button>().enabled = false;
        }
    }
    public void DefaultOptionButtons()
    {
        for (int i = 0; i < alternativeTextTMP.Length; i++)
        {
            alternativeTextTMP[i].GetComponent<Button>().enabled = true;
        }
        for (int i = 0; i < alternativeTextTMP.Length; i++)
        {
            alternativeTextTMP[i].GetComponent<Image>();
            ChangeButtonSprite(alternativeTextTMP[i].GetComponent<Image>(), SpriteStandardAlternative);
        }
        timer.Zerar();
    }
    //troca o sprite dos botoes
    public void ChangeButtonSprite(Image image, Sprite sprite)
    {
        image.sprite = sprite;
    }
    void PararTimer()
    {
        timer.Parar();
    }
    private void OnParadaTimer()
    {
        Debug.Log(questionPosition);
        if (questionPosition >= allQuestionScriptableObj.Length - 1)
        {
            Invoke("EndGame", 0.5f);
        }
        else
            Invoke("NewQuestion", 0.5f);
    }
    void GenerateQuestion()
    {
        currentQuestion = allQuestionScriptableObj[questionPosition];

        statementText.SetText(currentQuestion.GetStatements());
        string[] alternatives = currentQuestion.GetAlternatives();
        for (int i = 0; i < alternatives.Length; i++)
        {
            // Coloca os texto das alternativas nos TextMeshPro que sao filho de cada um dos botoes de alternativas.
            alternativeTextTMP[i].GetComponentInChildren<TextMeshProUGUI>().SetText(alternatives[i]);
        }
    }
    void NewQuestion()
    {
        questionPosition++;
        GenerateQuestion();
        DefaultOptionButtons();
        timer.Zerar();
    }
    public void RestartGame()
    {
        questionPosition = 0;
        correctly_Answers = 0;
        DefaultOptionButtons();
        GenerateQuestion();
        timer.Zerar();
        screens[0].SetActive(true);
        screens[1].SetActive(false);
    }
    void EndGame()
    {
        EndGameMessageTXT.SetText("Você acertou " + correctly_Answers + " questões de " + allQuestionScriptableObj.Length + "\n Parabéns!!! ");
        screens[1].SetActive(true);
        screens[0].SetActive(false);
    }
    void Update()
    {
        
    }
}
