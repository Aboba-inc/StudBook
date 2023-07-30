using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using System;

namespace StudBookApp.Views.CustomControls
{
    public class SubjectField : TemplatedControl
    {
        TextBox? _gradeTextBox;

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
                _gradeTextBox.KeyDown -= OnGrade_KeyDown;
                _gradeTextBox.PastingFromClipboard -= OnGrateTextBox_PastingFromClipboard;
            }

            // try to find the control with the given name
            _gradeTextBox = e.NameScope.Find("GradeTextBox") as TextBox;

            // listen to pointer-released events on the stars presenter.
            if (_gradeTextBox != null)
            {
                _gradeTextBox.KeyDown += OnGrade_KeyDown;
                _gradeTextBox.PastingFromClipboard += OnGrateTextBox_PastingFromClipboard;
            }
        }

        private void OnGrade_KeyDown(object? sender, KeyEventArgs e)
        {
            string key = e.Key.ToString();

            if ((key.Length != 2 || key[0] != 'D' || !Char.IsDigit(key[1]))
                || (int.Parse(_gradeTextBox?.Text + key[1].ToString()) > 100)
                || (_gradeTextBox?.Text is null || (_gradeTextBox?.Text?.Length == 0 && int.Parse(key[1].ToString()) == 0)))
            {
                e.Handled = true;
            }
        }

        private void OnGrateTextBox_PastingFromClipboard(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string? text = TopLevel.GetTopLevel(this)?.Clipboard?.GetTextAsync().Result;
            if (!int.TryParse(_gradeTextBox?.Text + text, out int grade) || grade > 100 || grade < 0
                || (_gradeTextBox?.Text?.Length == 0 && grade == 0))
                e.Handled = true;
        }

    }
}
