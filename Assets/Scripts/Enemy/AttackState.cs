using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;


    public override void Enter()
    {

    }

    public override void Exit()
    {
    }

    public override void Perfom()
    {
        if (enemy.CanSeePlayer())
        {
            // Look the lose player timer and increment the move and shot timers.
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);

            // if shot timer > fireRate
            if (shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            // Move the enemy to a random position after a random time.
            if (moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
            enemy.LastKnowPos = enemy.Player.transform.position;
        }
        else // Lost sight of player.
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                //change to the search state.
                stateMachine.ChangeState(new SearchState());

            }
        }
    }

    public void Shoot()
    {
        // store reference to the gun barrel.
        Transform gunBullel = enemy.gunBarrel;
        // instantiate a new bullet.
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject,gunBullel.position,enemy.transform.rotation);
        // calculate the direction to the player
        Vector3 shootDirection = (enemy.Player.transform.position - gunBullel.transform.position).normalized;
        // add force rigiboody of the bullet
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f,3f),Vector3.up) * shootDirection * 40;
       
        shotTimer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
