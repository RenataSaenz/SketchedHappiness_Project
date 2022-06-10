public interface IObservable 
{
    void Subscribe(IObserver obs);   
    void Unsubscribe(IObserver obs);
    void NotifyToObservers(float value, float maxValue);
}