using UnityEngine;
using System;

namespace ProceduralTexture
{
    public class GenerateTexture2DColorMatrix
    {
        public Texture2D GenerateTexture2d(Color[] colors, int colorMatrix)
        {
            colors = ValidateColorsArray(colors, colorMatrix);

            Texture2D texture = new Texture2D(colorMatrix, colorMatrix);

            texture.SetPixels(colors);

            texture.Apply();

            texture.filterMode = FilterMode.Point;

            return texture;
        }

        private Color[] ValidateColorsArray(Color[] colorsArray, int colorMatrix)
        {
            int colorMatrixLen = colorMatrix * colorMatrix;

            if (colorsArray.Length != colorMatrixLen)
            {
                if (colorsArray.Length < colorMatrixLen)
                {
                    Color[] resizedColors = new Color[colorMatrixLen];
                    for (int i = 0; i < colorsArray.Length; i++)
                    {
                        resizedColors[i] = colorsArray[i];
                    }

                    for (int i = colorsArray.Length; i < colorMatrixLen; i++)
                    {
                        resizedColors[i] = UnityEngine.Random.ColorHSV();
                    }

                    colorsArray = resizedColors;
                }
                else
                {
                    Array.Resize(ref colorsArray, colorMatrixLen);
                }
            }

            return colorsArray;
        }
    }
}
