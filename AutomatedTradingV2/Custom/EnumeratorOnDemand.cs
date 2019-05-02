using AutomatedTradingV2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTradingV2.Custom
{
    public abstract class EnumeratorOnDemand<T_Output, T_Load> : IEnumerator<T_Output>
    {
        public IEnumerator<T_Load> LoadEnumerator { get; set; }

        public T_Load Current
        {
            get
            {
                return this.LoadEnumerator.Current;
            }             
        }

        public string PartialPath { set; get; }

        public LoadInterface<T_Output, T_Load> Loader { get; set; }

        public EnumeratorOnDemand(LoadInterface<T_Output, T_Load> loader, IEnumerator<T_Load> enumerator, string partialPath)
        {
            this.Loader = loader;
            this.LoadEnumerator = enumerator;
            this.PartialPath = partialPath;
        }

        public bool MoveNext()
        {
            return LoadEnumerator.MoveNext();
        }

        public void Reset()
        {
            LoadEnumerator.Reset();
        }

        public void Dispose()
        {
            LoadEnumerator.Dispose();
        }

        void IDisposable.Dispose()
        {
            LoadEnumerator.Dispose();
        }

        bool IEnumerator.MoveNext()
        {
            return LoadEnumerator.MoveNext();
        }

        void IEnumerator.Reset()
        {
            LoadEnumerator.Reset();
        }

        T_Output IEnumerator<T_Output>.Current => Loader.LoadOnDemand(Current);

        object IEnumerator.Current => Loader.LoadOnDemand(Current);
    }


}
