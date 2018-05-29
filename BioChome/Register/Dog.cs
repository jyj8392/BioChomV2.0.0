using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Register
{
    public class Dog
    {
        public uint DogBytes, DogAddr;
        public byte[] DogData;
        public uint Retcode;

        [DllImport("Win32dll.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint DogRead(uint idogBytes, uint idogAddr, byte* pdogData);
        [DllImport("Win32dll.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint DogWrite(uint idogBytes, uint idogAddr, byte* pdogData);

        public unsafe Dog(ushort num)
        {
            DogBytes = num;
            DogData = new byte[DogBytes];
        }

        public unsafe void ReadDog()
        {
            fixed (byte* pDogData = &DogData[0])
            {
                Retcode = DogRead(DogBytes, DogAddr, pDogData);
            }
        }
        public unsafe void WriteDog()
        {
            fixed (byte* pDogData = &DogData[0])
            {
                Retcode = DogWrite(DogBytes, DogAddr, pDogData);
            }
        }
    }
}
