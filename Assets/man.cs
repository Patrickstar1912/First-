using UnityEngine;

public class man : MonoBehaviour
{
    public float moveSpeed = 5f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 获取输入（-1~1）
        float h = Input.GetAxisRaw("Horizontal");  // A, D / 左右键
        float v = Input.GetAxisRaw("Vertical");    // W, S / 上下键

        // 输入方向向量
        Vector3 dir = new Vector3(h, 0, v).normalized;

        // 若有输入，执行移动
        if (dir.magnitude >= 0.1f)
        {
            controller.Move(dir * moveSpeed * Time.deltaTime);
        }
    }
}