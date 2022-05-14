using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeTest : MonoBehaviour
{
    [EasyButtons.Button]
    private long Fibonacci(int index)
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

    [EasyButtons.Button]
    private byte[] Checksum(byte[] buffer)
    {
        int sum = 0;
        foreach (var b in buffer)
            sum += b;
        sum %= 0x100;
        byte[] ch = new byte[2];
        ch[0] = (byte)((sum >> 4) + 0x30);
        ch[1] = (byte)((sum & 0xF) + 0x30);
        Debug.Log(ch);  

        return ch;
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



    public float vP;
    public float vL1 = 0;
    public float vL2 = 1;
    public float halfway;
    public float distance;

    [EasyButtons.Button]
    private int Pointer()
    {      
        halfway = ( Mathf.Sqrt(Mathf.Pow(vL1, 2) + Mathf.Pow(vL2, 2)) ) / 2;
        distance = Mathf.Sqrt(Mathf.Pow(vL1, 2) + Mathf.Pow(vP, 2));

        if (distance > halfway)
        {
            Debug.Log("1");
            return 1;
        }
        else
        {
            Debug.Log("-1");
            return -1;
        }
    }


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


    /*
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

        int w;
        int h;
        int sequence = 2;

        // Returns null if there aren’t 3 or more gems in a row at this position
        public List<Vector2Int> GetConnectedGems(int x, int y)
        { 
            int w = grid.GetLength(1);
            int h = grid.GetLength(0);


        }

        List<Vector2Int> HorizontalCenter(int x, int y)
        {
            x += 1;
            for (int i=0; i < sequence; i++)
            {
                if (grid[x - 1, y] == grid[x, y])
                {
                    list.Add(grid[i]);
                    if (list.Count >= sequence)
                    {
                        return list;
                    }
                    else
                        list.Clear();
                }
                else
                    return null;
            }
            return null;
        }

        List<Vector2Int> Left(int x, int y)
        {
            for (int i = 0; i < sequence; i++)
            {
                if (grid[x - 1, y] == grid[x, y])
                {
                    list.Add(grid[i,i]);
                    if (list.Count >= sequence)
                    {
                        return list;
                    }
                    else
                        list.Clear();
                }
                else
                    return null;
            }
            return null;
        }

        List<Vector2Int> Right(int x, int y)
        {
            for (int i = 0; i < sequence; i++)
            {
                if (grid[x + 1, y] == grid[x, y])
                {
                    list.Add(i);
                    if (list.Count >= sequence)
                    {
                        return list;
                    }
                    else
                        list.Clear();
                }
                else
                    return null;
            }
            return null;
        }

        List<Vector2Int> VerticalCenter(int x, int y)
        {
            y += 1;
            for (int i = 0; i < sequence; i++)
            {
                if (grid[x, y - 1] == grid[x, y])
                {
                    list.Add(i);
                    if (list.Count >= sequence)
                    {
                        return list;
                    }
                    else
                        list.Clear();
                }
                else
                    return null;
            }
            return null;
        }

        List<Vector2Int> Up(int x, int y)
        {
            for (int i = 0; i < sequence; i++)
            {
                if (grid[x, y + 1] == grid[x, y])
                {
                    list.Add(i);
                    if (list.Count >= sequence)
                    {
                        return list;
                    }
                    else
                        list.Clear();
                }
                else
                    return null;
            }
            return null;
        }
    }

        List<Vector2Int> Down(int x, int y)
        {
            for (int i = 0; i < sequence; i++)
            {
                if (grid[x, y - 1] == grid[x, y])
                {
                    list.Add(i);
                    if (list.Count >= sequence)
                    {
                        return list;
                    }
                    else
                        list.Clear();
                }
                else
                    return null;
            }
            return null;
        }
    } 
    */























    /*
    short maxVolume = 32767;
    short minVolume = -32768;


    private void NormaliseAudio(short[] audio)
    {
        for (int i=0; i < audio.Length; i++)
        {
            if (audio[i] <= (maxVolume / 3))
            {
                audio[i] + audio[i];
            }
        }
    }
    */
}
