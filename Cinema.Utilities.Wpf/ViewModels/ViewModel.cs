﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cinema.Utilities.Wpf.ViewModels
{
    public abstract class ViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void SetProperty<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if(!oldValue?.Equals(newValue)??newValue!=null)
            {
                oldValue = newValue;

                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}