using Prism.Events;
using Slipways.Mobile.ViewModels;

namespace Slipways.Mobile.Events
{
    public class UpdateReadyEvent<T> : PubSubEvent<DataUpdateEventArgs<T>> { }
}
