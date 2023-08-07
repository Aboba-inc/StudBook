using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Globalization;

namespace StudBookApp.Views.CustomControls
{
    public class SubjectField : TemplatedControl
    {
        TextBox? _gradeTextBox;
        TextBox? _creditsTextBox;
        string? _creditValue;

        public static readonly StyledProperty<IBrush> BackgroundSubjectProperty =
        AvaloniaProperty.Register<SubjectField, IBrush>(nameof(BackgroundSubject), Brushes.White);

        public IBrush BackgroundSubject
        {
            get { return GetValue(BackgroundSubjectProperty); }
            set { SetValue(BackgroundSubjectProperty, value); }
        }

        public static readonly StyledProperty<IBrush> BackgroundCreditsProperty =
        AvaloniaProperty.Register<SubjectField, IBrush>(nameof(BackgroundCredits), Brushes.White);

        public IBrush BackgroundCredits
        {
            get { return GetValue(BackgroundCreditsProperty); }
            set { SetValue(BackgroundCreditsProperty, value); }
        }

        public static readonly StyledProperty<IBrush> BackgroundGradeProperty =
        AvaloniaProperty.Register<SubjectField, IBrush>(nameof(BackgroundGrade), Brushes.White);

        public IBrush BackgroundGrade
        {
            get { return GetValue(BackgroundGradeProperty); }
            set { SetValue(BackgroundGradeProperty, value); }
        }

        public static readonly StyledProperty<string> TextSubjectProperty =
        AvaloniaProperty.Register<SubjectField, string>(nameof(TextSubject));

        public string TextSubject
        {
            get { return GetValue(TextSubjectProperty); }
            set { SetValue(TextSubjectProperty, value); }
        }

        public static readonly StyledProperty<string> TextCreditsProperty =
        AvaloniaProperty.Register<SubjectField, string>(nameof(TextCredits));

        public string TextCredits
        {
            get { return GetValue(TextCreditsProperty); }
            set { SetValue(TextCreditsProperty, value); }
        }

        public static readonly StyledProperty<string> TextGradeProperty =
        AvaloniaProperty.Register<SubjectField, string>(nameof(TextGrade));

        public string TextGrade
        {
            get { return GetValue(TextGradeProperty); }
            set { SetValue(TextGradeProperty, value); }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            // if we had a control template before, we need to unsubscribe any event listeners
            if (_gradeTextBox is not null)
            {
                _gradeTextBox.KeyDown -= GradeTextBox_KeyDown;
                _gradeTextBox.PastingFromClipboard -= GrateTextBox_PastingFromClipboard;
                _gradeTextBox.TextChanged -= GradeTextBox_TextChanged;
            }

            if (_creditsTextBox is not null)
            {
                _creditsTextBox.TextChanged -= CreditsTextBox_LostFocus;
            }
            // try to find the control with the given name
            _gradeTextBox = e.NameScope.Find("GradeTextBox") as TextBox;
            _creditsTextBox = e.NameScope.Find("CreditsTextBox") as TextBox;

            // listen to pointer-released events on the stars presenter.
            if (_gradeTextBox is not null)
            {
                _gradeTextBox.KeyDown += GradeTextBox_KeyDown;
                _gradeTextBox.PastingFromClipboard += GrateTextBox_PastingFromClipboard;
                _gradeTextBox.TextChanged += GradeTextBox_TextChanged;
            }

            if (_creditsTextBox is not null)
            {
                _creditsTextBox.LostFocus += CreditsTextBox_LostFocus;
            }
        }

        private void CreditsTextBox_LostFocus(object? sender, RoutedEventArgs e)
        {
            if (_creditsTextBox is not null)
            {
                if (string.IsNullOrEmpty(_creditsTextBox.Text)
                    || (double.TryParse(_creditsTextBox.Text, CultureInfo.InvariantCulture, out double credit) && credit >= 0 && credit <= 12))
                {
                    _creditValue = _creditsTextBox.Text;
                }
                else
                {
                    _creditsTextBox.Text = _creditValue;
                }
            }
        }

        private void GradeTextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            if (_gradeTextBox?.Text == "00")
            {
                _gradeTextBox.Text = "0";
            }

            if (_gradeTextBox is not null && _gradeTextBox.Text?.Length > 1 && _gradeTextBox.Text[0] == '0')
            {
                _gradeTextBox.Text = _gradeTextBox.Text.TrimStart('0');
            }
        }

        private void GradeTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            string key = e.Key.ToString();

            if (e.KeyModifiers == KeyModifiers.Shift
                || _gradeTextBox is null
                || _gradeTextBox.Text is null
                || key.Length != 2
                || key[0] != 'D'
                || !Char.IsDigit(key[1])
                || (_gradeTextBox.SelectionStart == _gradeTextBox.SelectionEnd && int.TryParse(_gradeTextBox.Text.Insert(Math.Min(_gradeTextBox.SelectionStart, _gradeTextBox.SelectionEnd), key[1].ToString()), out int grade1) && grade1 > 100)
                || (_gradeTextBox!.SelectionStart != _gradeTextBox.SelectionEnd && int.TryParse(_gradeTextBox.Text.Remove(Math.Min(_gradeTextBox.SelectionStart, _gradeTextBox.SelectionEnd), Math.Abs(_gradeTextBox.SelectionEnd - _gradeTextBox.SelectionStart)).Insert(Math.Min(_gradeTextBox.SelectionStart, _gradeTextBox.SelectionEnd), key[1].ToString()), out int grade2) && grade2 > 100)
                || (_gradeTextBox.Text.Length > 0 && Math.Min(_gradeTextBox.SelectionStart, _gradeTextBox.SelectionEnd) == 0 && key[1] == '0'))
            {
                e.Handled = true;
            }
        }

        private void GrateTextBox_PastingFromClipboard(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string? text = TopLevel.GetTopLevel(this)?.Clipboard?.GetTextAsync().Result;
            if (!int.TryParse(_gradeTextBox?.Text + text, out int grade)
                || grade > 100
                || grade < 0
                || (_gradeTextBox?.Text?.Length == 0 && grade == 0))
            {
                e.Handled = true;
            }
        }
    }
}