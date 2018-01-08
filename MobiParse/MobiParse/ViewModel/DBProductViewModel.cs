using System;
using System.Collections.ObjectModel;
using MobiParse.Models;

namespace MobiParse.ViewModel
{
    public class DBProductViewModel : BaseViewModel
    {
        ObservableCollection<ProductDataModels> _categoryList;

        public DBProductViewModel()
        {
            ProductList = new ObservableCollection<ProductDataModels>();
        }

        public ObservableCollection<ProductDataModels> ProductList
        {
            get
            {
                return _categoryList;
            }
            set

            {
                _categoryList = value;
                RaisePropertyChanged(nameof(ProductList));
            }
        }

    }
}
