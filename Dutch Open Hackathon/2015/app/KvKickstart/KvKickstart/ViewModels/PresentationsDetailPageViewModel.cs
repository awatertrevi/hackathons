using System;
using Xamarin.Forms;
using DOH2015.Models;
using System.Collections.Generic;
using DOH2015.ComponentModel;
using System.Windows.Input;
using DOH2015.Framework;
using System.Threading.Tasks;

namespace DOH2015
{
	public class PresentationsDetailPageViewModel : ViewModelBase
	{
		INavigation Navigation { get ; set;}

		public Presentation SelectedPresentation { get { return get (() => this.SelectedPresentation); } set { set (() => this.SelectedPresentation, value); } }
		public Presenter Presenter { get { return get (() => this.Presenter); } set { set (() => this.Presenter, value); } }
		public List<Media> MediaList { get { return get (() => this.MediaList); } set { set (() => this.MediaList, value); } }

		public PresentationsDetailPageViewModel (INavigation navigation, Presentation presentation)
		{
			Navigation = navigation;
			SelectedPresentation = presentation;
			MediaList = new List<Media> () {
				new Media { name= "VisiteKaartje.ppt", file = "http://lorem.ipsum/VisiteKaartje.ppt", icon = "ic_ppt_50dp.png" },
				new Media { name= "Slides.pdf", file = "http://lorem.ipsum/Slides.pdf", icon = "ic_pdf_50dp.png"},
			};
			InitData ();
		}

		async void InitData()
		{
			int id;
			if(int.TryParse(SelectedPresentation.presenterId, out id)){
				Presenter = await KamerVanKoophandel.Presenter (id);
				if (Presenter != null && Presenter.avatar == null) {
					Presenter.avatar = "Person.jpg";
				}
			}
		}
	}
}

