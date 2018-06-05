using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GControlSrv;
using GControlSrv.Forms;

namespace GControlSrv
{
  public class AppContextMenu : ContextMenu
  {
    public AppContextMenu()
    {
      MenuItems.Add(new MenuItem("Settings", MiEditSettingsClick));
      MenuItems.Add(new MenuItem("Test", MiShowmainForm));
      MenuItems.Add("-");
      MenuItems.Add(new MenuItem("E&xit", MiExitClick));
      //contextMenu1.MenuItems.AddRange(new MenuItem[] { menuItem1 });
    }

    private static void MiExitClick(object Sender, EventArgs e) => Application.Exit();
    private static void MiEditSettingsClick(object Sender, EventArgs e) => Program.EditSettings();
    private static void MiShowmainForm(object Sender, EventArgs e) => Program.ShowMainForm();
  }
}
