using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActivator : MonoBehaviour
{
    // ������ �� ������, ������� ����� ������������
    public Button myButton;

    // ����� �������� (� ��������)
    public float delay = 5f;

    void Start()
    {
        // ������������ ������ �� ������
        myButton.gameObject.SetActive(false);

        // ��������� �������� ��� ��������� ������
        StartCoroutine(ActivateButtonAfterDelay());
    }

    // ��������, ������� ���������� ������ ����� �������� �����
    IEnumerator ActivateButtonAfterDelay()
    {
        // ���� �������� �����
        yield return new WaitForSeconds(delay);

        // ���������� ������
        myButton.gameObject.SetActive(true);
    }
}
