using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float cam_sens_rotate; // �������� ��������
    [SerializeField] private float cam_sens_move; // �������� ��������
    [SerializeField] private float cam_wheel;
    [SerializeField] private bool _isBlue;
    [SerializeField] private float YCamPosPlus;
    [SerializeField] private float YCamPosMinus;

    public GameObject Player;
    public float XCamPosPlus = 5;
    public float XCamPosMinus = -17;
    public float ZCamPosPlus = 12;
    public float ZCamPosMinus = -14;

    private Camera cam; // ������
    private float camX;
    private float camY;
    private float camZ;
    private Transform playerTr;
    private Transform camTr;
    private float oldX;
    private float oldZ;
    private float oldY;
    private float ypos;
    private float shag;

    #region MONO

    void Start()
    {
        cam = Camera.main;
        camTr = Camera.main.transform;
        shag = 0.7071065623731628f;
        camX = Player.transform.rotation.x;
        camY = Player.transform.rotation.y;
        ypos = Player.transform.position.y;
        playerTr = Player.transform;
        camZ = Vector3.Distance(Vector3.zero, playerTr.position);
        camTr.rotation = playerTr.rotation;
    }

    #endregion

    #region BODY

    void Update()
    {
        camX = 0f;
        camY = 0f;
        camZ = 0f;
        oldX = playerTr.position.x;
        oldZ = playerTr.position.z;
        oldY = playerTr.position.y;
        // ����� ��������� ����
        // � ��������� - ����������� ����
        float mX = Input.GetAxis("Mouse X");
        float mY = Input.GetAxis("Mouse Y");
        // � ������ �����
        float mW = Input.GetAxis("Mouse ScrollWheel");

        if (mW != 0)
        {
            camZ = mW * cam_wheel;
            // ����� ���������:
            // �������� ��������� �������� ������ (���� �������� ������)
            // �� ���������� ������ "������" ��� "�����"[� ���������� �� ���������������� ��������� ������� �����]
            // � ���������� ��������� � ��������� ������
            playerTr.transform.position += (Vector3)(playerTr.transform.rotation * (camZ > 0 ? Vector3.forward * camZ : Vector3.back * (-camZ)));
            if (_isBlue)
            {
                if (playerTr.transform.position.y <= YCamPosMinus || playerTr.transform.position.y >= YCamPosPlus || playerTr.position.x >= XCamPosPlus + (ypos + 8) * shag || playerTr.position.x <= XCamPosMinus + (ypos - 8) * shag || playerTr.position.z >= ZCamPosPlus + (ypos + 8) * shag || playerTr.position.z <= ZCamPosMinus + (ypos - 8) * shag)
                {
                    playerTr.position = new Vector3(oldX, oldY, oldZ);
                    ypos = oldY;
                }
                else
                {
                    ypos = playerTr.transform.position.y;
                }
            }
            else
            {
                if (playerTr.transform.position.y <= YCamPosMinus || playerTr.transform.position.y >= YCamPosPlus || playerTr.position.x >= XCamPosPlus - (ypos - 8) * shag || playerTr.position.x <= XCamPosMinus - (ypos + 8) * shag || playerTr.position.z >= ZCamPosPlus - (ypos - 8) * shag || playerTr.position.z <= ZCamPosMinus - (ypos + 8) * shag)
                {
                    playerTr.position = new Vector3(oldX, oldY, oldZ);
                    ypos = oldY;
                }
                else
                {
                    ypos = playerTr.transform.position.y;
                }
            }
        }

        // ���� ������ ������� �����
        if (Input.GetAxis("Fire3") > 0)
        {
            bool proverka = true;
            mX = -mX * cam_sens_move;
            mY = -mY * cam_sens_move;
            playerTr.position += (Vector3)(playerTr.rotation * (                      // ��������� ������ (���� �������� ������) �������� ��
                (mX > 0 ? Vector3.right * mX : Vector3.left * (-mX)) +          // ���������� ������ "�����"/"������" [� ���������� �� ���������������� ��������� ������] ����
                (mY > 0 ? Vector3.forward * mY : Vector3.back * (-mY))               // ���������� ������  "�����"/"����" [� ���������� �� ���������������� ��������� ������]
                ));                                                          // �� � ���� ������ ������, ����� position & rotation ��������� ������ ���� ���
            if (_isBlue)
            {
                if (playerTr.position.x >= XCamPosPlus + ypos * shag || playerTr.position.x <= XCamPosMinus + ypos * shag)
                {
                    playerTr.position = new Vector3(oldX, oldY, playerTr.position.z);
                    proverka = false;
                }
                if (playerTr.position.z >= ZCamPosPlus + ypos * shag || playerTr.position.z <= ZCamPosMinus + ypos * shag)
                {
                    playerTr.position = new Vector3(playerTr.position.x, oldY, oldZ);
                    proverka = false;
                }
            }
            else
            {
                if (playerTr.position.x >= XCamPosPlus - ypos * shag || playerTr.position.x <= XCamPosMinus - ypos * shag)
                {
                    playerTr.position = new Vector3(oldX, oldY, playerTr.position.z);
                    proverka = false;
                }
                if (playerTr.position.z >= ZCamPosPlus - ypos * shag || playerTr.position.z <= ZCamPosMinus - ypos * shag)
                {
                    playerTr.position = new Vector3(playerTr.position.x, oldY, oldZ);
                    proverka = false;
                }
            }
            if (proverka == true)
            {
                playerTr.position = new Vector3(playerTr.position.x, ypos, playerTr.position.z);
            }
        }
        camTr.position = playerTr.position;
    }

    #endregion
}
