using HICS_Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HICS_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstPage : ContentPage
    {
        FirstPageViewModel viewModel;
        public FirstPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new FirstPageViewModel();
        }
    }
}