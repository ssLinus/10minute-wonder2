using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DamageText : MonoBehaviour
{
    public float moveSpeed;
    public float destroyTime;
    public float damage;

    TextMesh text;

    void Start()
    {
        text = GetComponent<TextMesh>();
        text.text = damage.ToString();
        Invoke("DestroyObject", destroyTime);
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
