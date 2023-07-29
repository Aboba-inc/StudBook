using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace StudBookApp.Views.CustomControls
{
    public class SubjectField : TemplatedControl
    {
        public static readonly StyledProperty<IBrush> BackgroundSubjectProperty =
    AvaloniaProperty.Register<SubjectField, IBrush>(nameof(BackgroundSubject), Brushes.White);

        public IBrush BackgroundSubject
        {
            get { return GetValue(BackgroundSubjectProperty); }
            set { SetValue(BackgroundSubjectProperty, value); }
        }

        public static readonly StyledProperty<IBrush> BackgroundCreditProperty =
AvaloniaProperty.Register<SubjectField, IBrush>(nameof(BackgroundCredit), Brushes.White);

        public IBrush BackgroundCredit
        {
            get { return GetValue(BackgroundCreditProperty); }
            set { SetValue(BackgroundCreditProperty, value); }
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

        public static readonly StyledProperty<string> TextCreditProperty =
AvaloniaProperty.Register<SubjectField, string>(nameof(TextCredit));

        public string TextCredit
        {
            get { return GetValue(TextCreditProperty); }
            set { SetValue(TextCreditProperty, value); }
        }

        public static readonly StyledProperty<string> TextGradeProperty =
AvaloniaProperty.Register<SubjectField, string>(nameof(TextGrade));

        public string TextGrade
        {
            get { return GetValue(TextGradeProperty); }
            set { SetValue(TextGradeProperty, value); }
        }
    }
}
