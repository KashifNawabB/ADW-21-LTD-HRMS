﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public partial class JobHistoryDetails : Form
    {
        Commoncls cls = new Commoncls();

        JBHistory jbh = new JBHistory();
        JBHistoryDAL jdal = new JBHistoryDAL();
        JBHistoryBL jbal = new JBHistoryBL();
        UtilityClass ex = new UtilityClass();


        public JobHistoryDetails()
        {
            InitializeComponent();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            int insertrec = 0;
            try
            {
                jbh.JBHID = txtjbhid.Text;
                jbh.JBEMPID = cmbemp.SelectedValue.ToString();
                jbh.JBJOINGDATE = datejbhjoing.Value.Date;
                jbh.JBRSGDATE = datejbhresign.Value.Date;
                jbh.JBTITLE = txtjbtitle.Text;
                jbh.COMMENT = txtcomment.Text;
                insertrec = jbal.InsertJBH(jbh);

                if (insertrec > 0)
                {
                    MessageBox.Show("Record Insert Successfull","Insert",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Record Already Insert!!!");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "Insert Error");
            }
            finally
            {
                GridBind();
            }
        }

        private void JobHistoryDetails_Load(object sender, EventArgs e)
        {
            Load_Combo();
            GridBind();
        }

        private void Load_Combo()
        {
            cls.setComboList(this.cmbemp, "Select Emp_Id,Full_Name From Emp_Details Order By Emp_Id ASC", "Emp_Details", "Emp_Id", "Emp_Id");
            cls.setComboList(this.cmbfind, "Select Emp_Id,Full_Name From Emp_Details Order By Emp_Id ASC", "Emp_Details", "Emp_Id", "Emp_Id");
        }
        private void GridBind()
        {
            GridView1.DataSource = jdal.GetData();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
              
                jbh.JBEMPID = cmbemp.SelectedValue.ToString();
                jbh.JBHID = txtjbhid.Text;
                jbh.JBJOINGDATE = DateTime.Parse(datejbhjoing.Text);
                jbh.JBRSGDATE = DateTime.Parse(datejbhresign.Text);
                jbh.JBTITLE = txtjbtitle.Text;
                jbh.COMMENT = txtcomment.Text;
                jbal.Update_JBH(jbh);

                MessageBox.Show("Record Update Successfull", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GridBind();
                ex.ClearFormFields(this);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(),"Update error");
            }
        }

        private void btnfind_Click(object sender, EventArgs e)
        {
            try
            {
                String empId = cmbfind.SelectedValue.ToString().Trim();

                DataSet ds = jbal.Find_JBH(empId);
                DataRow row;
                row = ds.Tables[0].Rows[0];

                MessageBox.Show(row["Full_Name"].ToString());

                //GridBind();

                foreach (DataRow rows in ds.Tables[0].Rows)
                {
                    txtjbhid.Text = rows["Jh_ID"].ToString();
                    cmbemp.Text = row["Emp_Id"].ToString();
                    datejbhjoing.Text = row["Jh_Joingdate"].ToString();
                    datejbhresign.Text = row["jh_Resigndate"].ToString();
                    txtjbtitle.Text = row["jh_jobtitle"].ToString();
                    txtcomment.Text = row["jh_comment"].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = GridView1.SelectedRows[0];
                jbh.JBHID = selectedRow.Cells["Jh_ID"].Value.ToString();
                jbal.Delete_JBH(jbh);
                MessageBox.Show("Record Delete Successfull", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GridBind();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            ex.ClearFormFields(this);
        }

        private void btclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExpExc_Click(object sender, EventArgs e)
        {
            ex.Export_to_Excel(GridView1);
        }
    }
}
