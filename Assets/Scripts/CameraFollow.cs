using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // ��������� �� ������
    public float smoothSpeed = 2f;  // �������� ���� ������ �����

    private float highestY;         // ������� �����, ��� ����� �������

    void Start()
    {
        if (target != null)
        {
            highestY = target.position.y;
            transform.position = new Vector3(transform.position.x, highestY, transform.position.z);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // ���� ������� ������� ���� � ������� ������
        if (target.position.y > highestY)
        {
            highestY = target.position.y;

            Vector3 desiredPosition = new Vector3(transform.position.x, highestY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }

        // ���� ������� ��������� � ������ �� �������� ����
    }
    public void UpdateHighestY(float y)
{
    if (y > highestY)
    {
        highestY = y;
    }
}

}
