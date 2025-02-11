using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slam : MonoBehaviour
{
    public float repelForce = 10f; // Сила отталкивания
    public Animator animBobr;

    [SerializeField]
    public Collider enemyCollider; // Коллайдер врага, который находится в зоне

    
    void Update()
    {
        // Проверяем нажатие левой кнопки мыши
        if (Input.GetMouseButtonDown(0)) // 0 — это левая кнопка мыши
        {
            
            animBobr.SetBool("IsSlaming", true);
            Invoke("TurnOffAnim", 0.5f);


            if (enemyCollider != null)
            {
                RepelEnemyObject(enemyCollider);
            }
            
        }
        
    }

    // Когда враг входит в зону коллайдера
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy")) // Проверяем тег врага
        {
            enemyCollider = other; // Запоминаем коллайдер врага
        }
    }

    // Когда враг выходит из зоны коллайдера
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyCollider = null; // Сбрасываем коллайдер врага
        }
    }

    // Метод для отталкивания врага
    void RepelEnemyObject(Collider enemy)
    {
        // Получаем направление от игрока к врагу
        Vector3 repelDirection = enemy.transform.position - transform.position;
        repelDirection.Normalize();

        // Применяем силу к врагу
        Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
        if (enemyRigidbody != null)
        {
            enemyRigidbody.AddForce(repelDirection * repelForce, ForceMode.Impulse);
        }
    }

    public void TurnOffAnim()
    {
        animBobr.SetBool("IsSlaming", false);
       
    }

}
