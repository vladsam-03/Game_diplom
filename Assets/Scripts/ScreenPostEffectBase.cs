using UnityEngine;

// ������ ����� �����������, ����� �� ��������
[ExecuteInEditMode]
// ����������������� ����������� ������ ������ ���� ��������� � ������
[RequireComponent(typeof(Camera))]
// ������������� ������� ����� ��� �������������, �������� ������� - ������������� ������ ��������������� ����� ������ ����������, ����� ������������ ��������, ��������������� �������
public class ScreenPostEffectBase : MonoBehaviour
{

    // ���������� ����� �� ������ ����������
    public Shader shader = null;
    private Material _material = null;

    [System.Obsolete]
    public Material _Material
    {
        get
        {
            if (_material == null)
                _material = GenerateMaterial(shader);
            return _material;
        }
    }

    // �������� ���������� ��� �������� �������� �� ������ �������
    [System.Obsolete]
    protected Material GenerateMaterial(Shader shader)
    {
        // ������������ �� �������
        if (!SystemInfo.supportsImageEffects)
        {
            return null;
        }

        if (shader == null)
            return null;
        // ����� ������, ������������ �� ������
        if (shader.isSupported == false)
            return null;
        Material material = new Material(shader);
        material.hideFlags = HideFlags.DontSave;
        if (material)
            return material;
        return null;
    }

}