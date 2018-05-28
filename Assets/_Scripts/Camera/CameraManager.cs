using Player.Management;
using Spawners;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager m_playerManager = null;
        [SerializeField] private Vector3 m_offset = Vector3.zero;
        [SerializeField] private float m_smoothTime = 0.5f;

        [SerializeField] private float m_minZoom = 5f;
        [SerializeField] private float m_maxZoom = 5f;
        [SerializeField] private float m_zoomLimit = 50f;

        [SerializeField] private float m_fieldOfView = 1f;

        private Transform[] m_cameraTargets;

        private Camera m_camera = null;
        private Vector3 m_cameraVelocity;

        private bool m_isOrthographic = true;

        // Use this for initialization
        private void Awake()
        {
            m_camera = GetComponent<Camera>();

            m_isOrthographic = m_camera.orthographic;
        }

        public void Start()
        {
            m_cameraTargets = new Transform[m_playerManager.Length];
            for (int i = 0; i < m_cameraTargets.Length; i++)
                m_cameraTargets[i] = m_playerManager.GetPlayer(i).transform;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            if (m_cameraTargets.Length <= -1)
                return;

            Move();

            Zoom();
        }

        private void Move()
        {
            Vector3 centerPoint = GetCenterPoint();

            Vector3 startPosition = transform.position;
            Vector3 targetPosition = centerPoint + m_offset;

            transform.position = Vector3.SmoothDamp(startPosition, targetPosition, ref m_cameraVelocity, m_smoothTime);
        }

        private void Zoom()
        {
            float targetZoom = Mathf.Lerp(m_minZoom, m_maxZoom, GetGreatestDistance() / m_zoomLimit);

            if (m_isOrthographic)
                m_camera.orthographicSize = Mathf.Lerp(m_camera.orthographicSize, targetZoom, Time.deltaTime * m_fieldOfView);
            else
                m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, targetZoom, Time.deltaTime * m_fieldOfView);
        }

        private float GetGreatestDistance()
        {
            if (m_cameraTargets.Length <= 0)
                return 0f;

            Bounds bounds = new Bounds(m_cameraTargets[0].position, Vector3.zero);
            for (int i = 0; i < m_cameraTargets.Length; i++)
                bounds.Encapsulate(m_cameraTargets[i].position);

            return bounds.size.x;
        }

        private Vector3 GetCenterPoint()
        {
            if (m_cameraTargets.Length <= 0)
                return Vector3.zero;

            if (m_cameraTargets.Length == 1)
                return m_cameraTargets[0].position;

            Bounds bounds = new Bounds(m_cameraTargets[0].position, Vector3.zero);
            for (int i = 0; i < m_cameraTargets.Length; i++)
                bounds.Encapsulate(m_cameraTargets[i].position);

            return bounds.center;
        }
    }
}