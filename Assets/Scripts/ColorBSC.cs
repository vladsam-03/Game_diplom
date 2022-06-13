using UnityEngine;
using System.Collections;

// Наследуется от PostEffectBase
public class ColorBSC : ScreenPostEffectBase
{

    // Range контролирует диапазон параметров, которые могут быть введены
    [Range(0.0f, 3.0f)]
    public float brightness = 1.0f; // Яркость
    [Range(0.0f, 3.0f)]
    public float contrast = 1.0f; // контраст
    [Range(0.0f, 3.0f)]
    public float saturation = 1.0f; // Насыщенность

    // Перезаписать функцию OnRenderImage
    [System.Obsolete]
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // Постобработка только при наличии материала, если _Material пуст, без постобработки
        if (_Material)
        {
            // Значение параметра в шейдере можно установить с помощью Material.SetXXX («имя», значение)
            _Material.SetFloat("_Brightness", brightness);
            _Material.SetFloat("_Saturation", saturation);
            _Material.SetFloat("_Contrast", contrast);
            // Используем Material для обработки Texture, dest не обязательно экран, эффекты постобработки могут быть наложены!
            Graphics.Blit(src, dest, _Material);
        }
        else
        {
            // Рисуем напрямую
            Graphics.Blit(src, dest);
        }
    }

}