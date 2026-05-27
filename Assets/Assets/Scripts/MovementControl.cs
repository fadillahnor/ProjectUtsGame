using UnityEngine;

public class MovementControl : MonoBehaviour {
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public Transform[] tyreMeshes = new Transform[4];
    public float maxTorque = 50.0f;
    public Transform centerOfMass;

    [Header("Joystick Input")]
    public GameObject joystickPrefab;
    public Joystick joystick;

    private Rigidbody m_rigidbody;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        if (centerOfMass != null)
            m_rigidbody.centerOfMass = centerOfMass.localPosition;

        if (joystick == null && joystickPrefab != null)
        {
            GameObject instance = Instantiate(joystickPrefab);
            joystick = instance.GetComponent<Joystick>();
            if (joystick == null)
                Debug.LogWarning("Joystick prefab does not contain a Joystick component.");
        }
    }

    private void Update()
    {
        UpdateMeshesPositions();
    }

    private void FixedUpdate()
    {
        float steer = joystick != null ? joystick.Horizontal : Input.GetAxis("Horizontal");
        float acceleration = joystick != null ? joystick.Vertical : Input.GetAxis("Vertical");

        float fixedAngle = steer * 45f;
        if (wheelColliders.Length >= 2)
        {
            wheelColliders[0].steerAngle = fixedAngle;
            wheelColliders[1].steerAngle = fixedAngle;
        }

        for (int i = 0; i < wheelColliders.Length; i++)
        {
            if (wheelColliders[i] != null)
                wheelColliders[i].motorTorque = acceleration * maxTorque;
        }
    }

    private void UpdateMeshesPositions()
    {
        for (int i = 0; i < tyreMeshes.Length && i < wheelColliders.Length; i++)
        {
            if (wheelColliders[i] == null || tyreMeshes[i] == null)
                continue;

            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);
            tyreMeshes[i].position = pos;
            tyreMeshes[i].rotation = quat;
        }
    }
}
