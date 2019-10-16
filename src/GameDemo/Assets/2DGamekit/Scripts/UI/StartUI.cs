using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;
namespace Gamekit2D
{
    public class StartUI : MonoBehaviour {


        private void Start()
        {
            Cursor.visible = true;
        }
        public void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
		Application.Quit();
    #endif
        }


        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}
