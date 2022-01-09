using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class Async : MonoBehaviour {
    void Start() {
        TaskRun();
        TaskFromResult();
    }

    // async 키워드는 해당 메소드가 await 키워드를 가지고 있음을 표시
    // await 키워드가 선언된 대상은 대상이 값을 할당받을 때까지 기다리지 않고 프로그램이 가동된다(다른 Task 진행). 그리고 대상에게 값이 부여되면 중단된 위치에서 다시 진행
    async void TaskRun() {
        var task = Task.Run(() => TaskRunMethod(3));
        int count = await task;
        Debug.Log("Count : " + task.Result); // (3초후) 출력: 3
    }

    private int TaskRunMethod(int limit) {
        int count = 0;
        for (int i = 0; i < limit; i++) {
            ++count;
            Thread.Sleep(1000);
        }

        return count;
    }

    async void TaskFromResult() {
        int sum = await Task.FromResult(Add(4, 5));
        Debug.Log(sum); // 출력: 9
    }

    private int Add(int a, int b) {
        return a + b;
    }
}