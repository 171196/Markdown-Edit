﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Infrastructure;
using MahApps.Metro.Actions;
using MahApps.Metro.Controls;

namespace UserModule.Views
{
    public partial class SettingsPanel
    {
        private Brush _textBrush;
        private Style _toggleSwitchStyle;

        public SettingsPanel()
        {
            InitializeComponent();
            SetupResources();
            AddControls();
        }

        private void SetupResources()
        {
            _textBrush = new SolidColorBrush(Colors.GhostWhite);
            _textBrush.Freeze();

            _toggleSwitchStyle = new Style(
                typeof(ToggleSwitch),
                Application.Current.FindResource("MahApps.Metro.Styles.ToggleSwitch.Win10") as Style);
        }

        private void AddControls()
        {
            //todo: localize
            var editorSection = SectionHeader("Editor");
            editorSection.Children.Add(ToggleSwitch("Word Wrap", "Ctrl+W"));
            editorSection.Children.Add(ToggleSwitch("Auto Save", "Alt+S"));
            editorSection.Children.Add(ToggleSwitch("Line Numbers", null));
            editorSection.Children.Add(ToggleSwitch("Open Last File", null));
            editorSection.Children.Add(ToggleSwitch("Format Text on Save", null));
            editorSection.Children.Add(ToggleSwitch("Remember Cursor Position", null));
            editorSection.Children.Add(ToggleSwitch("Highlight Current Line", null));
            editorSection.Children.Add(ToggleSwitch("Show Vertical Scrollbar", null));
            editorSection.Children.Add(ToggleSwitch("Show Tabs", null));
            editorSection.Children.Add(ToggleSwitch("Show Space", null));
            editorSection.Children.Add(ToggleSwitch("Show Line Endings", null));
            editorSection.Children.Add(ToggleSwitch("Sychronize Scroll Positions", null));
            editorSection.Children.Add(ToggleSwitch("Github Markdown", null));
            editorSection.Children.Add(ToggleSwitch("Yes, I Donated!", null));

            var formatSection = SectionHeader("Format");
            formatSection.Children.Add(new FontChooser());
            formatSection.Children.Add(LineEndings());
            formatSection.Children.Add(Encodings());

            var spellCheckSection = SectionHeader("Spell Checking");
            spellCheckSection.Children.Add(ToggleSwitch("Spell Checking", "Ctrl+F7"));

            var advancedSection = SectionHeader("Advanced");

            var aboutSection = SectionHeader("About");
            aboutSection.Children.Add(AboutText("Version", Globals.AssemblyVersion));
            aboutSection.Children.Add(AboutLink("Web", "http://markdownedit.com", "http://markdownedit.com"));
            aboutSection.Children.Add(AboutLink("Donate", "http://mike-ward.net/donate", "http://mike-ward.net/donate"));
        }

        private StackPanel SectionHeader(string text)
        {
            Panel.Children.Add(TextBlock(text));
            Panel.Children.Add(new Separator { Margin = new Thickness(0, 3, 0, 5) });
            var stackPanel = new StackPanel { Margin = new Thickness(30, 5, 30, 5) };
            Panel.Children.Add(stackPanel);
            return stackPanel;
        }

        private ToggleSwitch ToggleSwitch(string label, string tooltip)
        {
            var toggle = new ToggleSwitch
            {
                OnLabel = $" {label}",
                OffLabel = $" {label}",
                Foreground = _textBrush,
                Margin = new Thickness(0, 5, 0, 5),
                ToolTip = tooltip,
                Style = _toggleSwitchStyle
            };
            return toggle;
        }

        private Grid LineEndings()
        {
            var grid = new Grid();
            var textBlock = TextBlock("Line Endings");
            grid.Children.Add(textBlock); // todo: localize
            return grid;
        }

        private Grid Encodings()
        {
            var grid = new Grid();
            var textBlock = TextBlock("Encoding"); // todo: localize
            textBlock.Width = 125;
            textBlock.HorizontalAlignment = HorizontalAlignment.Left;
            grid.Children.Add(textBlock);

            var encodingChooser = new EncodingChooser
            {
                Width = 150,
                HorizontalAlignment = HorizontalAlignment.Right
            };

            grid.Children.Add(encodingChooser);
            return grid;
        }

        private readonly Thickness _margin = new Thickness(110, 0, 0, 0);

        private Grid AboutText(string label, string value)
        {
            var grid = new Grid();
            grid.Children.Add(TextBlock(label));

            var tv = TextBlock(value);
            tv.Margin = _margin;
            grid.Children.Add(tv);

            return grid;
        }

        private Grid AboutLink(string label, string text, string link)
        {
            var grid = new Grid();
            grid.Children.Add(TextBlock(label));
            grid.Children.Add(TextLink(text, link));
            return grid;
        }

        private TextBlock TextBlock(string text)
        {
            return new TextBlock(new Run(text))
            {
                FontSize = 15,
                Foreground = _textBrush
            };
        }

        private TextBlock TextLink(string text, string link)
        {
            var hyperLink = new Hyperlink { NavigateUri = new Uri(link) };
            hyperLink.RequestNavigate += (s, e) => Process.Start(link);
            hyperLink.Inlines.Add(TextBlock(text));

            var tb = TextBlock("");
            tb.Inlines.Clear();
            tb.Inlines.Add(hyperLink);
            tb.Margin = _margin;
            return tb;
        }
    }
}