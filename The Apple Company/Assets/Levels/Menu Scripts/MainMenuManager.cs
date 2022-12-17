using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Button new_game;
    public Button level_select;
    public Button level1;
    public Button level2;
    public Button level3;
    public Button back;
    public Canvas level_menu;
    public Canvas main_menu;

    void Start()
    {
        level_menu.gameObject.SetActive(false);
    }

    void Update()
    {
        new_game.onClick.AddListener(startGame);
        level_select.onClick.AddListener(levelMenu);
        level1.onClick.AddListener(startOne);
        level2.onClick.AddListener(startTwo);
        level3.onClick.AddListener(startThree);
        back.onClick.AddListener(mainMenu);
        if (Input.GetKeyDown(KeyCode.Escape))
            mainMenu();
    }

    void startGame()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    void levelMenu()
    {
        level_menu.gameObject.SetActive(true);
        main_menu.gameObject.SetActive(false);
    }

    void startOne()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    void startTwo()
    {
        SceneManager.LoadScene("Level2", LoadSceneMode.Single);
    }

    void startThree()
    {
        SceneManager.LoadScene("Level3", LoadSceneMode.Single);
    }

    void mainMenu()
    {
        level_menu.gameObject.SetActive(false);
        main_menu.gameObject.SetActive(true);
    }
}
