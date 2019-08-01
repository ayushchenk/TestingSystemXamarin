using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestingSystem.XamarinForms.Models
{
	public class RelayCommand : ICommand
	{
		readonly Action<object> _action;
		readonly Predicate<object> _pred;

		public RelayCommand(Action<object> act, Predicate<object> pred = null)
		{
			_action = act;
			_pred = pred;
		}

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
		{
			return _pred == null ? true : _pred(parameter);
		}

		public void Execute(object parameter)
		{
			_action(parameter);
		}
	}
}
