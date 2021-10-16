using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace test.CT_System
{
    public class LB_Menu : MonoBehaviour
    {
        private bool isOpen = false;
        private List<Menu> menus = new List<Menu>();

        public void Start()
        {
            menus.Add(CManager.hk_Main);
            menus.Add(CManager.hk_Player);
            menus.Add(CManager.hk_itemSpawner);
            menus.Add(CManager.hk_misc);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
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

        public void OnGUI()
        {
            if (!isOpen)
                return;

            foreach (Menu menu in menus)
            {
                if (menu.isOpen)
                {
                    menu.window = GUILayout.Window(menu.wID, menu.window, menu.runWin, menu.title);
                }
            }
        }
    }
}