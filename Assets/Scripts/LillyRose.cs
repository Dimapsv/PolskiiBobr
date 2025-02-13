using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LillyRose : MonoBehaviour
{
    public float moveDownDistance = 1.0f; // Расстояние, на которое объект опускается
    public float moveDownDuration = 0.5f; // Время, в течение которого объект опускается
    public float moveUpDuration = 2.0f; // Время, в течение которого объект поднимается обратно

    private Vector3 initialPosition;
    private bool isMoving = false;

    
    void Start()
    {
        initialPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isMoving)
        {
            StartCoroutine(MoveObject());
        }
    }

    


    private IEnumerator MoveObject()
    {
        yield return new WaitForSeconds(2.0f);
        isMoving = true;

        // Опускаем объект
        Vector3 targetPosition = initialPosition - new Vector3(0, moveDownDistance, 0);
        float elapsedTime = 0f;

        while (elapsedTime < moveDownDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDownDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        // Ждем две секунды
        yield return new WaitForSeconds(2.0f);

        // Поднимаем объект обратно
        elapsedTime = 0f;

        while (elapsedTime < moveUpDuration)
        {
            transform.position = Vector3.Lerp(targetPosition, initialPosition, elapsedTime / moveUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition;
        isMoving = false;
    }

}
