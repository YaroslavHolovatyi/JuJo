using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAutoFit : MonoBehaviour
{
    public float defaultWidth = 9f;   // ������ � ������� �������� ��� 9:16
    public float defaultHeight = 16f;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        float targetAspect = defaultWidth / defaultHeight;
        float screenAspect = (float)Screen.width / Screen.height;

        if (screenAspect >= targetAspect)
        {
            // ������ ����� � ��������� ��������
            cam.orthographicSize = defaultHeight / 2f;
        }
        else
        {
            // ����� ����� � ����������� ������������ �����
            float size = defaultWidth / screenAspect / 2f;
            cam.orthographicSize = size;
        }
    }
}
