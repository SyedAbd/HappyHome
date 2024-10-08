using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    // Индекс сцены, на которую вы хотите переключиться
    public int sceneIndex;

    // Метод, который будет вызываться при нажатии кнопки
    public void SwitchScene()
    {
        // Переключение на сцену по индексу
        SceneManager.LoadScene(sceneIndex);
    }
}
