using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CodeTest : MonoBehaviour
{
    [EasyButtons.Button]
    static long Fibonacci(int index)
    {
        if (index < 0 || index > 5825) // no negative numbers please
            return -1; // indexes above 5825 are longer than long

        long x = 0;
        long y = 1;
        long z = 1;

        for (int i = 0; i < index; i++)
        {
            x = y; 
            y = z; 
            z = x + y;
        }
        return x;      
    }


    private uint CheckSum(byte[] inputData)
    {
        uint sum = 0;
        uint zeroOffset = 0x30;

        for (int i = 0; i < inputData.Length; i++)
        {
            int product = inputData[i] & 0x7F; // Take the low 7 bits from the input.
            product *= i + 1; // Multiply by the 1 based position.
            sum += (uint)product; // Add the product to the running sum.
        }

        byte[] result = new byte[8];
        for (int i = 0; i < 8; i++) 
        {
            uint current = (uint)(sum & 0x0f); // take the lowest 4 bits.
            current += zeroOffset; // Add '0'
            result[i] = (byte)current;
            sum = sum >> 4; // Right shift the bottom 4 bits off.
        }
        return sum;
    }



    [EasyButtons.Button]
    private void CoroutineTest()
    {
        Debug.Log("1");
        StartCoroutine(ACoroutine());
        StartCoroutine(ACoroutine());
        Debug.Log("2");
    }

    private IEnumerator ACoroutine()
    {
        Debug.Log("3");
        yield return new WaitForSeconds(1);
        Debug.Log("4");
        yield return new 
        WaitForSeconds(1); Debug.Log("5");
        yield break;
        Debug.Log("6");
    }


    #region Pointer
    public float vP;
    public float vL1 = 0;
    public float vL2 = 1;
    private float halfway;
    private float distance;

    [EasyButtons.Button]
    private int Pointer()
    {
        if (vP > vL2 || vP < vL1) return 0;

        halfway = ( Mathf.Sqrt(Mathf.Pow(vL1, 2) + Mathf.Pow(vL2, 2)) ) / 2;
        distance = Mathf.Sqrt(Mathf.Pow(vL1, 2) + Mathf.Pow(vP, 2));

        if (distance > halfway)
            return 1;
        else
            return -1;
    }
    #endregion


    #region Compare Dictionaries
    private bool AreDictionariesEqual(Dictionary<string, string> a, Dictionary<string, string> b) 
    {
        if (a.Count != b.Count) return false;

        foreach (KeyValuePair<string, string> kvpA in a)
            foreach (KeyValuePair<string, string> kvpB in b)
            {
                if (kvpA.Key != kvpB.Key) return false;
                else if (kvpA.Value != kvpB.Value) return false;
                else 
                    return true;
            }
        return false;
    }
    #endregion

    #region Match3
    public class Match3Grid
    {
        enum GemType
        {
            Empty,  // Not a gem - this grid square is empty
            Diamond, 
            Emerald, 
            Ruby, 
        } 

        // Assume this has been initialised already and has valid contents
        GemType[,] grid = new GemType[8, 10];

        List<Vector2Int> list = new List<Vector2Int>();

        int sequence = 3;

        // Returns null if there aren’t 3 or more gems in a row at this position
        public void GetConnectedGems(int x, int y)
        { 
            // Check Directions
            HorizontalCenter(x, y);
            VerticalCenter(x, y);
            Left(x,y);
            Right(x,y);
            Up(x,y);
            Down(x,y);
        }

        List<Vector2Int> HorizontalCenter(int x, int y)
        {
            x += 1;
            for (int i = 1; i < sequence; i++)
                if (grid[x - i, y] == grid[x, y])
                {
                    Vector2Int gemPos = new Vector2Int(x - i, y);
                    list.Add(gemPos);
                    if (list.Count >= sequence)
                        return list;
                    else
                        list.Clear();
                }
                else
                    return null;
            return null;
        }

        List<Vector2Int> VerticalCenter(int x, int y)
        {
            y += 1;
            for (int i = 1; i < sequence; i++)
                if (grid[x, y - i] == grid[x, y])
                {
                    Vector2Int gemPos = new Vector2Int(x, y - i);
                    list.Add(gemPos);
                    if (list.Count >= sequence)
                        return list;
                    else
                        list.Clear();
                }
                else
                    return null;
            return null;
        }

        List<Vector2Int> Left(int x, int y)
        {
            for (int i = 1; i < sequence; i++)
                if (grid[x - i, y] == grid[x, y])
                {
                    Vector2Int gemPos = new Vector2Int(x - i, y);
                    list.Add(gemPos);
                    if (list.Count >= sequence)
                        return list;
                    else
                        list.Clear();
                }
                else
                    return null;
            return null;
        }

        List<Vector2Int> Right(int x, int y)
        {
            for (int i = 1; i < sequence; i++)
                if (grid[x + i, y] == grid[x, y])
                {
                    Vector2Int gemPos = new Vector2Int(x + i, y);
                    list.Add(gemPos);
                    if (list.Count >= sequence)
                        return list;
                    else
                        list.Clear();
                }
                else
                    return null;
            return null;
        }

        List<Vector2Int> Up(int x, int y)
        {
            for (int i = 1; i < sequence; i++)
                if (grid[x, y + 1] == grid[x, y])
                {
                    Vector2Int gemPos = new Vector2Int(x, y + i);
                    list.Add(gemPos);
                    if (list.Count >= sequence)
                        return list;
                    else
                        list.Clear();
                }
                else
                    return null;
            return null;
        }

        List<Vector2Int> Down(int x, int y)
        {
            for (int i = 1; i < sequence; i++)
                if (grid[x, y - i] == grid[x, y])
                {
                    Vector2Int gemPos = new Vector2Int(x, y - i);
                    list.Add(gemPos);
                    if (list.Count >= sequence)
                        return list;
                    else
                        list.Clear();
                }
                else
                    return null;
            return null;
        }
    }
    #endregion

    #region Normalize Audio
    private void NormaliseAudio(short[] audio)
    {
        for (int i = 0; i < audio.Length; i++)
            if (audio[i] <= (Int16.MaxValue / 3))
            {
                audio[i] *= 3;
                if (audio[i] > Int16.MaxValue) audio[i] = Int16.MaxValue;
            }
    }
    #endregion

}
