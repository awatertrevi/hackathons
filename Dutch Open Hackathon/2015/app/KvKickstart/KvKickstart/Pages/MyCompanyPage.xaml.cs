using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DOH2015.Models;

namespace DOH2015
{
	public partial class MyCompanyPage : ContentPage
	{
		MyCompanyPageViewModel ViewModel { get { return (MyCompanyPageViewModel)BindingContext; } }

		public MyCompanyPage ()
		{
			InitializeComponent ();
			BindingContext = new MyCompanyPageViewModel (Navigation);
			TodoList.ItemSelected += Tasks_ItemSelected;
			eigenTaak.Completed += EigenTaak_Completed;
		}

		void EigenTaak_Completed (object sender, EventArgs e)
		{
			var ent = sender as Entry;
			if(ent != null){
				ViewModel.Todos.Add(new Todo() {title = ent.Text});	
				ent.Text = string.Empty;
			}
		}

		void Tasks_ItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			var todo = e.SelectedItem as Todo;
			if (todo != null) 
			{
				// TODO: Handle some propertychanged for the text color conveter.
			}
		}

		public void InfoClicked(object sender, EventArgs args)
		{
			Button button = (Button)sender;

			DisplayAlert("Informatie", button.CommandParameter == null ? "Niet meer informatie beschikbaar" : button.CommandParameter.ToString(), "Sluit");		
		}
	}
}

