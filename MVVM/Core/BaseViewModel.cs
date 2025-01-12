using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KCK_Project_WPF.MVVM.Core
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool[] menuAppear = { false, true, false, false, false, false, false, false, false, false };

        public bool[] MenuAppear
        {
            get { return menuAppear; }
            set { menuAppear = value; OnPropertyChanged(); }
        }

        protected void DisplayMenuNumber(int poz = 0)
        {
            if (poz >= MenuAppear.Length) return;
            var buf = Enumerable.Repeat(false, MenuAppear.Length).ToArray();
            buf[poz] = true;
            MenuAppear = buf;
        }
    }
}
