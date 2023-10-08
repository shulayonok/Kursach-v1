using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_v1
{
    internal class RC4
    {
        byte[] S = new byte[256];
        int x = 0;
        int y = 0;

        public RC4(byte[] key)
        {
            KSA(key);
        }

        public static void Change(byte[] array, int index1, int index2)
        {
            byte temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
        // Key - Scheduling Algorithm (алгоритм ключевого расписания)
        private void KSA(byte[] key)
        {
            int keyLength = key.Length;

            for (int i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % keyLength]) % 256;
                Change(S, i, j);
            }
        }
        //Pseudo - Random - Generation - Algorithm (генератор псевдослучайной последовательности)
        private byte PRGA()
        {
            x = (x + 1) % 256;
            y = (y + S[x]) % 256;

            Change(S, x, y);

            return S[(S[x] + S[y]) % 256];
        }

        public byte[] Encode(byte[] dataB, int size)
        {
            byte[] data = dataB.Take(size).ToArray();

            byte[] cipher = new byte[data.Length];

            for (int m = 0; m < data.Length; m++)
            {
                cipher[m] = (byte)(data[m] ^ PRGA());
            }

            return cipher;
        }

        public byte[] Decode(byte[] dataB, int size)
        {
            return Encode(dataB, size);
        }
    }
}
