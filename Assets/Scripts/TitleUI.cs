using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    //[SerializeField] private AudioClip menuMusic;
    [SerializeField] private Image controls;
    private bool areControlsVisible = false;

    private void Update()
    {
        controls.gameObject.SetActive(areControlsVisible);

        if (!areControlsVisible)
            return;

        bool keyboardPressed =
            Keyboard.current != null &&
            Keyboard.current.anyKey.wasPressedThisFrame;

        bool gamepadPressed =
            Gamepad.current != null &&
            Gamepad.current.allControls.Any(c =>
                c is ButtonControl b && b.wasPressedThisFrame);

        if (keyboardPressed || gamepadPressed)
        {
            areControlsVisible = false;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ShowControls()
    {
        areControlsVisible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
