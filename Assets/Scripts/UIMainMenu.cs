using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Utils;
using System.Collections.Generic;

public class UIMainMenu : MonoBehaviour
{

    private bool levelSelection = false;
    private bool controlSettings = false;

    private string selectedKey = "";

    private static int mainPageButtonSize_x = 200;
    private static int mainPageButtonSize_y = 60;
    private static int mainPageColumnPos_x = 470;
    private static int mainPageStartPos_y = 150;

    private static int levelSelectionButtonSize_x = 200;
    private static int levelSelectionButtonSize_y = 60;
    private static int levelSelectionStartPos_x = 460;
    private static int levelSelectionStartPos_y = 250;

    private static int controlButtonSize_x = 300;
    private static int controlButtonSize_y = 30;
    private static int controlLeftColumnPos_x = 260;
    private static int controlRightColumnPos_x = 590;
    private static int controlStartPos_y = 50;

    private Dictionary<string, KeyCode> keySettings = GlooConstants.getKeys();

    void Update() {

        if(Input.GetKeyDown(KeyCode.Return) ||Input.GetKeyDown("joystick button 7")) {
            Time.timeScale = 1;
            SceneManager.LoadScene("CinematiqueEntreeScene");
        }
        if (Input.GetKeyDown(KeyCode.G) ||Input.GetKeyDown("joystick button 0")) {
            SceneManager.LoadScene("Generique");
        }

        if(Input.GetKeyDown(KeyCode.P)) {
            controlSettings = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            Application.Quit();
        }



    }



    void OnGUI()
    {
        GUI.backgroundColor = Color.clear;

        if (levelSelection)
        {

            if (GUI.Button(new Rect(levelSelectionStartPos_x, levelSelectionStartPos_y, levelSelectionButtonSize_x, levelSelectionButtonSize_y), "RETOUR"))
            {
                levelSelection = false;
            }

        }

        else if (controlSettings)
        {
            if(selectedKey != "")
            {
                KeyCode newKey = FetchKey();
                if (newKey != KeyCode.None)
                {
                    keySettings[selectedKey] = newKey;
                    selectedKey = "";
                }         
            }

            int count = 0;
            //GUI.skin.GetStyle("label").fontSize = 20;
            foreach (string key in keySettings.Keys)
            {
                int currentPosY = controlStartPos_y + count * (controlButtonSize_y + 20);
                if (selectedKey == key)
                {
                    GUI.contentColor = Color.red;
                }
                else
                    GUI.contentColor = Color.white;
                GUI.Box(new Rect(controlRightColumnPos_x, currentPosY, controlButtonSize_x, controlButtonSize_y), keySettings[key].ToString());
                if (GUI.Button(new Rect(controlLeftColumnPos_x, currentPosY, controlButtonSize_x, controlButtonSize_y), key))
                {
                    selectedKey = key;
                }

                count++;
            }

            if (GUI.Button(new Rect(controlLeftColumnPos_x, controlStartPos_y + count * (controlButtonSize_y + 20), controlButtonSize_x, controlButtonSize_y), "PAR DEFAUT"))
            {
                GlooConstants.resetToDefaultConfig();
                keySettings = GlooConstants.getKeys();
            }

            if (GUI.Button(new Rect(controlRightColumnPos_x , controlStartPos_y + count * (controlButtonSize_y + 20), controlButtonSize_x, controlButtonSize_y), "VALIDER"))
            {
                GlooConstants.setKeys(keySettings);
                controlSettings = false;
            }

        }

        else
        {
            //Quitter 
            if (GUI.Button(new Rect(mainPageColumnPos_x + 2 * mainPageButtonSize_x, mainPageStartPos_y-110, mainPageButtonSize_x, mainPageButtonSize_y), ""))
            {
                Time.timeScale = 1;
                Application.Quit();
            }

            /*if (GUI.Button(new Rect(mainPageColumnPos_x, mainPageStartPos_y + mainPageButtonSize_y + 20, mainPageButtonSize_x, mainPageButtonSize_y), ""))
            {
                levelSelection = true;
            }*/

            //controles
            if (GUI.Button(new Rect(mainPageColumnPos_x + 2* mainPageButtonSize_x -20, mainPageStartPos_y + 2 * (mainPageButtonSize_y + 20) +30 , mainPageButtonSize_x, mainPageButtonSize_y), ""))
            {
                controlSettings = true;
            }
            //jeu
            if (GUI.Button(new Rect(mainPageColumnPos_x+40, mainPageStartPos_y + 3 * (mainPageButtonSize_y + 20) + 40+40, mainPageButtonSize_x, mainPageButtonSize_y+60), ""))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("CinematiqueEntreeScene");
       
            }

            //cinématique 

            if (GUI.Button(new Rect(mainPageColumnPos_x -2* mainPageButtonSize_x -40, mainPageStartPos_y + 3 * (mainPageButtonSize_y + 20) + 40 , mainPageButtonSize_x+50, mainPageButtonSize_y + 40), ""))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("CinematiqueEntreeScene");

            }

            // générique 
            if (GUI.Button(new Rect(mainPageColumnPos_x - 2 * mainPageButtonSize_x -40, mainPageStartPos_y + 5 * (mainPageButtonSize_y + 20) + 40 +10 , mainPageButtonSize_x+40, mainPageButtonSize_y + 40), "") )
            {
                SceneManager.LoadScene("Generique");

            }



            GUI.skin.GetStyle("label").fontSize = 40;
            //GUI.Label(new Rect(mainPageColumnPos_x + 40, mainPageStartPos_y - 100, 300, 100), "GLOO");

        }
    }

    private KeyCode FetchKey()  // warning : tested for computer keybord only
    {
        int e = System.Enum.GetNames(typeof(KeyCode)).Length;
        for (int i = 0; i < e; i++)
        {
            if (Input.GetKey((KeyCode)i))
            {
                return (KeyCode)i;
            }
        }

        return KeyCode.None;
    }
}
