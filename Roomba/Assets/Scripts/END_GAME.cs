using UnityEngine;

public class END_GAME : MonoBehaviour
{
    private SceneChange _sceneChange;
    private void Start()
    {
        _sceneChange = GetComponent<SceneChange>();
    }
    public void ENDGAME()
    {
        _sceneChange.ChangeScene("Credits");
    }
}
