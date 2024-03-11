using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float moveSpeed;
    public float destroyTime;
    public float damage;
    public int index = 0;

    TextMesh text;

    void Start()
    {
        text = GetComponent<TextMesh>();
        text.text = damage.ToString();
        Invoke("DestroyObject", destroyTime);

        if (index == 1)
        {
            text.color = Color.red;
            moveSpeed /= 3f;
        }
        if (index == 2)
        {
            text.color = Color.magenta;
            moveSpeed /= 4f;
        }
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
