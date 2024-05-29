using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPHDotNetCore.Shared;
using TPHDotNetCore.WinFormsApp.Models;

namespace TPHDotNetCore.WinFormsApp
{

    public partial class FrmBlogList : Form
    {
        private readonly DapperService _dapperService;

        //private const int _edit = 1;
        //private const int _delete = 1;
        public FrmBlogList()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            _dapperService = new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }

        private void FrmBlogList_Load(object sender, EventArgs e)
        {
            BlogList();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var blogId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["colId"].Value);

            if (e.ColumnIndex == (int)EnumFormControlType.Edit)
            {
                FrmBlog frm = new FrmBlog(blogId);
                frm.ShowDialog();

                BlogList();
            }
            else if(e.ColumnIndex == (int)EnumFormControlType.Delete)
            {
                var dialogResult = MessageBox.Show("Are you sure about deleting?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes) return;

                DeleteBlog(blogId);


            }

            #region switch enum

            //EnumFormControlType enumFormControlType = EnumFormControlType.None;

            //switch (enumFormControlType)
            //{
            //    case EnumFormControlType.None:
            //        break;
            //    case EnumFormControlType.Delete:
            //        break;
            //    case EnumFormControlType.Edit:
            //        break;
            //}

            #endregion
        }


        private void BlogList()
        {
            List<BlogModel> lst = _dapperService.Query<BlogModel>(@"SELECT [BlogId]
                                                                      ,[BlogTitle]
                                                                      ,[BlogAuthor]
                                                                      ,[BlogContent]
                                                                  FROM [dbo].[Tbl_Blog]");
            dgvData.DataSource = lst;
        }

        private void DeleteBlog(int id)
        {
            string query = @"DELETE [dbo].[Tbl_Blog]
                            WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, new { BlogId = id });
            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            MessageBox.Show(message);
            BlogList();
        }
    }
}
