using UnityEngine;

public class END_GAME : MonoBehaviour
{
    private SceneChange _sceneChange;
    public void ENDGAME()
    {
        _sceneChange.ChangeScene("Credits");
    }
}
