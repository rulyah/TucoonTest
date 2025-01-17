using System;
using System.Collections;
using UnityEngine;

namespace GUI.Screens
{
    public class LoadingScreen : ScreenBase
    {
        public void Load() =>
            StartCoroutine(Delay(() => ScreensManager.instance.Open<GameplayScreen>().LoadGame(), 2f));

        public void New() =>
            StartCoroutine(Delay(() => ScreensManager.instance.Open<GameplayScreen>().NewGame(), 2f));
        
        private IEnumerator Delay(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}