using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEditor.EventSystems;
using UnityEngine.Events;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreHandler : MonoBehaviour
{
    public delegate void ScoreChangeAction(int deltaScore);
    public event ScoreChangeAction ChangeScore;
    public delegate void ScoreChangeReaction();
    public event ScoreChangeReaction OnScoreChange;

    private EventHandler itemsProcessed;
    public event EventHandler ItemsProcessed
    {
        add
        {
            itemsProcessed -= value;
            itemsProcessed += value;
        }

        remove
        {
            itemsProcessed -= value;
        }
    }
    public int score;

    public Text scoreText;

    private void Awake()
    {
        ChangeScore += changeScore;
        OnScoreChange += updateScoreBoard;
    }

    public void changeScore(int deltaScore)
    {
        score += deltaScore;

        OnScoreChange();
    }

    void updateScoreBoard()
    {
        scoreText.text = "x " + score;
    }
}
