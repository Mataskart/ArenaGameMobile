using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private float timeUntilAttack=0;

    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        attackArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilAttack -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && timeUntilAttack <= 0)
        {
            Attack();
            SetTimeUntilAttack();
        }
    }

    private void Attack()
    {
        Movement movement = GetComponent<Movement>();
        attackArea.SetActive(true);
        movement.AttackAnim(); 

        Movement playerMovement = GetComponent<Movement>();
        playerMovement.moveSpeed *= 0.3f; // Slow down player movement while attacking
        Invoke("SpeedReturn",0.15f);
        Invoke("DeactivateAttackArea", 0.2f); // Delay the deactivation
    }

    private void DeactivateAttackArea()
    {
        attackArea.SetActive(false);
    }

    private void SetTimeUntilAttack()
    {
        timeUntilAttack = 0.15f;
    }
    private void SpeedReturn()
    {
        Movement playerMovement = GetComponent<Movement>();
        playerMovement.moveSpeed = 3f; // Reset player movement speed to normal
    }
}
