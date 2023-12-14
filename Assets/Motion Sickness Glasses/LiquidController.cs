using UnityEngine;

[ExecuteInEditMode]
public class LiquidController : MonoBehaviour
{
    #region Variables
    private Mesh Mesh { get; set; }
    private Material Material { get; set; }

    private float FillAmount { get; set; } = 0.5f;
    [SerializeField]
    float MaxWobble = 0.03f;
    [SerializeField]
    float WobbleSpeedMove = 1f;
    [SerializeField]
    float Recovery = 1f;
    [SerializeField]
    float Thickness = 1f;

    private Vector3 LastPosition { get; set; }
    private Vector3 Velocity { get; set; }
    private Quaternion LastRotation { get; set; }
    private Vector3 AngularVelocity { get; set; }

    float wobbleAmountX;
    float wobbleAmountZ;
    float wobbleAmountToAddX;
    float wobbleAmountToAddZ;
    float pulse;
    float sinewave;
    float time = 0.5f;
    #endregion

    // Use this for initialization
    private void Start()
    {
        Mesh = GetComponent<MeshFilter>().sharedMesh;
        Material = GetComponent<Renderer>().sharedMaterial;
    }

    // Update is called once per frame
    private void Update()
    {
        float deltaTime = Time.deltaTime;
        time += deltaTime;

        if (deltaTime != 0)
        {
            // decrease wobble over time
            wobbleAmountToAddX = Mathf.Lerp(wobbleAmountToAddX, 0, (deltaTime * Recovery));
            wobbleAmountToAddZ = Mathf.Lerp(wobbleAmountToAddZ, 0, (deltaTime * Recovery));

            // make a sine wave of the decreasing wobble
            pulse = 2 * Mathf.PI * WobbleSpeedMove;
            sinewave = Mathf.Lerp(sinewave, Mathf.Sin(pulse * time), deltaTime * Mathf.Clamp(Velocity.magnitude + AngularVelocity.magnitude, Thickness, 10));

            wobbleAmountX = wobbleAmountToAddX * sinewave;
            wobbleAmountZ = wobbleAmountToAddZ * sinewave;

            // Calculate velocity
            Velocity = (LastPosition - transform.position) / deltaTime;
            // Calculate angular velocity
            AngularVelocity = GetAngularVelocity(LastRotation, transform.rotation);

            // add clamped velocity to wobble
            wobbleAmountToAddX += Mathf.Clamp((Velocity.x + (Velocity.y * 0.2f) + AngularVelocity.z + AngularVelocity.y) * MaxWobble, -MaxWobble, MaxWobble);
            wobbleAmountToAddZ += Mathf.Clamp((Velocity.z + (Velocity.y * 0.2f) + AngularVelocity.x + AngularVelocity.y) * MaxWobble, -MaxWobble, MaxWobble);
        }

        // send it to the shader
        Material.SetFloat("_Rotate_X", wobbleAmountX);
        Material.SetFloat("_Rotate_Z", wobbleAmountZ);

        // set fill amount
        SetTranformPosition(deltaTime);

        // keep last position
        LastPosition = transform.position;
        LastRotation = transform.rotation;
    }

    private void SetTranformPosition(float deltaTime)
    {
        Vector3 worldPosition = transform.TransformPoint(new Vector3(Mesh.bounds.center.x, Mesh.bounds.center.y, Mesh.bounds.center.z));
        Vector3 transformposition = worldPosition - transform.position - new Vector3(0, FillAmount, 0);
        Material.SetVector("_Fill_Amount", transformposition);
    }

    private Vector3 GetAngularVelocity(Quaternion foreLastFrameRotation, Quaternion lastFrameRotation)
    {
        Quaternion quaternion = lastFrameRotation * Quaternion.Inverse(foreLastFrameRotation);

        // If there is no rotation, then return zero.
        // You may want to increase this closer to 1 if you want to handle very small rotations.
        // Beware, if it is too close to one your answer will be NaN
        if (Mathf.Abs(quaternion.w) > 1023.5f / 1024.0f)
        {
            return Vector3.zero;
        }

        float gain;
        // Handle negatives, we could just flip it but this is faster
        if (quaternion.w < 0.0f)
        {
            float angle = Mathf.Acos(-quaternion.w);
            gain = -2.0f * angle / (Mathf.Sin(angle) * Time.deltaTime);
        }
        else
        {
            float angle = Mathf.Acos(quaternion.w);
            gain = 2.0f * angle / (Mathf.Sin(angle) * Time.deltaTime);
        }

        Vector3 angularVelocity = new Vector3(quaternion.x * gain, quaternion.y * gain, quaternion.z * gain);
        if (float.IsNaN(angularVelocity.z))
        {
            angularVelocity = Vector3.zero;
        }
        return angularVelocity;
    }
}