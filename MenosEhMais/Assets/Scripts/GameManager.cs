using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;
    //public AudioSource source;
    //public AudioClip[] clip;

    public bool pauseIsOpen;
    public GameObject pauseMenu;
    //private int level;

    // Start is called before the first frame update
    void Start() {
		if (instance == null) {
			instance = this;
            //SetMusic(0);
		} else if (instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

        pauseIsOpen = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ButtonFunction("options");
        }
    }

    public void ButtonFunction(string button) {
        switch(button) {
            case "play":
            SceneManager.LoadScene("SampleScene");
            //SetMusic(1);
            //level = 1;
            break;

            case "menu":
            if(pauseIsOpen) {
                ButtonFunction("pause");
            }
            SceneManager.LoadScene("Menu");
            //SetMusic(0);
            break;

            case "exit":
            QuitGame();
            break;

            case "options":
            if (pauseIsOpen) {
                //SetMusic(1);
                Destroy(this.transform.GetChild(0).gameObject);
                pauseIsOpen = false;
            } else {
                //SetMusic(0);
                Instantiate(pauseMenu, transform);
                pauseIsOpen = true;
            }
            break;
            
            case "muteSound":
            
            //SetMusic(1);
            break;

            case "seeResults":
            
                //ScreneManager.LoadScene("EndScreen");
            break;

            case "tryAgain":
            SceneManager.LoadScene("SampleScene");
            //SetMusic(1);
            break;

            default:
            break;
        }
    }

    //0- Menus; 1- Combat; 2- Death;
    /*public void SetMusic(int songVal) {
        source.clip = clip[songVal];
        source.Play(0);
    }*/

    public void QuitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}