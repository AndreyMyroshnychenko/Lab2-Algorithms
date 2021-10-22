using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ПА_Лаб._2
{
    class DataBase
    {
        List<List<int[]>> ind_area = new List<List<int[]>>();
        string index_area;
        int N;
        int empty;

        public DataBase(string name)
        {
            index_area = name;
            string path = $"..\\netcoreapp3.1\\{index_area}";
            StreamReader sr;
            try
            {
                sr = new StreamReader(path);
            }
            catch
            {
                Fill();
                sr = new StreamReader(path);
            }
            string line;
            N = 10;
            empty = (int)(0.9 * N);
            int current = 0;
            ind_area.Add(new List<int[]>());
            while ((line = sr.ReadLine()) != null)
            {
                if (line != " ")
                {
                    string[] k = new string[2];
                    k = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    int[] k_l = new int[2] { Convert.ToInt32(k[0]), Convert.ToInt32(k[1]) };
                    ind_area[current].Add(k_l);
                }
                else
                {
                    if (ind_area[current].Count != 0)
                    {
                        ind_area.Add(new List<int[]>());
                        current++;
                    }
                }
            }
            sr.Close();
        }

        public void Fill()
        {
            StreamWriter sw_zap = new StreamWriter($"..\\netcoreapp3.1\\main_{index_area}", false, Encoding.Default);
            int z = 0;
            List<int> loc = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
            List<Element> res = new List<Element>();
            for (int i = 1; i <= 4; i++)
            {
                Random rand = new Random();
                int n = rand.Next(5, 8);
                for (int j = 0; j < n; j++)
                {
                    int cu = rand.Next(0, loc.Count);
                    res.Add(new Element(loc[cu], z));
                    loc.RemoveAt(cu);
                    sw_zap.WriteLine($"{z} {rand.Next(0, 10000)} 1");
                    z++;
                }
            }
            sw_zap.Close();
            int cue = 0;
            while (cue != res.Count)
            {
                int i_min = -1;
                int min = 10000;
                for (int i = cue; i < res.Count; i++)
                {
                    if (res[i].key < min)
                    {
                        i_min = i;
                        min = res[i].key;
                    }
                }
                Element t = res[cue];
                res[cue] = res[i_min];
                res[i_min] = t;
                cue++;
            }
            StreamWriter sw = new StreamWriter(index_area, false, Encoding.Default);
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (res.Count != 0)
                        if (res[0].key <= i * 10)
                        {
                            sw.WriteLine($"{res[0].key} {res[0].value}");
                            res.RemoveAt(0);
                        }
                        else
                        {
                            sw.WriteLine(" ");
                        }
                    else
                    {
                        sw.WriteLine(" ");
                    }
                }
            }
            sw.Close();
        }


        public int Find(int key, bool delete)
        {
            int block = (key - 1) / 10;
            List<int[]> loc = ind_area[block];
            int i = loc.Count / 2 + 1;
            int h = loc.Count / 2;
            while (true)
            {
                if (i < 0 || i >= loc.Count)
                    return -1;
                h = h / 2;
                if (loc[i][0] == key)
                {
                    if (delete)
                        Remove(i + block * 10, loc[i][1]);
                    return loc[i][1];
                }
                else if (loc[i][0] > key)
                {
                    i = i - (h + 1);
                }
                else
                {
                    i = i + (h + 1);
                }
                if (h == 0)
                {
                    loc = ind_area[ind_area.Count - 1];
                    if (loc.Count == 0)
                        return -1;
                    i = loc.Count / 2 + 1;
                    h = loc.Count / 2;
                    while (true)
                    {
                        if (loc[i][0] == key)
                        {
                            if (delete)
                                Remove(i + block * 10, loc[i][1]);
                            return loc[i][1];
                        }
                        else if (loc[i][0] > key)
                            i = i - (h + 1);
                        else
                            i = i + (h + 1);
                        if (h == 0)
                            return -1;
                    }
                }
            }
        }

        public string Alert(int key)
        {
            if (key == -1)
            {
                return ("Not Found");
            }
            StreamReader sr = new StreamReader($"..\\netcoreapp3.1\\main_{index_area}");
            string line;
            List<string> main = new List<string>();
            while ((line = sr.ReadLine()) != null)
            {
                main.Add(line);
            }
            sr.Close();
            string[] arr = main[key].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            return arr[1];
        }

        public void Add(int value)
        {
            StreamReader sr = new StreamReader($"..\\netcoreapp3.1\\main_{index_area}");
            string line;
            List<string> main = new List<string>();
            while ((line = sr.ReadLine()) != null)
            {
                main.Add(line);
            }
            main.Add($"{main.Count} {value} 1");
            sr.Close();
            StreamWriter sw_zap = new StreamWriter($"..\\netcoreapp3.1\\main_{index_area}", false, Encoding.Default);
            while (main.Count != 0)
            {
                sw_zap.WriteLine(main[0]);
                main.RemoveAt(0);
            }
            sw_zap.Close();
        }

        public void Edit(int key_i, int value)
        {
            StreamReader sr = new StreamReader($"..\\netcoreapp3.1\\main_{index_area}");
            string line;
            List<string> main = new List<string>();
            while ((line = sr.ReadLine()) != null)
            {
                main.Add(line);
            }
            key_i = Find(key_i, false);
            if (key_i == -1)
            {
                Console.WriteLine("No");
            }
            main[key_i] = $"{key_i} {value} 1";
            sr.Close();
            StreamWriter sw_zap = new StreamWriter($"..\\netcoreapp3.1\\main_{index_area}", false, Encoding.Default);
            while (main.Count != 0)
            {
                sw_zap.WriteLine(main[0]);
                main.RemoveAt(0);
            }
            sw_zap.Close();
        }

        public void Remove(int key_i, int key_v)
        {
            StreamReader sr = new StreamReader($"..\\netcoreapp3.1\\main_{index_area}");
            string line;
            List<string> main = new List<string>();
            while ((line = sr.ReadLine()) != null)
            {
                main.Add(line);
            }
            sr.Close();
            main[key_v] = main[key_v].Substring(0, main[key_v].Length - 1) + "0";
            StreamReader sr_zp = new StreamReader($"..\\netcoreapp3.1\\{index_area}");
            string ind_text = "";
            int n = 0;
            while ((line = sr_zp.ReadLine()) != null)
            {
                if (key_i != n)
                    ind_text += line + "\r\n";
                if (n == ((key_i - 1) / 10 + 1) * 10)
                    ind_text += " " + "\r\n";
                n++;
            }
            sr_zp.Close();
            StreamWriter sw = new StreamWriter(index_area, false, Encoding.Default);
            sw.Write(ind_text);
            sw.Close();
            StreamWriter sw_zap = new StreamWriter($"..\\netcoreapp3.1\\main_{index_area}", false, Encoding.Default);
            while (main.Count != 0)
            {
                sw_zap.WriteLine(main[0]);
                main.RemoveAt(0);
            }
            sw_zap.Close();
        }
    }
}
