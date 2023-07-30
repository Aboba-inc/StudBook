using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using ReactiveUI;
using StudBookApp.ViewModels.Base;
using System;
using System.Reactive;
using StudBookApp.Themes.StyleHelpers;
using StudBookApp.Model;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Input;

namespace StudBookApp.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region Properties

    public string Grade { get; set; } = "0";

    public Subject[] Subjects { get; set; }

    public BindingList<MyString> SubjectNames { get; set; }

    public BindingList<MyString> SubjectCredits { get; set; }

    public BindingList<MyString> SubjectGrades { get; set; }


    #endregion

    #region Commands

    #region Close
    public ReactiveCommand<Unit, Unit> CloseApplicationCommand { get; }
    void CloseApplication()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
        {
            lifetime.Shutdown();
        }
    }
    #endregion

    #region Change theme
    public ReactiveCommand<Unit, Unit> ChangeThemeCommand { get; }
    void ChangeTheme(StyleManager? styles)
    {
        styles?.UseTheme(styles.CurrentTheme switch
        {
            Theme.Default => Theme.New,
            Theme.New => Theme.Default,
            _ => throw new ArgumentOutOfRangeException(nameof(styles.CurrentTheme))
        });
    }
    #endregion

    #region Grade key down event handler
    public ReactiveCommand<KeyEventArgs, Unit> GradeKeyDownCommand { get; }
    Unit GradeKeyDown(KeyEventArgs e)
    {
        if (!char.IsNumber((char)e.Key))
            e.Handled = true;

        return new Unit();
    }
    #endregion

    #endregion

    public MainViewModel(StyleManager? styles)
    {
        Subjects = new Subject[8];
        for (int i = 0; i < Subjects.Length; i++)
            Subjects[i] = new Subject();

        SubjectNames = new BindingList<MyString>();
        for (int i = 0; i < Subjects.Length; i++)
            SubjectNames.Add(new MyString() { Value = "" });

        SubjectCredits = new BindingList<MyString>();
        for (int i = 0; i < Subjects.Length; i++)
            SubjectCredits.Add(new MyString() { Value = "" });

        SubjectGrades = new BindingList<MyString>();
        for (int i = 0; i < Subjects.Length; i++)
            SubjectGrades.Add(new MyString() { Value = "" });

        SubjectNames.ListChanged += SubjectNames_ListChanged;
        SubjectCredits.ListChanged += SubjectCredits_ListChanged;
        SubjectGrades.ListChanged += SubjectGrades_ListChanged;

        CloseApplicationCommand = ReactiveCommand.Create(CloseApplication);
        ChangeThemeCommand = ReactiveCommand.Create(() => ChangeTheme(styles));
        GradeKeyDownCommand = ReactiveCommand.Create<KeyEventArgs, Unit>(GradeKeyDown);
    }

    public void GradeKeyDown(object sender, KeyEventArgs e)
    {
        if (!char.IsNumber((char)e.Key))
            e.Handled = true;

    }

    private void SubjectCredits_ListChanged(object? sender, ListChangedEventArgs e)
    {
        for (int i = 0; i < Subjects.Length; i++)
        {
            Subjects[i].Credits = double.Parse(string.IsNullOrEmpty(SubjectCredits[i].Value) ? "0" : SubjectCredits[i].Value.Replace('.', ','));
        }
        CalculateGrade();
    }

    private void SubjectGrades_ListChanged(object? sender, ListChangedEventArgs e)
    {
        for (int i = 0; i < Subjects.Length; i++)
        {
            if (int.TryParse(SubjectGrades[i].Value, out int grade) && grade >= 0 && grade < 100)
            {
                Subjects[i].Grade = grade;
            }
        }
        CalculateGrade();
    }

    private void SubjectNames_ListChanged(object? sender, ListChangedEventArgs e)
    {
        for (int i = 0; i < Subjects.Length; i++)
        {
            Subjects[i].Name = SubjectNames[i].Value;
        }
        CalculateGrade();
    }

    private void CalculateGrade()
    {
        double grade = 0;
        double creditsSum = Subjects.Where(s => s.Credits > 0 && s.Grade > 0).Sum(s => s.Credits);
        foreach (var subject in Subjects.Where(s => s.Credits > 0 && s.Grade > 0))
        {
            grade += (subject.Credits / creditsSum) * subject.Grade;
        }
        Grade = Math.Round(grade, 2).ToString();
        OnPropertyChanged("Grade");
    }
}

public class MyString : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string _value;
    public string Value
    {
        get { return _value; }
        set
        {
            _value = value;
            OnPropertyChanged("Value");
        }
    }

    void OnPropertyChanged(string propertyName)
    {
        var handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
