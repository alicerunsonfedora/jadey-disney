using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409



namespace Jadey
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public partial class MainPage : Page
    {
        string mdDocContents { get; set; }
        string mdDocKuraberu { get; set; }
        string fileName { get; set; }
        ApplicationView titleBar = ApplicationView.GetForCurrentView();

        public MainPage()
        {
            this.InitializeComponent();
            titleBar.Title = "Untitled Document";
        }

        #region File Management and Information
        /// <summary>
        /// File management
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void newButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            mdDocKuraberu = mdDocView.Text;
            if (mdDocKuraberu != "" || mdDocKuraberu != mdDocContents)
            {
                ContentDialog subscribeDialog = new ContentDialog
                {
                    Title = "Continue without saving?",
                    Content = "Are you sure you want to make a new document without saving this one first?",
                    CloseButtonText = "Cancel",
                    PrimaryButtonText = "Yes",
                    SecondaryButtonText = "Save first",
                    DefaultButton = ContentDialogButton.Primary
                };

                ContentDialogResult result = await subscribeDialog.ShowAsync();

                switch(result)
                {
                    case ContentDialogResult.Primary:
                        mdDocView.Text = "";
                        break;

                    case ContentDialogResult.Secondary:
                        saveFile();
                        mdDocView.Text = "";
                        break;
                    default:
                        break;
                }
                    

            }
            
        }

        private void mdDocView_TextChanged(object sender, RoutedEventArgs e)
        {
            mdDocKuraberu = mdDocView.Text;
            if (mdDocContents != mdDocKuraberu)
            {
                if (titleBar.Title == fileName)
                {
                    titleBar.Title = fileName + "*";
                } else
                {
                    titleBar.Title = "Untitled Document*";
                }

                
            }
        }

        private async void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog aboutDialog = new ContentDialog
            {
                Title = "About Jadey",
                Content = "Jadey 1.0.0\n(C) 2017 | Marquis Kurt\nLicensed under GNU GPL v3.",
                CloseButtonText = "Dismiss",
            };

            ContentDialogResult result = await aboutDialog.ShowAsync();
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            // Display message showing the label of the command that was invoked
            
        }

        private async void openButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".md");


            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)

            {
                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read); using
                (StreamReader reader = new StreamReader(stream.AsStream()))
                {
                    mdDocView.Text = reader.ReadToEnd();
                    mdDocContents = mdDocView.Text;
                    fileName = file.Name;
                    titleBar.Title = fileName;
                }


            }
        }

        private void SaveButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            saveFile();
        }

        public async void saveFile()
        {
            var fileContents = mdDocView.Text;

            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.SuggestedFileName = "YYYY-MM-DD-Name-of-post";
            savePicker.FileTypeChoices.Add("Markdown File", new List<string>() { ".md" });

            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until
                // we finish making changes and call CompleteUpdatesAsync.
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                // write to file
                await Windows.Storage.FileIO.WriteTextAsync(file, mdDocView.Text);
                // Let Windows know that we're finished changing the file so
                // the other app can update the remote version of the file.
                // Completing updates may require Windows to ask for user input.
                Windows.Storage.Provider.FileUpdateStatus status =
                    await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
            }
        }
        #endregion

        #region Insertions
        /// <summary>
        /// Inserting things into the DocView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            var tempSelectText = mdDocView.SelectedText;

            if (tempSelectText == "")
            {
                mdDocView.SelectedText = "**Insert some text here**";
            }

            else
            {
                mdDocView.SelectedText = "**" + tempSelectText + "**";
            }
        }

        private void italicsButton_Click(object sender, RoutedEventArgs e)
        {
            var tempSelectText = mdDocView.SelectedText;

            if (tempSelectText == "")
            {
                mdDocView.SelectedText = "_Insert some text here_";
            }

            else
            {
                mdDocView.SelectedText = "_" + tempSelectText + "_";
            }
        }

        private void headingAMenu_Click(object sender, RoutedEventArgs e)
        {
            var tempSelectText = mdDocView.SelectedText;

            if (tempSelectText == "")
            {
                mdDocView.SelectedText = "# Insert some text here";
            }

            else
            {
                mdDocView.SelectedText = "# " + tempSelectText;
            }
        }

        private void headingBMenu_Click(object sender, RoutedEventArgs e)
        {
            var tempSelectText = mdDocView.SelectedText;

            if (tempSelectText == "")
            {
                mdDocView.SelectedText = "## Insert some text here";
            }

            else
            {
                mdDocView.SelectedText = "## " + tempSelectText;
            }
        }

        private void headingCMenu_Click(object sender, RoutedEventArgs e)
        {
            var tempSelectText = mdDocView.SelectedText;

            if (tempSelectText == "")
            {
                mdDocView.SelectedText = "### Insert some text here";
            }

            else
            {
                mdDocView.SelectedText = "### " + tempSelectText;
            }
        }

        private void headingDMenu_Click(object sender, RoutedEventArgs e)
        {
            var tempSelectText = mdDocView.SelectedText;

            if (tempSelectText == "")
            {
                mdDocView.SelectedText = "#### Insert some text here";
            }

            else
            {
                mdDocView.SelectedText = "#### " + tempSelectText;
            }
        }

        private void headingEMenu_Click(object sender, RoutedEventArgs e)
        {
            var tempSelectText = mdDocView.SelectedText;

            if (tempSelectText == "")
            {
                mdDocView.SelectedText = "##### Insert some text here";
            }

            else
            {
                mdDocView.SelectedText = "##### " + tempSelectText;
            }
        }

        private void headingFMenu_Click(object sender, RoutedEventArgs e)
        {
            var tempSelectText = mdDocView.SelectedText;

            if (tempSelectText == "")
            {
                mdDocView.SelectedText = "###### Insert some text here";
            }

            else
            {
                mdDocView.SelectedText = "###### " + tempSelectText;
            }
        }

        private void jekyllButton_Click(object sender, RoutedEventArgs e)
        {
            var tempPost = mdDocView.Text;
            var jekyllLayout = "---\nlayout: post\nauthor: John Seymour\ntitle: Name of post\ncategories: markdown\nexcerpt-separator: <!--more-->\n---";

            if (tempPost != "")
            {
                mdDocView.Text = jekyllLayout + "\n" + tempPost;
            }
            else
            {
                mdDocView.Text = jekyllLayout + "\n";
            }
        }
        

        /*private void insertLink_Click(object sender, RoutedEventArgs e)
        {
            var linkNameText = linkName.Text;
            var linkURLText = linkURL.Text;
            mdDocView.SelectedText = "[" + linkNameText + "](" + linkURLText + ")";
        }
        

        private void insertImage_Click(object sender, RoutedEventArgs e)
        {
            var imgText = imageName.Text;
            var imgUrl = imageURL.Text;
            mdDocView.SelectedText = "![" + imgText + "](" + imgUrl + ")";
        }*/
        #endregion
    }
}