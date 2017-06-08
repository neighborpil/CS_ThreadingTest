using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/*]
[Background 쓰레드 vs Foreground 쓰레드 ]

Thread 클래스 객체를 생성한 후 Start()를 실행하기 전에 IsBackground 속성을 true/false로 지정할 수 있는데, 
만약 true로 지정하면 이 쓰레드는 백그라운드 쓰레드가 된다. 
디폴트 값은 false 즉 Foreground 쓰레드이다. 
백그라운드와 Foreground 쓰레드의 기본적인 차이점은 
Foreground 쓰레드는 메인 쓰레드가 종료되더라도 Foreground 쓰레드가 살아 있는 한, 프로세스가 종료되지 않고 계속 실행되고, 
백그라운드 쓰레드는 메인 쓰레드가 종료되면 바로 프로세스를 종료한다는 점이다. 
*/
namespace _2_Thread2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Foreground 쓰레드
            Thread t1 = new Thread(new ThreadStart(Run));
            t1.Start();

            Thread t2 = new Thread(new ThreadStart(Run));
            t2.IsBackground = true;
            t2.Start();
#if DEBUG
            Console.ReadKey();
#endif
        }

        private static void Run()
        {
            Console.WriteLine("Run");
        }
    }
}
