using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/*
[C# ThreadPool 사용 ]
.NET의 Thread클래스를 이용하여 쓰레드를 하나씩 만들어 사용하는 것이 아니라, 
이미 존재하는 쓰레드풀에서 사용가능한 작업 쓰레드를 할당 받아 사용하는 방식이 있는데, 
이는 다수의 쓰레드를 계속 만들어 사용하는 것보다 효율적이다. 
이렇게 시스템에 존재하는 쓰레드풀에 있는 쓰레드를 사용하기 위해서는 
(1) ThreadPool 클래스, 
(2) 비동기 델리게이트(Asynchronous delegate), 
(3) .NET 4 Task 클래스, 
(4) .NET 4 Task<T> 클래스, 
(5) BackgroundWorker 클래스 등을 사용할 수 있다. 
이 중 ThreadPool 클래스의 경우, ThreadPool.QueueUserWorkItem() 를 사용하여 
실행하고자 하는 메서드 델리게이트를 지정하면 시스템풀에서 쓰레드를 할당하여 실행하게 된다. 
이 방식은 실행되는 메서드로부터 리턴 값을 돌려받을 필요가 없는 곳에 주로 사용된다. 
리턴값이 필요한 경우는 비동기 델리게이트(Asynchronous delegate)를 사용한다.

[ThreadPool에서의 쓰레드 생성 과정]
.NET의 쓰레드풀은 기본적으로(by default) CPU당 최소 1개의 쓰레드에서 최대 250개의 쓰레드를 생성하여 운영하게 된다 
(주: 최대 250개의 쓰레드수는 CLR 2.0 SP1 부터 적용된다. 그 이전 버젼은 최대 25개이다). 
예를 들어, 만약 해당 컴퓨터가 4개의 CPU를 가지고 있으면, 
ThreadPool은 최소 4개의 쓰레드 그리고 최대 1024개의 쓰레드를 가질 수 있게된다. 
ThreadPool에서의 쓰레드 생성 과정을 보면, 
처음 최소 CPU당 1개의 쓰레드에서 시작해서 계속 쓰레드 생성 요청을 받아 쓰레드풀에 쓰레드를 생성하게 되는데, 
최대 쓰레드풀 쓰레드 수까지만 쓰레드를 생성할 수 있다. 
만약 중간에 사용되는 쓰레드가 작업을 끝내고 쓰레드풀로 돌아오면, 해당 쓰레드는 재사용된다. 
또한 최대 쓰레드수 만큼 쓰레드가 생생된 후, 계속 쓰레드 생성 요청이 있으면, 해당 요청 쓰레드는 생성되지 않고 대기하게 된다. 
그리고, 쓰레드 생성시 요청되는 쓰레드수가 해당 컴퓨터의 CPU수보다 많아지면, 
CLR 시스템은 쓰레드를 즉시 생성하지 않고 초당 2개의 쓰레드를 새로 생성되도록 지연하게 된다 (Thread Throttling). 
예를 들어, 4개의 CPU를 가진 컴퓨터에서 50개의 쓰레드를 생성한다고 하면, 
처음 4개의 쓰레드는 즉시 생성이 되고, 나머지 46개는 46/2 즉 약 23초의 생성시간이 소요된다 
(물론 이는 기존의 쓰레드들이 23초보다 오래 계속 실행한다는 가정하에... 그렇지 않다면 완료된 쓰레드가 재활용 될 것이다). 
여기서 23초는 이론적 최소 시간이고 실제는 타 프로세스나 서비스 부하 등의 영향을 받아 더 느릴 수 있다.

ThreadPool 클래스는 디폴트 최대, 최소 쓰레드 수를 재설정하도록 
ThreadPool.SetMaxThreads(), ThreadPool.SetMinThreads() 함수를 제공하고 있는데, 
개발자는 필요에 따라 최대, 최소 쓰레드를 조정하게 된다. 
만약 어떤 프로그램이 50개의 쓰레드가 항상 사용될 것이라고 예측 된다면, 
ThreadPool.SetMinThreads(50, 50) 을 사용하여 미리 쓰레드풀 쓰레드들을 생성해서 Thread Throttling 지연을 피할 수 있다. 
*/
namespace _3_ThreadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            // 쓰레드풀에 있는 쓰레드를 이용하여
            // Calc() 메서드 실행. 
            // 리턴값 없을 경우 사용.
            ThreadPool.QueueUserWorkItem(Calc); // radius=null
            ThreadPool.QueueUserWorkItem(Calc, 10.0);
            ThreadPool.QueueUserWorkItem(Calc, 20.0);

#if DEBUG
            Console.ReadKey();
#endif
        }

        private static void Calc(object radius)
        {
            if (radius == null)
                return;

            double r = (double)radius;
            double area = r * r * Math.PI;
            Console.WriteLine($"r={r}, area={area}");
        }
    }
}
