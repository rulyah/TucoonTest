using UnityEngine;
using UnityEngine.UI;

namespace GUI.Screens
{
    public class MenuScreen : ScreenBase
    {
        [SerializeField] private Button btnNewGame;
        [SerializeField] private Button btnLoadGame;
        
        private void Start()
        {
            btnNewGame.onClick.AddListener(OnNewGameClick);
            btnLoadGame.onClick.AddListener(OnLoadGameClick);
        }
        
        private void OnDestroy()
        {
            btnNewGame.onClick.RemoveListener(OnNewGameClick);
            btnLoadGame.onClick.RemoveListener(OnLoadGameClick);
        }
        
        private void OnNewGameClick() => ScreensManager.instance.Open<LoadingScreen>().New();
        
        private void OnLoadGameClick() => ScreensManager.instance.Open<LoadingScreen>().Load();
    }
}