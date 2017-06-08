using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/*
동일 클래스가 아닌 다른 클래스의 메서드를 쓰레드에 호출하기 위해서는 
해당 클래스의 객체를 생성한 후 (혹은 외부로부터 전달 받은 후) 
그 객체의 메서드를 델리게이트로 Thread에 전달하면 된다.
*/
namespace Thread1_3
{
    class Helper
    {
        public void Run()
        {
            Console.WriteLine("Helper.Run");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Helper클래스의 Run메서드 호출
            Helper helper = new Helper();
            Thread t = new Thread(helper.Run);
            t.Start();
        }
    }
}
