using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jam.Architecture
{
    public class TitleManager : MonoBehaviour
    {
        public void LoadGame()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}