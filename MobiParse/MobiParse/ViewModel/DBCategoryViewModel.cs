using System;
using System.Collections.ObjectModel;
using MobiParse.Models;
using Xamarin.Forms;

namespace MobiParse.ViewModel
{
    public class DBCategoryViewModel : BaseViewModel
    {

        ObservableCollection<CategoryDataModels> _categoryList;
        public DBCategoryViewModel()
        {
            CategoryList = new ObservableCollection<CategoryDataModels>();
        }

        public ObservableCollection<CategoryDataModels> CategoryList
        {
            get
            {
                return _categoryList;
            }
            set

            {
                _categoryList = value;
                RaisePropertyChanged(nameof(CategoryList));
            }
        }

    }
}

