using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memento
{
    class Originator
    {
        private string state;
        public string State
        {
            get { return state;}
            set { state = value; }
        }
        public Memento CreateMomento()
        {
            return (new Memento(state));
        }
        public void SetMemento(Memento memento)
        {
            state = memento.State;
        }
        public void Show()
        {
            Console.WriteLine("State=" + state);
        }
    }
}
