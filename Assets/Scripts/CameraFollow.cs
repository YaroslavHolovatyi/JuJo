using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // ѕосиланн€ на гравц€
    public float smoothSpeed = 2f;  // Ўвидк≥сть руху камери вгору

    private float highestY;         // Ќайвища точка, €коњ дос€г гравець

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

        // якщо гравець п≥дн€вс€ вище Ч п≥дн≥маЇмо камеру
        if (target.position.y > highestY)
        {
            highestY = target.position.y;

            Vector3 desiredPosition = new Vector3(transform.position.x, highestY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }

        // якщо гравець опустивс€ Ч камера не рухаЇтьс€ вниз
    }
    public void UpdateHighestY(float y)
{
    if (y > highestY)
    {
        highestY = y;
    }
}

}
