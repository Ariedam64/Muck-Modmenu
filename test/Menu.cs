using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace test
{
    public class Menu : MonoBehaviour
    {
        public bool isOpen;
        public string title;
        public Rect window;
        public int wID;

        public Menu(Rect window, string title, int wID, bool isOpen)
        {
            this.isOpen = isOpen;
            this.title = title;
            this.window = window;
            this.wID = wID;
        }

        public virtual void runWin(int id)
        {          
            if (id != 0 && id != 5 && id != 6 && id != 2 && id != 7 && id != 9 && id != 8)
            {
                if (GUILayout.Button("Exit"))
                    {
                        isOpen = false;
                    }
            }
                
            GUI.DragWindow();
        }
    }
}