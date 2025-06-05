using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{

    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreen;



    private void Start()
    {
        //if(SaveManager.instance.HaveSaveData() == false)
        //    continueButton.SetActive(false);

        if (SaveManager.instance != null && SaveManager.instance.HaveSaveData())
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }

     
    }

    public void ContinueGame()
    {
        //StartCoroutine(LoadSceneWithfadeEffect(1.5f));
        SceneManager.LoadScene(sceneName);
        Debug.Log("Continue Game");

    }

    public void NewGame()
    {
        SaveManager.instance.DeleteSaveData();
       
        //StartCoroutine(LoadSceneWithfadeEffect(1.5f));

        SceneManager.LoadScene(sceneName);

        Debug.Log("New Game");
    }
    
    public void ExitGame()
    {
        Debug.Log("Exit Game");
    }


    IEnumerator LoadSceneWithfadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }

}
