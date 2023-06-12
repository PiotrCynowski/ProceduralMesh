using UnityEngine;
using System;

namespace ProceduralTexture
{
    public class GenerateTexture2DColorMatrix
    {
        public Texture2D GenerateTexture2d(Color[] colors, int colorMatrix)
        {
            colors = ValidateColorsArray(colors, colorMatrix*colorMatrix);

            Texture2D texture = new Texture2D(colorMatrix, colorMatrix);

            texture.SetPixels(colors);

            texture.Apply();

            texture.filterMode = FilterMode.Point;

            return texture;
        }

        private Color[] ValidateColorsArray(Color[] colorsArray, int colorMatrix)
        {
            if (colorsArray.Length % 2 != 0)
            {
                colorMatrix++;
            }

            if (colorsArray.Length != colorMatrix)
            {
                if (colorsArray.Length < colorMatrix)
                {
                    Color[] resizedColors = new Color[colorMatrix];
                    for (int i = 0; i < colorsArray.Length; i++)
                    {
                        resizedColors[i] = colorsArray[i];
                    }

                    for (int i = colorsArray.Length; i < colorMatrix; i++)
                    {
                        resizedColors[i] = UnityEngine.Random.ColorHSV();
                    }

                    colorsArray = resizedColors;
                }
                else
                {
                    Array.Resize(ref colorsArray, colorMatrix);
                }
            }

            return colorsArray;
        }
    }
}
