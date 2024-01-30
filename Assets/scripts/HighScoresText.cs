using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HighScoresText : MonoBehaviour
{
    public static HighScoresText instance;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text currentScoreText;
    public float[] topScores = new float[3];
    private int sceneIndex;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;

        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        topScores = new float[3];

        switch (sceneIndex)
        {
            case 1:
                for (int i = 0; i < topScores.Length; i++)
                {
                    topScores[i] = PlayerPrefs.GetFloat("topRun" + i, 50);
                }
                break;
            case 2:
                for (int i = 0; i < topScores.Length; i++)
                {
                    topScores[i] = PlayerPrefs.GetFloat("topJump" + i, 0);
                }
                break;
            case 3:
                for (int i = 0; i < topScores.Length; i++)
                {
                    topScores[i] = PlayerPrefs.GetFloat("topJavelin" + i, 0);
                }
                break;
        }
    }
    void Start()
    {
        Debug.Log(sceneIndex);
        UpdateScoreText();
    }

    public void AddScores(float newScore, int sceneIndex)
    {
        // Load existing scores from PlayerPrefs before making any comparisons
        for (int x = 0; x < topScores.Length; x++)
        {
            switch (sceneIndex)
            {
                case 1:
                    topScores[x] = PlayerPrefs.GetFloat("topRun" + x, 100);
                    break;
                case 2:
                    topScores[x] = PlayerPrefs.GetFloat("topJump" + x, 0);
                    break;
                case 3:
                    topScores[x] = PlayerPrefs.GetFloat("topJavelin" + x, 0);
                    break;
            }
        }

        for (int i = 0; i < topScores.Length; i++)
        {
            if (sceneIndex == 1 && newScore < topScores[i])
            {
                ShiftAndInsert(newScore, i, "topRun");
                break;
            }
            else if (sceneIndex == 2 && newScore > topScores[i])
            {
                ShiftAndInsert(newScore, i, "topJump");
                break;
            }
            else if (sceneIndex == 3 && newScore > topScores[i])
            {
                ShiftAndInsert(newScore, i, "topJavelin");
                break;
            }
        }

        // Move this part outside the main loop to avoid unnecessary reloading
        for (int x = 0; x < topScores.Length; x++)
        {
            switch (sceneIndex)
            {
                case 1:
                    PlayerPrefs.SetFloat("topRun" + x, topScores[x]);
                    break;
                case 2:
                    PlayerPrefs.SetFloat("topJump" + x, topScores[x]);
                    break;
                case 3:
                    PlayerPrefs.SetFloat("topJavelin" + x, topScores[x]);
                    break;
            }
        }

        PlayerPrefs.Save();
    }

    private void ShiftAndInsert(float newScore, int index, string keyPrefix)
    {
        for (int j = topScores.Length - 1; j > index; j--)
        {
            topScores[j] = topScores[j - 1];
        }

        topScores[index] = newScore;

        // PlayerPrefs update is moved to the main loop
        // PlayerPrefs.SetFloat(keyPrefix + index, newScore);
    }

    /*
    public void AddScores(float newScore, int sceneIndex)
    {
        // Load existing scores from PlayerPrefs before making any comparisons
        for (int x = 0; x < topScores.Length; x++)
        {
            switch (sceneIndex)
            {
                case 1:
                    topScores[x] = PlayerPrefs.GetFloat("topRun" + x, 100);
                    break;
                case 2:
                    topScores[x] = PlayerPrefs.GetFloat("topJump" + x, 0);
                    break;
                case 3:
                    topScores[x] = PlayerPrefs.GetFloat("topJavelin" + x, 0);
                    break;
            }
        }

        for (int i = 0; i < topScores.Length; i++)
        {
            if (sceneIndex == 1 && newScore < topScores[i])
            {
                ShiftAndInsert(newScore, i, "topRun");
                break;
            }
            else if (sceneIndex == 2 && newScore > topScores[i])
            {
                ShiftAndInsert(newScore, i, "topJump");
                break;
            }
            else if (sceneIndex == 3 && newScore > topScores[i])
            {
                ShiftAndInsert(newScore, i, "topJavelin");
                break;
            }
        }
    }

    private void ShiftAndInsert(float newScore, int index, string keyPrefix)
    {
        for (int j = topScores.Length - 1; j > index; j--)
        {
            topScores[j] = topScores[j - 1];
        }

        topScores[index] = newScore;

        PlayerPrefs.SetFloat(keyPrefix + index, newScore);
        PlayerPrefs.Save();
    }
    */

    //OLD LEADERBOARD, DOESNT CALCULATE ELEMENT2 CORRECT FOR SOME REASON
    /*
    public void AddScores(float newScore, int sceneIndex)
    {

        for(int i = 0;i < topScores.Length;i++)
        {
            if(sceneIndex == 1)
            {
                if(newScore < topScores[i])
                {
                    for (int j = topScores.Length - 1; j>i; j--)
                    {
                        topScores[j] = topScores[j-1];
                    }

                    topScores[i] = newScore;

                    PlayerPrefs.SetFloat("topRun" + i, newScore);
                    PlayerPrefs.Save();
                    break;
                }
            }

            else if(sceneIndex == 2)
            {
                if(newScore > topScores[i])
                {
                    for (int j = topScores.Length - 1; j > i; j--)
                    {
                        topScores[j] = topScores[j - 1];
                    }

                    topScores[i] = newScore;

                    PlayerPrefs.SetFloat("topJump" + i, newScore);
                    PlayerPrefs.Save();
                    break;
                }
            }

            else if (sceneIndex == 3)
            {
                if (newScore > topScores[i])
                {
                    for (int j = topScores.Length - 1; j > i; j--)
                    {
                        topScores[j] = topScores[j - 1];
                    }

                    topScores[i] = newScore;

                    PlayerPrefs.SetFloat("topJavelin" + i, newScore);
                    PlayerPrefs.Save();
                    break;
                }
            }
            for (int x = 0; x < topScores.Length; x++)
            {
                switch (sceneIndex)
                {
                    case 1:
                        topScores[x] = PlayerPrefs.GetFloat("topRun" + x, 100);
                        break;
                    case 2:
                        topScores[x] = PlayerPrefs.GetFloat("topJump" + x, 0);
                        break;
                    case 3:
                        topScores[x] = PlayerPrefs.GetFloat("topJavelin" + x, 0);
                        break;
                }
            }
        }
    }
    */

    public void UpdateScoreText()
    {
        highScoreText.text = "";

        switch (sceneIndex)
        {
            case 1:
                for (int i = 0; i < topScores.Length; i++)
                {
                    highScoreText.text += (i + 1) + ". " + topScores[i].ToString("F") + "s" + "\n";
                }
                break;
            case 2:
                for (int i = 0; i < topScores.Length; i++)
                {
                    highScoreText.text += (i + 1) + ". " + topScores[i].ToString("F") + "m" + "\n";
                }
                break;
            case 3:
                for (int i = 0; i < topScores.Length; i++)
                {
                    highScoreText.text += (i + 1) + ". " + topScores[i].ToString("F") + "m" + "\n";
                }
                break;
        }
    }

    public void UpdateCurrentScoreText(float score, int sceneIndex)
    {
        currentScoreText.text = "";

        switch (sceneIndex)
        {
            case 1:
                currentScoreText.text = "100m time: " + score.ToString("F") + "s";
            break;

            case 2:
                currentScoreText.text = "Jump distance: " + score.ToString("F") + "m";
            break;

            case 3:
                currentScoreText.text = "Javelin distance: " + score.ToString("F") + "m";
            break;

        }
    }
}
