using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadonlyTest {

    class Program {

        static readonly int A1 = B1 + 1;

        static readonly int B1 = 1;

        const int A2 = B2 + 1;

        const int B2 = 1;

        static void Main(string[] args) {
            // readonly
            Console.WriteLine(A1);
            // const
            Console.WriteLine(A2);
            Console.Read();
        }
    }
}
