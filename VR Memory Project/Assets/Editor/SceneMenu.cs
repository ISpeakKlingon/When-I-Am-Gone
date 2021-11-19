using UnityEditor;
using UnityEditor.SceneManagement;

public static class SceneMenu
{
    [MenuItem("Scenes/Menu")]
    public static void OpenMenu()
    {
        OpenScene("Menu");
    }

    [MenuItem("Scenes/Game")]
    public static void OpenGame()
    {
        OpenScene("Game");
    }

    [MenuItem("Scenes/Memory2020")]
    public static void OpenMemory2020()
    {
        OpenScene("Memory2020");
    }

    [MenuItem("Scenes/GameOver")]
    public static void OpenGameOver()
    {
        OpenScene("GameOver");
    }

    private static void OpenScene(string sceneName)
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Persistent.unity", OpenSceneMode.Single);
        EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity", OpenSceneMode.Additive);
    }
}
