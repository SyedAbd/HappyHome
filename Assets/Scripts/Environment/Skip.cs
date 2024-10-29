using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    // ������ �����, �� ������� �� ������ �������������
    public int sceneIndex;

    // �����, ������� ����� ���������� ��� ������� ������
    public void SwitchScene()
    {
        // ������������ �� ����� �� �������
        SceneManager.LoadScene(sceneIndex);
    }
}
