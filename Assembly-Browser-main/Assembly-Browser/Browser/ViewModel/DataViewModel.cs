using Assembly_BrowserLib;
using Browser.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Browser.ViewModel
{
    class DataViewModel : ViewModelBase
    {
        private List<Namesp> _Types;
        private OpenFile _OpenCommand;
        private DataModel _AsmData = new DataModel();
        private string _Path;
        public List<Namesp> Types
        {
            get { return _Types; }
            set
            {
                _Types = value;
                OnPropertyChanged();
            }
        }
        public OpenFile OpenCommand
        {
            
            get 
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = ".Net assembly files (*.exe, *.dll) |*.exe;*.dll";
                return _OpenCommand ??
                  (_OpenCommand = new OpenFile(obj =>
                  {
                      openFileDialog.ShowDialog();
                      Path = openFileDialog.FileName;

                  }));
            }
        }
        public string Path
        {
            get { return _Path; }
            set
            {
                _Path = value; 
                Types = GetDataForTreeView(_AsmData.GetAsmData(_Path));
                OnPropertyChanged();
            }
        }

        public List<Namesp> GetDataForTreeView(List<NamespaceInfo> namespaces)
        {
            var namesps = new List<Namesp>();
            foreach(var item in namespaces)
            {
                var nsp = new Namesp();
                nsp.Name = item.Name;
                foreach (var type in item.Classes)
                {
                    var typeInfoFlat = new TypeInfoFlat();
                    typeInfoFlat.Name = type.Name;

                    foreach (var method in type.Methods)
                        typeInfoFlat.MFPC.Add(method.Name);

                    foreach (var fields in type.Fields)
                        typeInfoFlat.MFPC.Add(fields.Name);

                    foreach (var property in type.Properties)
                        typeInfoFlat.MFPC.Add(property.Name);

                    foreach (var constructor in type.Constructors)
                        typeInfoFlat.MFPC.Add(constructor.Name);

                    nsp.Classes.Add(typeInfoFlat);
                }
                namesps.Add(nsp);
            }
            return namesps;
        }

    }
}
