using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActivator : MonoBehaviour
{
    // Ссылка на кнопку, которую нужно активировать
    public Button myButton;

    // Время задержки (в секундах)
    public float delay = 5f;

    void Start()
    {
        // Деактивируем кнопку на старте
        myButton.gameObject.SetActive(false);

        // Запускаем корутину для активации кнопки
        StartCoroutine(ActivateButtonAfterDelay());
    }

    // Корутину, которая активирует кнопку через заданное время
    IEnumerator ActivateButtonAfterDelay()
    {
        // Ждем заданное время
        yield return new WaitForSeconds(delay);

        // Активируем кнопку
        myButton.gameObject.SetActive(true);
    }
}
