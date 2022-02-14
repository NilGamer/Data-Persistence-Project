using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string userName;
    public string UserName_h;
    public int HighScore;
    public TextMeshProUGUI TextAreaUser;

    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        userName = TextAreaUser.text;
        SceneManager.LoadScene("main");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
    EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }

    //data persistence on session
    [System.Serializable]
    class SaveData
    {
        public string HighScoreUser;
        public int HighScoreG;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.HighScoreUser = UserName_h;
        data.HighScoreG = HighScore;
        
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath+"/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath+"/savefile.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            UserName_h = UserName_h != null ? data.HighScoreUser : "test";
            HighScore = data.HighScoreG;
        }
    }
}
