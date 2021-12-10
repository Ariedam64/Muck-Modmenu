using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using test.CT_Hacks;
using UnityEngine;

namespace test.CT_System
{
    public class LB_Menu : MonoBehaviour
    {
        private bool isOpen = false;
        private List<Menu> menus = new List<Menu>();
        public static PlayerManager[] listeJoueur = new PlayerManager[0];
        public static MenuUI[] listeMenu = new MenuUI[0];
        public static string MAHversion;

        public void Start()
        {
            menus.Add(CManager.hk_Main);
            menus.Add(CManager.hk_Player);
            menus.Add(CManager.hk_itemSpawner);
            menus.Add(CManager.hk_misc);
            menus.Add(CManager.hk_daycycle);
            menus.Add(CManager.hk_powerUp);
            menus.Add(CManager.hk_server);
            menus.Add(CManager.hk_mobspawner);
            menus.Add(CManager.hk_guicolors);
            menus.Add(CManager.hk_waypoints);
            updateCheck();
        }

        public void Update()
        {


            if (UnityEngine.Object.FindObjectsOfType<Tutorial>().Length >= 1)
            {
                UnityEngine.Object.Destroy(Tutorial.Instance.gameObject);
            }
            listeMenu = UnityEngine.Object.FindObjectsOfType<MenuUI>();

            if (listeMenu.Length != 0)
            {
                listeJoueur = new PlayerManager[0];
                if (isOpen)
                {
                    isOpen = !isOpen;
                }
            }
            if (listeMenu.Length == 0 && listeJoueur.Length == 0)
            {
                listeJoueur = UnityEngine.Object.FindObjectsOfType<PlayerManager>();
            }

            if (listeMenu.Length == 0)
            {
                PlayerManager trouve = Array.Find(listeJoueur, x => x.transform == PlayerMovement.Instance.transform);
                int indice = Array.IndexOf(listeJoueur, trouve);
                listeJoueur[indice].username = "YOURSELF";
            }


            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (listeMenu.Length != 0)
                {
                        StatusMessage.Instance.DisplayMessage("You look forward to cheat, try to start a game first :)");
                }
                else {
                    isOpen = !isOpen;
                    if (!isOpen)
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Confined;
                    }
                    else
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                    }
                }
            }
        }

        public void OnGUI()
        {
            if (!isOpen)
                return;

            foreach (Menu menu in menus)
            {
                if (menu.isOpen)
                {
                    GUI.backgroundColor = H_GUIColors.MenuBackgroundColor;
                    GUI.contentColor = H_GUIColors.MenuFrontColor;
                    menu.window = GUILayout.Window(menu.wID, menu.window, menu.runWin, menu.title);
                }
            }
        }
        public static void updateCheck()
        {
            WebClient client = new WebClient();
            string reply = client.DownloadString("https://docs.google.com/document/d/1_xnvb8HyLcy70HIe5o1TZ971YNR2Xubfnf7qhnHyR8k/edit?usp=sharing");
            string reply2 = Variables.deleteAllBefore(reply, "é");
            int index = reply2.IndexOf("è");
            if (index >= 0)
                MAHversion = reply2.Substring(0, index);
        }
    }
}