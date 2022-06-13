using UnityEngine;

// Эффект также срабатывает, когда не работает
[ExecuteInEditMode]
// Послеэкранирующие спецэффекты обычно должны быть привязаны к камере
[RequireComponent(typeof(Camera))]
// Предоставляем базовый класс для постобработки, основная функция - перетаскивать шейдер непосредственно через панель «Инспектор», чтобы генерировать материал, соответствующий шейдеру
public class ScreenPostEffectBase : MonoBehaviour
{

    // Перетащите прямо на панель инспектора
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

    // Создание материалов для экранных эффектов на основе шейдера
    [System.Obsolete]
    protected Material GenerateMaterial(Shader shader)
    {
        // Поддерживает ли система
        if (!SystemInfo.supportsImageEffects)
        {
            return null;
        }

        if (shader == null)
            return null;
        // Нужно судить, поддерживает ли шейдер
        if (shader.isSupported == false)
            return null;
        Material material = new Material(shader);
        material.hideFlags = HideFlags.DontSave;
        if (material)
            return material;
        return null;
    }

}