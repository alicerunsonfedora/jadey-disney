using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Jadey
{
    public partial class Markdown
    {
        var tempSelectText = mdDocView.SelectedText;

        public void emphasize(string type)
        {

            if (type == "strong")
            {

                if (tempSelectText == "")
                {
                    mdDocView.SelectedText = "**Insert some text here**";
                }

                else
                {
                    mdDocView.SelectedText = "**" + tempSelectText + "**";
                }
            }

            else if (type == "subtle")
            {
                if (tempSelectText == "")
                {
                    mdDocView.SelectedText = "_Insert some text here_";
                }

                else
                {
                    mdDocView.SelectedText = "_" + tempSelectText + "_";
                }
            }
        }

        public void makeHeader(int level)
        {
            switch (level)
            {
                case 1:
                    if (tempSelectText == "")
                    {
                        mdDocView.SelectedText = "# Insert some text here";
                    }

                    else
                    {
                        mdDocView.SelectedText = "# " + tempSelectText;
                    }

                case 2:
                    if (tempSelectText == "")
                    {
                        mdDocView.SelectedText = "## Insert some text here";
                    }

                    else
                    {
                        mdDocView.SelectedText = "## " + tempSelectText;
                    }

                case 3:
                    if (tempSelectText == "")
                    {
                        mdDocView.SelectedText = "### Insert some text here";
                    }

                    else
                    {
                        mdDocView.SelectedText = "### " + tempSelectText;
                    }
                default:
                    break;

            }
        }
    }
}