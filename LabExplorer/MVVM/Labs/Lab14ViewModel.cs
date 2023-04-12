using LabExplorer.Common;
using Labs.Lab13;
using Labs.Lab14;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace LabExplorer.MVVM.Labs
{
	public sealed class Lab14ViewModel : BaseViewModel
	{
		private IFigure selectedFigure;
		private string radius, height;

		private RelayCommand addNewConeCommand, deleteFigureCommand, sortFiguresCommand;

		public ObservableCollection<IFigure> Figures { get; set; }
		
		public IFigure SelectedFigure
		{
			get => selectedFigure;
			set
			{
				if (value is Cone cone)
					SelectCone(cone);
				
				SetField(ref selectedFigure, value);
			}
		}
		
		public string Radius
		{
			get => radius;
			set
			{
				SetField(ref radius, value);
				UpdateSelected();
			}
		}

		public string Height
		{
			get => height;
			set
			{
				SetField(ref height, value);
				UpdateSelected();
			}
		}

		public RelayCommand AddNewConeCommand
		{
			get
			{
				return addNewConeCommand ??= new RelayCommand(obj =>
				{
					Figures.Add(new Cone());
				});
			}
		}

		public RelayCommand DeleteFigureCommand
		{
			get
			{
				return deleteFigureCommand ??= new RelayCommand(obj =>
				{
					if (SelectedFigure != null)
						Figures.Remove(SelectedFigure);
				});
			}
		}

		public RelayCommand SortFiguresCommand
		{
			get
			{
				return sortFiguresCommand ??= new RelayCommand(obj =>
				{
					List<IFigure> sorted = new(Figures);
					sorted.Sort((a, b) => b.Volume.CompareTo(a.Volume));

					Figures.Clear();
					foreach (IFigure figure in sorted)
						Figures.Add(figure);
				});
			}
		}

		private void SelectCone(Cone cone)
		{
			radius = cone.Radius.ToString(CultureInfo.InvariantCulture);
			height = cone.Height.ToString(CultureInfo.InvariantCulture);
			OnPropertyChanged(nameof(Radius));
			OnPropertyChanged(nameof(Height));
		}

		private void UpdateSelected()
		{
			if (selectedFigure is null)
				return;

			if (selectedFigure is Cone cone)
			{
				if (!radius.Parse(out double radiusValue) || !height.Parse(out double heightValue))
					return;

				cone.Radius = radiusValue;
				cone.Height = heightValue;
				UpdateListView();
			}
		}

		private void UpdateListView()
		{
			IFigure selected = selectedFigure;

			List<IFigure> temp = new();
			foreach (IFigure figure in Figures)
				temp.Add(figure);

			Figures.Clear();
			foreach (IFigure figure in temp)
				Figures.Add(figure);

			SelectedFigure = selected;
		}

		public Lab14ViewModel()
		{
			Figures = new();
		}
	}
}
