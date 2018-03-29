using System;
using DOH2015.ComponentModel;
using Xamarin.Forms;
using DOH2015.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace DOH2015
{
	public class MyCompanyPageViewModel : ViewModelBase
	{
		INavigation Navigation {get; set;}
		public List<Todo> Todos { get { return get (() => this.Todos); } set { set (() => this.Todos, value); } }
		public ICommand InfoClicked { get { return get (() => this.InfoClicked); } set { set (() => this.InfoClicked, value); } }

		public MyCompanyPageViewModel (INavigation navigation)
		{
			Navigation = navigation;
			Todos = new List<Todo> () {
				new Todo { title = "Test taak 1", description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec vulputate diam. Curabitur vel elit in augue vehicula molestie quis elementum nulla. Nullam consectetur tellus tortor, nec consequat magna luctus quis. Donec ultricies commodo nunc in blandit. Ut eget erat nec risus semper auctor sed eu ante. Nam congue ante non velit pulvinar aliquet. Sed viverra venenatis auctor. Etiam tempus tortor id odio fringilla sodales. Quisque porta dictum libero ac tempor. Ut vitae posuere metus. Maecenas eget volutpat eros."},
				new Todo { title = "Test taak 2" },
			};
		}
	}
}

