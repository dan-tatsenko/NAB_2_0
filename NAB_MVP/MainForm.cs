﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAB_MVC.Views;


namespace NAB_MVC
{
    public partial class frmMainForm : Form, IBankingFileView
    {

        public event EventHandler AddTransactionRequested;
        public event EventHandler UpdateTransacationRequested;
        public event EventHandler<SavingFileEventArgs> SaveFileRequested;
        public event EventHandler<DeletingTransactionEventArg> DeleteTransactionRequested;
        public event EventHandler<SelectedIndexChangedEventArg> SelectedIndexChanged;
        private bool enabledView;

        public frmMainForm(List<string> PaymentChannels, List<string> PaymentInstructions)
        {
            InitializeComponent();
            cbxPaymentChannel.DataSource = null;
            cbxPaymentChannel.DataSource = PaymentChannels;
            cbxPaymentInstruction.DataSource = null;
            cbxPaymentInstruction.DataSource = PaymentInstructions;
        }


        public string SourceIdetifierText { get => txtSourceIdentifier.Text; set => txtSourceIdentifier.Text = value; }
        public string AccountNumberText { get => txtAccountNumber.Text; set => txtAccountNumber.Text = value; }
        public string PaymentInstructionText
        {
            get
            {
                int i = cbxPaymentInstruction.SelectedIndex;
                return cbxPaymentInstruction.Items[i].ToString();
            }
            
            set => cbxPaymentInstruction.SelectedIndex = cbxPaymentInstruction.Items.IndexOf(value);
        }
        public string PaymentChannelText
        {
            get
            {
                int i = cbxPaymentChannel.SelectedIndex;
                return cbxPaymentChannel.Items[i].ToString();
            }

            set => cbxPaymentChannel.SelectedIndex = cbxPaymentChannel.Items.IndexOf(value);
        }
        public string CreditCardText { get => txtCreditCard.Text; set => txtCreditCard.Text = value; }
        public string ErrorCorrectionReasonText { get => txtErrorCorrectionReason.Text; set => txtErrorCorrectionReason.Text = value; }
        public string AmountText { get => txtAmount.Text; set => txtAmount.Text = value; }
        public DateTime PaymentDate { get => dtpPaymentDate.Value; set => dtpPaymentDate.Value = value; }
        public DateTime PaymentTime { get => dtpPaymentTime.Value; set => dtpPaymentTime.Value = value; }
        public DateTime SettlementTime { get => dtpSettlementDate.Value; set => dtpSettlementDate.Value = value; }
        public string BankTransactionIDText { get => txtBankTransactionID.Text; set => txtBankTransactionID.Text = value; }
        public string AuthorisationCodeText { get => txtAuthorisationCode.Text; set => txtAuthorisationCode.Text = value; }
        public string OriginalRefText { get => txtOriginalReference.Text; set => txtOriginalReference.Text = value; }

        public bool IsViewEnabled
        {
            get => enabledView;
            set
            {
                enabledView = value;

                lstFile.Enabled = value;
                txtAccountNumber.Enabled = value;
                txtAmount.Enabled = value;
                txtAuthorisationCode.Enabled = value;
                txtBankTransactionID.Enabled = value;
                txtCreditCard.Enabled = value;
                txtErrorCorrectionReason.Enabled = value;
                txtOriginalReference.Enabled = value;
                txtSourceIdentifier.Enabled = value;
                dtpPaymentDate.Enabled = value;
                dtpPaymentTime.Enabled = value;
                dtpSettlementDate.Enabled = value;
                cbxPaymentChannel.Enabled = value;
                cbxPaymentInstruction.Enabled = value;
                btnUpdateTransaction.Enabled = value;
                btnDeleteTransaction.Enabled = value;
            }
        }

        public void UpdateListDataSource (List<string> data, int index)
        {
            lstFile.DataSource = null;
            lstFile.DataSource = data;
            lstFile.SelectedIndex = index;
        }

        private void btnAddNewTransaction_Click(object sender, EventArgs e)
        {
            AddTransactionRequested(this, EventArgs.Empty);
        }

        private void btnUpdateTransaction_Click(object sender, EventArgs e)
        {
            UpdateTransacationRequested(this, EventArgs.Empty);
        }

        private void btnDeleteTransaction_Click(object sender, EventArgs e)
        {
            DeleteTransactionRequested(this, new DeletingTransactionEventArg(lstFile.SelectedIndex));
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog.ShowDialog();
        }

        private void SaveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            SavingFileEventArgs args = new SavingFileEventArgs(SaveFileDialog.FileName);
            SaveFileRequested(this, args);
        }

        public void ClearView()
        {
            txtAccountNumber.Text = "";
            txtAmount.Text = "";
            txtAuthorisationCode.Text = "";
            txtBankTransactionID.Text = "";
            txtCreditCard.Text = "";
            txtErrorCorrectionReason.Text = "";
            txtOriginalReference.Text = "";
            txtSourceIdentifier.Text = "";
            dtpPaymentDate.Value = DateTime.Today;
            dtpPaymentTime.Value = DateTime.Now;
            dtpSettlementDate.Value = DateTime.Today;
            cbxPaymentChannel.SelectedIndex = 0;
            cbxPaymentInstruction.SelectedIndex = 0;
        }

        private void lstFile_Click(object sender, EventArgs e)
        {
            if (lstFile.SelectedIndex > -1)
            {
                SelectedIndexChanged(this, new SelectedIndexChangedEventArg(lstFile.SelectedIndex));
            }
        }
    }
}
