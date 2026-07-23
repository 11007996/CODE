using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCode
{
    class users:INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string Name
        {
            get { return name; }
            set {
                name = value;
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
        }
    }
}
