using Assembly_BrowserLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Browser.ViewModel
{
    class DataViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
        public DataViewModel()
        {
            string path = @"C:\Users\Xiaomi\source\repos\Faker\Faker\bin\Debug\netcoreapp3.1\Faker.dll";
            _Types = new AssemblyBrowser().GetAssemblyInfo(path);
        }
        private List<TypeInfo> _Types;
        public List<TypeInfo> Types
        {
            get { return _Types; }
            set
            {
                _Types = value;
                OnPropertyChanged();
            }
        }
    }
}
