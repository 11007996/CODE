using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfToolkit.VM
{
    class bindViewModel:ObservableObject
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value;
                OnPropertyChanged();
            }
        }


        // 在ViewModel中定义命令
        public RelayCommand showTextVoid { get; set; }
        public RelayCommand<string> showText { get; set; }

        public bindViewModel()
        {
            showText = new RelayCommand<string>(setText);
            showTextVoid = new RelayCommand(setAge);
        }

        private void setText(string str)
        {
            Name = Name+" "+ str;
        }

        private void setAge()
        {
            Age++;
        }



        private int age=1;
        public int Age
        {
            get { return age; }
            set
            {
                if (age == value) return;

                age = value;

                OnPropertyChanged();
            }
        }
    }
}
