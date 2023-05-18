using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Perguntas Visao")]
    private PerguntasSO currentQuestion;
    [SerializeField] private PerguntasSO[] allQuestionScriptableObj;
    [SerializeField] private TextMeshProUGUI statementText;
    [SerializeField] private GameObject[] alternativeTextTMP;
    [Header ("Sprites Alternatives")]
    [SerializeField] private Sprite spriteStandardAlternative;
    [SerializeField] private Sprite spriteRightAlternative;
    [SerializeField] private Sprite spriteWrongAlternative;
    [Header("Visual Alert")]
    [SerializeField] private Sprite spriteVisualAlertWrong;
    [SerializeField] private Sprite spriteVisualAlertRight;
    [SerializeField] private TextMeshProUGUI textVisualAlert;
    [Header("Variaveis Controller")]
    private Timer timer;
    private int index;
    private int points;
    private bool isRight;
    [Header("Screen Change")]
    [SerializeField] private GameObject[] screens;
    [SerializeField] private TextMeshProUGUI endGameMessageTXT;
    void Start()
    {
        index = 0;
        points = 0;
        isRight = false;
        //screens[0].SetActive(true);
        screens[1].SetActive(true);
        screens[2].GetComponent<Canvas>().gameObject.SetActive(false);
        timer = GetComponent<Timer>();
        timer.RegistrarParada(OnParadaTimer);
        currentQuestion = allQuestionScriptableObj[index];
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
        int rightAnswer = currentQuestion.GetRightAnswer();
        Image imageAlt = alternativeTextTMP[selectedAlternative].GetComponent<Image>();
        if (selectedAlternative == rightAnswer)
        {
            ChangeSprite(imageAlt, spriteRightAlternative);
            Debug.Log("ganhou");
            points++;
            isRight = true;
        }
        else
        {
            Image imageAltRight = alternativeTextTMP[rightAnswer].GetComponent<Image>();
            ChangeSprite(imageAlt, spriteWrongAlternative);
            ChangeSprite(imageAltRight, spriteRightAlternative);
            Debug.Log("perdeu");
            isRight = false;
        }
        
        PararTimer();

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
            ChangeSprite(alternativeTextTMP[i].GetComponent<Image>(), spriteStandardAlternative);
        }
        timer.Zerar();
    }
    //troca o sprite dos botoes
    public void ChangeSprite(Image image, Sprite sprite)
    {
        image.sprite = sprite;
    }
    void PararTimer()
    {
        timer.Parar();
    }
    private void OnParadaTimer()
    {
        if (timer.GetCurrentTime() > timer.GetMaxTime())
            isRight = false;
            DisableOptionButtons();
        var visualBoxAlertGameObject = screens[2];
        visualBoxAlertGameObject.GetComponent<Canvas>().gameObject.SetActive(true);
        var imagemAlert = visualBoxAlertGameObject.GetComponentInChildren<Image>();
        
        
        //verifica se ouve clique
        if (isRight) 
        {
            ChangeSprite(imagemAlert, spriteVisualAlertRight);
            textVisualAlert.SetText("Parabéns, meu caro Gafanhoto!\n A resposta Correta é \n\n" + currentQuestion.GetAlternatives()[currentQuestion.GetRightAnswer()]);
        }
        else
        {
            ChangeSprite(imagemAlert, spriteVisualAlertWrong);
            textVisualAlert.SetText("Poxa vida, essa você não acertou!\n A resposta Correta é \n\n" + currentQuestion.GetAlternatives()[currentQuestion.GetRightAnswer()]);
        }
    }
    void SetQuestion(int iQ)
    {
        currentQuestion = allQuestionScriptableObj[iQ];
        statementText.SetText(currentQuestion.GetStatements());
        string[] alternatives = currentQuestion.GetAlternatives();
        for (int i = 0; i < alternatives.Length; i++)
        {
            var setAlternativeText =alternativeTextTMP[i].GetComponentInChildren<TextMeshProUGUI>();
            setAlternativeText.SetText(alternatives[i]);
        }
    }
    public void NewQuestion()
    {
        index++;
        if (index <= allQuestionScriptableObj.Length - 1)
        {
            screens[2].GetComponent<Canvas>().gameObject.SetActive(false);
            SetQuestion(index);
            DefaultOptionButtons();
        }
        else
        {
            EndGame();
        }
            
    }
    /*
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
    */
    void EndGame()
    {
        screens[3].SetActive(true);
        screens[2].SetActive(false);
        screens[1].SetActive(false);
        screens[0].SetActive(false);
        endGameMessageTXT.SetText("Você acertou " + points + " questões de " + allQuestionScriptableObj.Length + "\n Parabéns!!! ");
        
    }
}
