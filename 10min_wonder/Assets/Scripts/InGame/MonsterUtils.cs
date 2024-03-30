using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonsterUtils
{
    public static GameObject FindClosestMonster(Transform fromPosition, int index)
    {
        List<GameObject> monsters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));
        GameObject closestMonster = null;

        for (int i = 0; i <= index && monsters.Count > 0; i++)
        {
            float closestDistance = Mathf.Infinity;

            foreach (GameObject monster in monsters)
            {
                float distance = Vector2.Distance(fromPosition.position, monster.transform.position);
                if (distance < closestDistance)
                {
                    closestMonster = monster;
                    closestDistance = distance;
                }
            }

            monsters.Remove(closestMonster);
        }

        return closestMonster;
    }
}
