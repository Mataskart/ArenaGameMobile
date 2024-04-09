using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private float timeToAttack = 0.5f;
    private Animator anim;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        attackArea = transform.GetChild(0).gameObject;
        attackArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        anim.SetTrigger("isAttacking");
        attackArea.SetActive(true);
        // Add any additional attack logic here

        yield return new WaitForSeconds(timeToAttack); // Wait for the specified time before disabling the attack

        attackArea.SetActive(false);
        anim.ResetTrigger("isAttacking");
        isAttacking = false;

    }
}
