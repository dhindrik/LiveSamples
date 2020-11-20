using System;
using System.Windows.Input;
using TinyMvvm;

namespace LiveSample.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }


        public ICommand Start => new TinyCommand(async() =>
        {
            await Navigation.NavigateToAsync($"{nameof(LiveViewModel)}?name={Name}");
        });
    }
}
