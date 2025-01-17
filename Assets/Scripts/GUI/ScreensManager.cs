using System.Collections.Generic;
using GUI.Screens;
using UnityEngine;

namespace GUI
{
    public class ScreensManager : MonoBehaviour
    {
        [SerializeField] private List<ScreenBase> _screens;

        public static ScreensManager instance { get; private set; }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        private void Start() => Open<MenuScreen>();

        public T Open<T>() where T : ScreenBase
        {
            T currentScreen = default;
            foreach (var screen in _screens)
            {
                if (screen is T s)
                {
                    currentScreen = s;
                    currentScreen.Open();
                }
                else
                    screen.Close();
            }
        
            return currentScreen;
        }
    }
}
