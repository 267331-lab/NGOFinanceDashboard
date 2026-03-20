public interface IObserver
{
 void Update(int data);
}

public interface IObservable
{
void AddObserver(IObserver observer, List<IObserver> _observers);
void RemoveObserver(IObserver observer,List<IObserver> _observers);
void NotifyObservers(List<IObserver> _observers);

}
