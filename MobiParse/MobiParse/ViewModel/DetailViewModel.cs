using MobiParse.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiParse.ViewModel
{
    public class DetailViewModel : BaseViewModel
    {
        private List<string> _names;
        
        public List<string> Names
        {
            get
            {
                return _names;
            }
            set
            {
                _names = value;
                RaisePropertyChanged(nameof(Names));
            }
        }

    }
}
