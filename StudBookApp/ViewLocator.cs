using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using StudBookApp.ViewModels.Base;

namespace StudBookApp
{
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;
        public bool Match(object data)
        {
            return data is ViewModelBase;
        }

        public Control? Build(object? data)
        {
            var name = data.GetType().FullName.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type);
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + name };
            }
        }

        //public IControl Build(object data)
        //{
        //    var name = data.GetType().FullName.Replace("ViewModel", "View");
        //    var type = Type.GetType(name);

        //    if (type != null)
        //    {
        //        return (Control)Activator.CreateInstance(type);
        //    }
        //    else
        //    {
        //        return new TextBlock { Text = "Not Found: " + name };
        //    }
        //}
    }
}