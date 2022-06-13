// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
 
Shader "Unlit/ColorBSC"
{
         // Блок атрибутов, атрибуты, используемые шейдером, можно настроить прямо на панели Инспектора
	Properties 
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Brightness("Brightness", Float) = 1
		_Saturation("Saturation", Float) = 1
		_Contrast("Contrast", Float) = 1
	}
 
	 // У каждого шейдера есть Subshaer, и каждый Subhaer находится в параллельной взаимосвязи. Может работать только один Subhader, в основном для другого оборудования
	SubShader
	{
		 // Реальная работа - Pass, в шейдере могут быть разные проходы, вы можете выполнить несколько проходов
		Pass
			{
				 // Устанавливаем некоторые состояния рендеринга, здесь подробно не объясняется
				ZTest Always Cull Off ZWrite Off
				
				CGPROGRAM
				 // Содержимое в свойствах только для панели «Инспектор», реальное утверждение здесь, обратите внимание на соответствие приведенному выше
				sampler2D _MainTex;
				half _Brightness;
				half _Saturation;
				half _Contrast;
 
				 // функции vert и frag
				#pragma vertex vert
				#pragma fragment frag
				#include "Lighting.cginc"
 
				 // Параметры, передаваемые в пиксельный шейдер из вершинного шейдера
				struct v2f
				{
					 float4 pos: SV_POSITION; // Положение вершины
					 half2 uv: TEXCOORD0; // UV-координаты
				};
 
				//vertex shader
				 // appdata_img: ввод вершинного шейдера с позицией и координатой текстуры
				v2f vert(appdata_img v)
				{
					v2f o;
					 // поворот из собственного пространства в проекционное пространство
					o.pos = UnityObjectToClipPos(v.vertex);
					 // UV координаты назначены на выход
					o.uv = v.texcoord;
					return o;
				}
 
				//fragment shader
				fixed4 frag(v2f i) : SV_Target
				{
					 // Выборка в соответствии с UV-координатами из _MainTex
					fixed4 renderTex = tex2D(_MainTex, i.uv);
					 // яркость яркости напрямую умножается на коэффициент, который является общим масштабированием RGB для регулировки яркости
					fixed3 finalColor = renderTex * _Brightness;
					 // насыщенность насыщенности: сначала вычисляем самое низкое значение насыщенности при той же яркости по формуле:
					fixed gray = 0.2125 * renderTex.r + 0.7154 * renderTex.g + 0.0721 * renderTex.b;
					fixed3 grayColor = fixed3(gray, gray, gray);
					 // Разница между изображением с самой низкой насыщенностью и исходным изображением по насыщенности
					finalColor = lerp(grayColor, finalColor, _Saturation);
					 // контраст: сначала рассчитать самое низкое значение контраста
					fixed3 avgColor = fixed3(0.5, 0.5, 0.5);
					 // Согласно контрасту, разница между изображением с самой низкой контрастностью и исходным изображением
					finalColor = lerp(avgColor, finalColor, _Contrast);
					 // Возвращаем результат, альфа-канал остается неизменным
					return fixed4(finalColor, renderTex.a);
				}
					ENDCG
		}
	}
	 // Меры безопасности для предотвращения отказа шейдера
	FallBack Off
 
}