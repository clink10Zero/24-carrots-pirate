using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Filtre
{

    public static float[,] MakeFiltre(FiltreStruct filtre, int mapWidth, int mapHeight)
    {
        float[,] filtreMap = new float[mapWidth, mapHeight];

        switch(filtre.type)
        {
            case FiltreStruct.TypeFiltre.bord :
                filtreMap = MakeBord(filtre.moditication, mapWidth, mapHeight);
                break;
            case FiltreStruct.TypeFiltre.centre :
                filtreMap = MakeCentre(filtre.moditication, mapWidth, mapHeight);
                break;

        }

        return filtreMap;
    }

    private static float[,] MakeBord(AnimationCurve curve, int mapWidth, int mapHeight)
    {
        float[,] filtreMap = new float[mapWidth, mapHeight];
        for(int y = 0; y < mapHeight/2; y++)
        {
            for(int x = 0; x < mapWidth/2; x++)
            {
                float fx = (float)x / (float)mapWidth;
                float fy = (float)y / (float)mapHeight;
                
                float xv = curve.Evaluate(fx);
                float yv = curve.Evaluate(fy);

                filtreMap[x, y] = xv * yv;
                filtreMap[x, mapHeight - y - 1] = xv * yv;
                filtreMap[mapWidth - x - 1, y] = xv * yv;
                filtreMap[mapWidth - x - 1, mapHeight - y - 1] = xv * yv;
            }
        }
        return filtreMap;
    }

    private static float[,] MakeCentre(AnimationCurve curve, int mapWidth, int mapHeight)
    {
        float[,] filtreMap = new float[mapWidth, mapHeight];
        for (int y = 0; y < mapHeight / 2; y++)
        {
            for (int x = 0; x < mapWidth / 2; x++)
            {
                float fx = (float)x / (float)mapWidth;
                float fy = (float)y / (float)mapHeight;
                float xv = curve.Evaluate(fx);
                float yv = curve.Evaluate(fy);
                if(yv > xv)
                {
                    filtreMap[x, y] = yv;
                    filtreMap[x, mapHeight - y - 1] = yv;
                    filtreMap[mapWidth - x - 1, y] = yv;
                    filtreMap[mapWidth - x - 1, mapHeight - y - 1] = yv;
                }
                else if (xv > yv)
                {
                    filtreMap[x, y] = xv;
                    filtreMap[x, mapHeight - y - 1] = xv;
                    filtreMap[mapWidth - x - 1, y] = xv;
                    filtreMap[mapWidth - x - 1, mapHeight - y - 1] = xv;
                }
                else
                {
                    filtreMap[x, y] = xv;
                    filtreMap[x, mapHeight - y - 1] = xv;
                    filtreMap[mapWidth - x - 1, y] = xv;
                    filtreMap[mapWidth - x - 1, mapHeight - y - 1] = xv;
                }
            }
        }
        return filtreMap;
    }

    public static float[,] Apply(float[,] noiseMap, float[,] filtre, int mapWidth, int mapHeight)
    {
        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x ++)
            {
                noiseMap[x, y] *= filtre[x, y];
            }
        }

        return noiseMap;
    }

}
