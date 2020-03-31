using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStage {
    Start = 0,
    HeadFlyIn,
    Work,
    HeadYeet,
}

public class GameLoop : Singleton<GameLoop>
{
    public GameObject HeadofHairPrefab;
    [SerializeField]
    Transform headStartTransform;
    [SerializeField]
    Transform headHairCutTransform;
    [SerializeField]
    Transform headYeetToTransform;
    [SerializeField]
    AudioSource thankyouSFX;
    [SerializeField]
    AudioSource fuckyouSFX;
    [SerializeField]
    AudioSource justfuckmyshitupSFX;
    [SerializeField]
    AudioSource bellDongSFX;


    public GameObject JFMSUSign;
    [SerializeField]

    public GrowButton GrowButton;
    public GrowButton ShrinkButton;
    public bool GameStart { get; private set; }

    float showSignDuration = 4;

    GameObject TheHead = null;

    bool headIsUpset;
    bool headIsVeryHappy;

    float headUpsetRotateSpeed = 500.0f;
    float headHappyRotateSpeed = 300.0f;

    float stageStartTime = 0;
    GameStage _gameStage = GameStage.Start;
    GameStage gameStage { 
        get { return _gameStage; } 
        set {
            if (_gameStage != value) {
                switch (value) {
                    case GameStage.HeadFlyIn:
                        TheHead = GameObject.Instantiate(HeadofHairPrefab, headStartTransform.position, headStartTransform.rotation);
                        StartCoroutine(play_intro_sound_cr());
                        break;
                    case GameStage.Work:
                        GameStart = true;
                        break;
                    case GameStage.HeadYeet:
                        int number = Random.Range(0, 10);
                        headIsUpset = number == 0;
                        if (headIsUpset)
                        {
                            fuckyouSFX.Play();
                        }
                        else
                        {
                            number = Random.Range(0, 10);
                            headIsVeryHappy = number <= 3;
                            thankyouSFX.Play();
                        }
                        break;
                }

                JFMSUSign.SetActive(value == GameStage.Work);
                
                stageStartTime = Time.time;
                _gameStage = value; 
                Debug.Log("New Game Stage: " + _gameStage);
            }
        }
    }

    public void DonePressed() {
        gameStage = GameStage.HeadYeet;
    }

    public void BellDonged() {
        Debug.Log("BellDonged");
        if (gameStage == GameStage.Start) {
            gameStage = GameStage.HeadFlyIn;
        }
        if (gameStage == GameStage.Work) {
            gameStage = GameStage.HeadYeet;
        }
        bellDongSFX.Play();

    }

    void Update() {
        switch (gameStage) {
            case GameStage.Start:
                //Uncomment for autoplay
                // gameStage = GameStage.HeadFlyIn;
                break;
            case GameStage.HeadFlyIn: 
                {
                    float flightDuration = 3;
                    float flightFraction = Mathf.Min(1, (Time.time - stageStartTime) / flightDuration);
                    TheHead.transform.position = Vector3.Lerp(headStartTransform.position, 
                        headHairCutTransform.position, flightFraction);
                    TheHead.transform.rotation = Quaternion.Lerp(headStartTransform.rotation, 
                        headHairCutTransform.rotation, flightFraction);
                }
                if (( headHairCutTransform.position - TheHead.transform.position).magnitude < 0.1f ) {
                    TheHead.transform.position = headHairCutTransform.position;
                    TheHead.transform.rotation = headHairCutTransform.rotation;
                    gameStage = GameStage.Work;
                }
                break;
            case GameStage.Work:
                if (Time.time - stageStartTime > 5) {
                    JFMSUSign.SetActive(false);
                }

                //TODO: could check if all not StrandColler.IsWorkable, then yeet earlier.
                break;
            case GameStage.HeadYeet:
                if (TheHead)
                {
                    float flightDuration = 3;
                    float flightFraction = Mathf.Min(1, (Time.time - stageStartTime) / flightDuration);
                    TheHead.transform.position = Vector3.Lerp(headHairCutTransform.position, 
                        headYeetToTransform.position, flightFraction);

                    //    TheHead.transform.rotation = Quaternion.Lerp(headHairCutTransform.rotation,
                    //        headYeetToTransform.rotation, flightFraction);

                    if (( headYeetToTransform.position - TheHead.transform.position).magnitude < 0.1f ) {
                        TheHead.transform.position = headHairCutTransform.position;
                        TheHead.transform.rotation = headHairCutTransform.rotation;
                        
                        Destroy(TheHead);
                        TheHead = null; //redundant with above line???
                    }

                    if (TheHead != null)
                    {
                        if (headIsUpset)
                        {
                            TheHead.transform.Rotate(Vector3.forward * (headUpsetRotateSpeed * Time.deltaTime));
                        }
                        else if (headIsVeryHappy)
                        {
                            TheHead.transform.Rotate(Vector3.up * (headHappyRotateSpeed * Time.deltaTime));
                        }
                    }
                }

                if (Time.time - stageStartTime > 5) {
                    gameStage = GameStage.HeadFlyIn;
                }

                break;
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            DonePressed();
        }
        
    }

    private IEnumerator play_intro_sound_cr()
    {
        yield return new WaitForSeconds(2.0f);
        justfuckmyshitupSFX.Play();
    }

}
