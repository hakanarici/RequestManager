using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Netsparker.Core.RequestManager;

namespace Netsparker.RequestManager
{
    public partial class MainForm : Form
    {
        #region Private Members

        private IHttpRequestManager m_RequestManager;
        private List<HttpResponse> m_RequestHistory;
        private BindingSource m_Source;

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            m_RequestManager = new HttpRequestManager();
            m_RequestHistory = new List<HttpResponse>();
            m_Source = new BindingSource();
            m_Source.DataSource = m_RequestHistory;
            initializeRequestHistory();
        }

        #endregion

        #region Private Methods

        private void initializeRequestHistory()
        {
            dgvResponses.DataSource = null;
            dgvResponses.DataSource = m_Source;

            dgvResponses.Columns["Headers"].Visible = false;
            dgvResponses.Columns["Body"].Visible = false;

            dgvResponses.Columns["BodyLength"].HeaderText = "Body";
            dgvResponses.AutoResizeColumns();
            dgvResponses.Refresh();

            // Dirty hack to work around unbound DataGridView OutOfRangeException bug.
            var newItem = m_Source.AddNew();
            m_Source.Remove(newItem);
        }

        private void updateResponseData(string headers, string body)
        {
            txtResponse.Text = String.Format("{0}\n\nBody:[{1}]", headers, body);
        }

        private void updateStatusBar(int statusCode, string status, string host)
        {
            lblStatus.Text = String.Format("{0} ({1}) : {2}", statusCode, status, host);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
 
        private void writeError(string exceptionMessage)
        {
            lblStatus.Text = String.Format("ERROR: {0}", exceptionMessage);
        }

        private IHttpRequestBuilder createRequestBuilder()
        {
            IHttpRequestBuilder requestBuilder = m_RequestManager.GetBuilder();

            requestBuilder.OnRequestExecuted += requestBuilder_OnRequestExecuted;

            return requestBuilder;
        }

        #region Event Handlers

        private void requestBuilder_OnRequestExecuted(object sender, RequestExecutedEventArgs args)
        {
            // Use txtResponse as dispatcher
            txtResponse.Invoke(new Action(() =>
            {
                int hostEndIndex = args.URL.IndexOf(args.Host) + args.Host.Length;
                string URL = args.URL.Substring(hostEndIndex, args.URL.Length - hostEndIndex);
                // Add new request history item
                m_RequestHistory.Add(new HttpResponse { Result = args.StatusCode, Status = args.Status, Host = args.Host, URL = URL, Headers = args.Headers, Body = args.Body, BodyLength = args.Body.Length });
                updateResponseData(args.Headers, args.Body);
                // Update status bar
                updateStatusBar(args.StatusCode, args.Status, args.Host);
                // Re-initialize datagridview to update datasource
                initializeRequestHistory();
                // Select last item in the request history
                dgvResponses.Rows[m_RequestHistory.Count - 1].Selected = true;
            }));

            IHttpRequestBuilder builder = (IHttpRequestBuilder)sender;
            builder.OnRequestExecuted -= requestBuilder_OnRequestExecuted;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            btnSend.Invoke(new Action(() =>
            {
                IHttpRequestBuilder requestBuilder = createRequestBuilder();
                try
                {
                    lblStatus.Text = "Executing...";
                    requestBuilder.ExecuteRequest(txtRawRequest.Text);
                }
                catch (Exception ex)
                {
                    writeError(ex.Message);
                }
            }));
        }

        private void dgvResponses_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            return;
        }

        private void dgvResponses_SelectionChanged(object sender, EventArgs e)
        {
            int selectedIndex = dgvResponses.SelectedRows.Count > 0 ? dgvResponses.SelectedRows[0].Index : -1;

            if (selectedIndex >= 0)
            {
                HttpResponse response = m_RequestHistory[selectedIndex];
                if (!String.IsNullOrEmpty(response.Headers))
                {
                    updateResponseData(response.Headers, response.Body);
                    updateStatusBar(response.Result, response.Status, response.Host);
                }
            }
        }

        private void txtRawRequest_KeyDown(object sender, KeyEventArgs e)
        {
            // Workaround multiline textbox select all restriciton
            if (e.Control && e.KeyCode == Keys.A)
                txtRawRequest.SelectAll();
        }

        #endregion
        
        #endregion
    }
}
 