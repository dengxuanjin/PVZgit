using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieDaifu : MonoBehaviour
{
    private Animator m_animator;

    private Text Dialogue;

    public GameObject DialoguePanel;

    private void Start()
    {
        //m_animator = GetComponent<Animator>();
        Dialogue = DialoguePanel.transform.Find("DialogueText").GetComponent<Text>();

    }

    private void OnMouseDown()
    {
        //m_animator.SetTrigger("Speak");
        DialoguePanel.SetActive(true);
        Dialogue.DOText("[����]:\n\n����̫��\n��Ҫ�����������\n�ٳԵ������", 4f);
    }

}
