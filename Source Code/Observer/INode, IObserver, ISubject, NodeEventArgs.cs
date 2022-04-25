using System;

/*
 Observer 인터페이스와 Subject 인터페이스가 INode를 구현하는 이유는
 Observer를 구현하는 객체도 Subject를 구현하는 객체의 용도로 사용할수 있게 하기 위함임.
 즉 옵저버 인터페이스를 구현하는 객체도 하위 노드를 가질수있음.
 따라서 트리 구조형으로 이벤트가 아래로 타고 내려가 처리되는 형식임 (터널링 참고)
*/

public interface INode
{

}

public interface IObserver : INode
{

}

public interface ISubject : INode
{
    event EventHandler eventArray;
    void NotifyObservers(EventArgs args);
}

public class NodeEventArgs : EventArgs
{
    public string eventName;
    public object[] data;
    public object result;

    public NodeEventArgs(string eventName, params object[] data)
    {
        this.eventName = eventName;
        this.data = data;
    }
}