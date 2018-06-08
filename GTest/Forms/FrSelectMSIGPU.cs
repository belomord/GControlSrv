using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MApps;

namespace GTest.Forms
{

  public partial class FrSelectMSIGPU: Form
  {
    static public string SelectGPU(IWin32Window owner=null, string OldGpuId="")
    {
      string result = "";

      using (FrSelectMSIGPU frSelectMSIGPU = new FrSelectMSIGPU())
      {
        frSelectMSIGPU.oldGPUId = OldGpuId;

        frSelectMSIGPU.Reload();

        if (frSelectMSIGPU.ShowDialog(owner) == DialogResult.OK)
        {
          result = frSelectMSIGPU.CurrentGUIDId;
        }
      }

      return result;
    }

    string oldGPUId="";

    void InitDgvMSI()
    {
      dgvMSI.Rows.Clear();
      dgvMSI.Columns.Clear();
      dgvMSI.AutoGenerateColumns = false;
      dgvMSI.RowHeadersVisible = false;
      dgvMSI.MultiSelect = false;
      dgvMSI.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      dgvMSI.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      dgvMSI.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

      int normalColumnWidth = 100;

      //dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "", ReadOnly = true, Width = 30 });
      //dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Index", ReadOnly = true, Width = normalColumnWidth });
      //dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Device", ReadOnly = true, Width = normalColumnWidth * 2 });
      //dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Family", ReadOnly = true, Width = normalColumnWidth });
      //dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "GpuId", ReadOnly = true, Width = 400 });

      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { Name = "", ReadOnly = true, Width = 30 });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Index", ReadOnly = true, Width = (int)normalColumnWidth / 2 });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Type", ReadOnly = true, Width = (int)normalColumnWidth / 2 });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Type Idx", ReadOnly = true, Width = (int)normalColumnWidth / 2 });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Device", ReadOnly = true, Width = normalColumnWidth * 2 });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Family", ReadOnly = true, Width = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { Name = "GpuId", ReadOnly = true, Width = 400 });
    }

    void FillDgvMSI()
    {
      InitDgvMSI();

      for (int i = 0; i < MSIABControl.GpuCount; i++)
      {
        GPUDrvType DrvType;
        int DrvTypeIndex;

        MSIABControl.GetType(i, out DrvType, out DrvTypeIndex);
        dgvMSI.Rows.Add(new String[]
                            { i.ToString(),
                              MSIABControl.MAHM.GpuEntries[i].Index.ToString(),
                              DrvType.ToString(),
                              DrvTypeIndex.ToString(),
                              MSIABControl.MAHM.GpuEntries[i].Device,
                              MSIABControl.MAHM.GpuEntries[i].Family,
                              MSIABControl.MAHM.GpuEntries[i].GpuId
                            });
      }

    }

    void SetFormState()
    {
      btnOk.Enabled = CurrentGUIDId != "";
    }

    public MSIABControlState Reload()
    {
      MSIABControlState result;
      result = MSIABControl.Start(0);

      if (result == MSIABControlState.Success)
      {
        FillDgvMSI();
        for (int i = 0; i < dgvMSI.RowCount; i++)
        {
          if ((dgvMSI["GpuId", i].Value != null) && (oldGPUId.Trim() == dgvMSI["GpuId", i].Value.ToString().Trim()))
          {
            dgvMSI.CurrentCell = dgvMSI.Rows[i].Cells[0];
            break;
          }
        }
      }
      else
      {
        InitDgvMSI();
      }

      SetFormState();
      lblState.Text = "Reload: " + result.ToString();
      return result;
    }

    public int GetCurrentIndex()
    {
      return (dgvMSI == null)||(dgvMSI.CurrentRow == null) ? -1 : dgvMSI.CurrentRow.Index;
    }

    public string CurrentGUIDId { get => (GetCurrentIndex() >= 0) && (GetCurrentIndex() < dgvMSI.RowCount) ? (string)dgvMSI["GpuId", GetCurrentIndex()].Value : ""; }

    public FrSelectMSIGPU()
    {
      InitializeComponent();
      //InitDgvMSI();
    }

    private void dgvMSI_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      SetFormState();
    }

    private void dgvMSI_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      SetFormState();
    }

    private void btnReload_Click(object sender, EventArgs e)
    {
      Reload();
    }
  }
}
