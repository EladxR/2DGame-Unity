using UnityEngine;

public class GameFinishedButtons : MonoBehaviour
{
    public void onMenu()
    {
        Loader.Load("Main");
    }
    public void Exit()
    {
        Application.Quit(); // quit game (ignore in Edit)
    }
}
