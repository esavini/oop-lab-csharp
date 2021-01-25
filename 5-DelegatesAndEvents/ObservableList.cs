using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DelegatesAndEvents
{
    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private readonly IList<TItem> _list;

        public ObservableList()
        {
            _list = new List<TItem>();
        }

        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => _list.Count;

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly => _list.IsReadOnly;

        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get
            {
                if (index >= _list.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                return _list[index];
            }
            set
            {
                if (index >= _list.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                TItem oldValue = _list[index];

                _list[index] = value;

                ElementChanged?.Invoke(this, value, oldValue, index);
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator() => _list.GetEnumerator();


        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            _list.Add(item);
            ElementInserted?.Invoke(this, item, this.Count - 1);
        }

        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear()
        {
            for (int i = _list.Count - 1; i >= 0; i--)
            {
                var oldValue = _list[i];
                _list.RemoveAt(i);
                ElementRemoved?.Invoke(this, oldValue, i);
            }
        }

        /// <inheritdoc cref="ICollection{T}.Contains" />
        public bool Contains(TItem item) => _list.Contains(item);

        /// <inheritdoc cref="ICollection{T}.CopyTo" />
        public void CopyTo(TItem[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].Equals(item))
                {
                    var oldValue = _list[i];

                    _list.RemoveAt(i);

                    ElementRemoved?.Invoke(this, oldValue, i);
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc cref="IList{T}.IndexOf" />
        public int IndexOf(TItem item) => _list.IndexOf(item);


        /// <inheritdoc cref="IList{T}.Insert" />
        public void Insert(int index, TItem item)
        {
            if (index >= _list.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var oldValue = _list[index];

            _list.Insert(index, item);

            ElementChanged?.Invoke(this, item, oldValue, index);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            if (index >= _list.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var oldValue = _list[index];
            _list.RemoveAt(index);

            ElementRemoved?.Invoke(this, oldValue, index);
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            if (obj is IObservableList<TItem> list)
            {
                return Equals(list);
            }

            return false;
        }

        public bool Equals(IObservableList<TItem> list)
        {
            if (list is null)
            {
                return false;
            }

            if (ReferenceEquals(this, list))
            {
                return true;
            }

            if (Count != list.Count)
            {
                return false;
            }

            if (GetHashCode() != list.GetHashCode())
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode() => _list.GetHashCode();

        /// <inheritdoc cref="object.ToString" />
        public override string ToString() => _list.ToString();
    }
}
