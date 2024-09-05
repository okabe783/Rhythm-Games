using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

//MenuバーにSceneMenuを追加
public static class SceneNavigation
{
    [MenuItem("Scene/Master")]
    public static void OpenScene0()
    {
        EditorSceneManager.SaveOpenScenes();
        OpenScene(0);
    }
    
    [MenuItem("Scene/TitleScene")]
    public static void OpenScene1()
    {
        EditorSceneManager.SaveOpenScenes();
        OpenScene(1);
    }
    
    [MenuItem("Scene/SelectMusicScene")]
    public static void OpenScene2()
    {
        EditorSceneManager.SaveOpenScenes();
        OpenScene(2);
    }

    [MenuItem("Scene/MainScene")]
    public static void OpenScene3()
    {
        EditorSceneManager.SaveOpenScenes();
        OpenScene(3);
    }

    [MenuItem("Scene/Result")]
    public static void OpenScene4()
    {
        EditorSceneManager.SaveOpenScenes();
        OpenScene(4);
    }

    private static void OpenScene(int sceneIndex)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
        if (!string.IsNullOrEmpty(scenePath))
        {
            EditorSceneManager.OpenScene(scenePath);
        }
    }
}
