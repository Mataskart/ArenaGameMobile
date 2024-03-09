using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private float timeToAttack = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        StartCoroutine(ContinuousAttack());
    }

    private IEnumerator ContinuousAttack()
    {
        while (true) // This will keep the coroutine running indefinitely
        {
            Attack();
            yield return new WaitForSeconds(timeToAttack); // Wait for the specified time before attacking again
        }
    }

    private void Attack()
    {
        attackArea.SetActive(true);
        // You may want to add any additional attack logic here
        StartCoroutine(DisableAttackAfterDelay());
    }

    private IEnumerator DisableAttackAfterDelay()
    {
        yield return new WaitForSeconds(timeToAttack); // Wait for the specified time before disabling the attack
        attackArea.SetActive(false);
    }
}
