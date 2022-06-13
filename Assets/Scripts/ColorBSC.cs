using UnityEngine;
using System.Collections;

// ����������� �� PostEffectBase
public class ColorBSC : ScreenPostEffectBase
{

    // Range ������������ �������� ����������, ������� ����� ���� �������
    [Range(0.0f, 3.0f)]
    public float brightness = 1.0f; // �������
    [Range(0.0f, 3.0f)]
    public float contrast = 1.0f; // ��������
    [Range(0.0f, 3.0f)]
    public float saturation = 1.0f; // ������������

    // ������������ ������� OnRenderImage
    [System.Obsolete]
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // ������������� ������ ��� ������� ���������, ���� _Material ����, ��� �������������
        if (_Material)
        {
            // �������� ��������� � ������� ����� ���������� � ������� Material.SetXXX (�����, ��������)
            _Material.SetFloat("_Brightness", brightness);
            _Material.SetFloat("_Saturation", saturation);
            _Material.SetFloat("_Contrast", contrast);
            // ���������� Material ��� ��������� Texture, dest �� ����������� �����, ������� ������������� ����� ���� ��������!
            Graphics.Blit(src, dest, _Material);
        }
        else
        {
            // ������ ��������
            Graphics.Blit(src, dest);
        }
    }

}