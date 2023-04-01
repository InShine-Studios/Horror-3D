using UnityEngine;
using UnityEngine.SceneManagement;

namespace Astralization.Menus
{

    /*
     * Class to control Main Menu buttons.
     */
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            Debug.Log("[MENU SYSTEM] QUIT!");
            Application.Quit();
        }
    }
}