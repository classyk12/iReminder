using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iReminder.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyReminderPage 
	{
		public MyReminderPage ()
		{
			InitializeComponent ();
		}

        private async void NewReminderClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new NewReminderPage(), true);
        }
    }
}