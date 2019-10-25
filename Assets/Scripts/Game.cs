using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Game : MonoBehaviour
{  
    public GameObject uiReady;
    public GameObject uiInGame;
    public GameObject uiGameOver;

    public Player player;

    public PipelineManger pipelineManger;

    public Text uiScore;
    public int score;
    public Text uiScore2;
    public int Score
    {
        get { return score; }
        set {

            this.score = value;
            this.uiScore.text = this.score.ToString();
            this.uiScore2.text = this.score.ToString();
        }

    }
    public enum GAME_STATUS
    {
        Ready,
        InGame,
        GameOver,
    }
      private GAME_STATUS status;
    public GAME_STATUS Status
    {
        get { return status; }
        set {
            this.status = value;
            this.UpdateUI();
        }
    }
    // Start is called before the first frame update
    void Start()
    {        
        uiReady.SetActive(true);
        Status = GAME_STATUS.Ready;
        this.player.OnDeath += Player_OnDeath;
        this.player.OnScore = OnPlayerScore;
    }
    void OnPlayerScore(int score)
    {
        this.Score += score;
    }
    private void Player_OnDeath()
    {
        this.Status = GAME_STATUS.GameOver;
        this.pipelineManger.Stop();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        this.Status = GAME_STATUS.InGame;
        
        pipelineManger.StartRun();
        player.Fly();
        Debug.LogFormat("StartGame:{0}",this.Status);


    }

    public void UpdateUI()
    {
        this.uiReady.SetActive(this.Status == GAME_STATUS.Ready);
        this.uiInGame.SetActive(this.Status == GAME_STATUS.InGame);
        this.uiGameOver.SetActive(this.Status == GAME_STATUS.GameOver);
    }

    public void Restart()
    {
        this.Status = GAME_STATUS.Ready;
        this.pipelineManger.Init();
        this.player.Init();
    }
}
