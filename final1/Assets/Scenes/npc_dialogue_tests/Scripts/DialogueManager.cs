using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void startDialogue(Dialogue dialogue)
    {
        animator.SetBool("open", true);

        //make sure queue is empty
        sentences.Clear();

        //add all sentences to queue
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) //convo over so end
        {
            EndConversation();
            return;
        }
        else //otherwise keep going
        {
            string curr = sentences.Dequeue();
            dialogueText.text = curr;
        }
    }

    private void EndConversation()
    {
        //end convo
        animator.SetBool("open", false);
    }
}
