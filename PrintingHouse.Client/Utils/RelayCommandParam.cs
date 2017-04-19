namespace PrintingHouse.Client
{
    using System;
    using System.Windows.Input;

    public class RelayCommandParam<T> : ICommand
    {
        private readonly Action<T> _execute;
        public Predicate<T> CanExecuteFunc { get; private set; }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public RelayCommandParam(Action<T> execute) : this(execute, p => true)
        { }

        public RelayCommandParam(Action<T> execute, Predicate<T> canExecuteFunc)
        {
            _execute = execute;
            CanExecuteFunc = canExecuteFunc;
        }

        public bool CanExecute(object parameter)
        {
            var canExecute = CanExecuteFunc((T)parameter);
            return canExecute;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
