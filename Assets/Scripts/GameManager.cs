using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PerguntasSO currentQuestion;
    [SerializeField] private TextMeshProUGUI statementText;
    [SerializeField] private GameObject[] alternativeTextTMP;
    [Header ("sprites")]
    [SerializeField] private Sprite SpriteRightAlternative;
    [SerializeField] private Sprite SpriteWrongAlternative;

    void Start()
    {
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
        Image imageAlt = alternativeTextTMP[selectedAlternative].GetComponent<Image>();
        int rightAnswer = currentQuestion.GetRightAnswer();
        Image imageAltRight = alternativeTextTMP[rightAnswer].GetComponent<Image>();
        if (selectedAlternative == rightAnswer)
        {
            ChangeButtonSprite(imageAlt, SpriteRightAlternative);
            DisableOptionButtons();
            Debug.Log("ganhou");
        }
        else
        {
            ChangeButtonSprite(imageAlt, SpriteWrongAlternative);
            DisableOptionButtons();
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
    //troca o sprite dos botoes
    public void ChangeButtonSprite(Image image, Sprite sprite)
    {
        image.sprite = sprite;
    }
    void Update()
    {
        
    }
}
