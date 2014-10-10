using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace LOGI.Framework.Toolkit.Foundation.Repository
{
    ///<summary>
    /// EntityBase
    ///</summary>
    [DataContract]
    public abstract class EntityBase : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, EmptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(string propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // An System.Collections.ObjectModel.ObservableCollection that raises
        // individual item removal notifications on clear and prevents adding duplicates.
        public class FixupCollection<T> : ObservableCollection<T>
        {
            protected override void ClearItems()
            {
                new List<T>(this).ForEach(t => Remove(t));
            }

            protected override void InsertItem(int index, T item)
            {
                if (!this.Contains(item))
                {
                    base.InsertItem(index, item);
                }
            }
        }

    }
}
