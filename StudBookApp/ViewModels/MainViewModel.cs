using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using DynamicData;
using Nito.AsyncEx;
using ReactiveUI;
using StudBookApp.Models;
using StudBookApp.Services;
using StudBookApp.Themes.StyleHelpers;
using StudBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace StudBookApp.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region Fields

    private GroupService _groupService;
    private List<Group> _allGroups;

    #endregion

    #region Private Properties

    private Subject[] Subjects { get; set; }

    #endregion

    #region Public Properties

    public string Grade { get; set; } = "0";

    public BindingList<MyString> SubjectNames { get; set; }

    public BindingList<MyString> SubjectCredits { get; set; }

    public BindingList<MyString> SubjectGrades { get; set; }

    private INotifyTaskCompletion _initializationNotifier;
    public INotifyTaskCompletion InitializationNotifier
    {
        get => _initializationNotifier;
        private set
        {
            _initializationNotifier = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Group> _groups;
    public ObservableCollection<Group> Groups
    {
        get => _groups;
        set
        {
            if (value != null)
            {
                _groups = value;
                OnPropertyChanged();
            }
        }
    }

    private int _groupSelectedIndex;
    public int GroupSelectedIndex
    {
        get => _groupSelectedIndex;
        set
        {
            _groupSelectedIndex = value;
            ClearFields(SubjectNames, SubjectCredits, SubjectGrades);
            SetSubjects();
            OnPropertyChanged();
        }
    }

    private int _facultySelectedIndex;
    public int FacultySelectedIndex
    {
        get => _facultySelectedIndex;
        set
        {
            _facultySelectedIndex = value;
            FilterGroups();
            OnPropertyChanged();
        }
    }

    private int _courseSelectedIndex;
    public int CourseSelectedIndex
    {
        get => _courseSelectedIndex;
        set
        {
            _courseSelectedIndex = value;
            FilterGroups();
            OnPropertyChanged();
        }
    }

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
            Theme.Default => Theme.Khai,
            Theme.Khai => Theme.Default,
            _ => throw new ArgumentOutOfRangeException(nameof(styles.CurrentTheme))
        });
    }
    #endregion

    #endregion

    public MainViewModel(StyleManager? styles)
    {
        Subjects = new Subject[9];
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

        InitializationNotifier = NotifyTaskCompletion.Create(InitializeAsync());

        SubjectNames.ListChanged += SubjectNames_ListChanged;
        SubjectCredits.ListChanged += SubjectCredits_ListChanged;
        SubjectGrades.ListChanged += SubjectGrades_ListChanged;

        CloseApplicationCommand = ReactiveCommand.Create(CloseApplication);
        ChangeThemeCommand = ReactiveCommand.Create(() => ChangeTheme(styles));
    }

    private void SubjectCredits_ListChanged(object? sender, ListChangedEventArgs e)
    {
        for (int i = 0; i < Subjects.Length; i++)
        {
            if (double.TryParse(SubjectCredits[i].Value?.Replace('.', ','), out double credit) && credit >= 0 && credit <= 12)
            {
                Subjects[i].Credits = credit;
            }
            else if (string.IsNullOrEmpty(SubjectCredits[i].Value))
            {
                Subjects[i].Credits = 0;
            }
        }
        CalculateGrade();
    }

    private void SubjectGrades_ListChanged(object? sender, ListChangedEventArgs e)
    {
        for (int i = 0; i < Subjects.Length; i++)
        {
            if (int.TryParse(SubjectGrades[i].Value, out int grade) && grade >= 0 && grade <= 100)
            {
                Subjects[i].Grade = grade;
            }
            else if (string.IsNullOrEmpty(SubjectGrades[i].Value))
            {
                Subjects[i].Grade = -1;
            }
        }
        CalculateGrade();
    }

    private void SubjectNames_ListChanged(object? sender, ListChangedEventArgs e)
    {
        for (int i = 0; i < Subjects.Length; i++)
        {
            Subjects[i].Name = SubjectNames[i].Value ?? "";
        }
        CalculateGrade();
    }

    private async Task InitializeAsync()
    {
        _groupService = new GroupService();
        await LoadData();
    }

    public async Task LoadData()
    {
        var data = await _groupService.GetGroups();
        _allGroups = new List<Group>(data);
        Groups = new ObservableCollection<Group>() { new Group() };
        Groups.AddRange(_allGroups);
        //Groups = new ObservableCollection<Group>(data);
    }

    private void ClearFields(params BindingList<MyString>[] list)
    {
        foreach (var item in list)
        {
            for (int i = 0; i < Subjects.Length; i++)
                item[i].Value = "";
        }
    }

    private void SetSubjects()
    {
        if (GroupSelectedIndex >= 0)
        {
            for (int i = 0; i < Groups[GroupSelectedIndex].Subjects.Count; i++)
            {
                SubjectNames[i].Value = Groups[GroupSelectedIndex].Subjects[i].Name;
                SubjectCredits[i].Value = Groups[GroupSelectedIndex].Subjects[i].Credits.ToString();
            }
        }
    }

    private void FilterGroups()
    {
        if (_allGroups is not null)
        {
            Groups = new ObservableCollection<Group>() { new Group() };
            if (FacultySelectedIndex > 0 && CourseSelectedIndex > 0)
            {
                Groups.AddRange(
                    _allGroups
                    .Where(gr => gr.Faculty == FacultySelectedIndex && gr.Course == CourseSelectedIndex)
                    );
            }
            else if (FacultySelectedIndex > 0)
            {
                Groups.AddRange(
                    _allGroups
                    .Where(gr => gr.Faculty == FacultySelectedIndex)
                    );
            }
            else if (CourseSelectedIndex > 0)
            {
                Groups.AddRange(
                    _allGroups
                    .Where(gr => gr.Course == CourseSelectedIndex)
                    );
            }
            else
            {
                Groups.AddRange(_allGroups);
            }
        }
    }

    private void CalculateGrade()
    {
        double grade = 0;
        var subjectsToCalculate = Subjects.Where(s => s.Credits > 0 && s.Grade >= 0);
        double creditsSum = subjectsToCalculate.Sum(s => s.Credits);

        foreach (var subject in subjectsToCalculate)
        {
            grade += (subject.Credits / creditsSum) * subject.Grade;
        }

        Grade = Math.Round(grade, 2).ToString();
        OnPropertyChanged("Grade");
    }
}

public class MyString : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _value;
    public string? Value
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
