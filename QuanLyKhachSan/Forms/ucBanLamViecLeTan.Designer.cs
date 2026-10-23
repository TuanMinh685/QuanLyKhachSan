namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class ucBanLamViecLeTan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblDatePill = new System.Windows.Forms.Label();
            this.tblQuickActions = new System.Windows.Forms.TableLayoutPanel();
            this.tblMainColumns = new System.Windows.Forms.TableLayoutPanel();
            this.cardActivities = new System.Windows.Forms.Panel();
            this.lblActivitiesTitle = new System.Windows.Forms.Label();
            this.lblActivitiesUpdated = new System.Windows.Forms.Label();
            this.dgvActivities = new System.Windows.Forms.DataGridView();
            this.flowActivityButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBottomTaoDat = new System.Windows.Forms.Button();
            this.btnBottomNhanPhong = new System.Windows.Forms.Button();
            this.btnBottomTraPhong = new System.Windows.Forms.Button();
            this.cardAlerts = new System.Windows.Forms.Panel();
            this.lblAlertsTitle = new System.Windows.Forms.Label();
            this.lblAlertsBadge = new System.Windows.Forms.Label();
            this.pnlAlertOverdue = new System.Windows.Forms.Panel();
            this.pnlAlertCleaning = new System.Windows.Forms.Panel();
            this.pnlAlertMaintenance = new System.Windows.Forms.Panel();
            this.cardShiftNote = new System.Windows.Forms.Panel();
            this.lblShiftNoteTitle = new System.Windows.Forms.Label();
            this.lblShiftNoteText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivities)).BeginInit();
            this.SuspendLayout();
            // 
            // ucBanLamViecLeTan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9.5F, 21.5F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "ucBanLamViecLeTan";
            this.Size = new System.Drawing.Size(1200, 800);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivities)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblDatePill;
        private System.Windows.Forms.TableLayoutPanel tblQuickActions;
        private System.Windows.Forms.TableLayoutPanel tblMainColumns;
        private System.Windows.Forms.Panel cardActivities;
        private System.Windows.Forms.Label lblActivitiesTitle;
        private System.Windows.Forms.Label lblActivitiesUpdated;
        private System.Windows.Forms.DataGridView dgvActivities;
        private System.Windows.Forms.FlowLayoutPanel flowActivityButtons;
        private System.Windows.Forms.Button btnBottomTaoDat;
        private System.Windows.Forms.Button btnBottomNhanPhong;
        private System.Windows.Forms.Button btnBottomTraPhong;
        private System.Windows.Forms.Panel cardAlerts;
        private System.Windows.Forms.Label lblAlertsTitle;
        private System.Windows.Forms.Label lblAlertsBadge;
        private System.Windows.Forms.Panel pnlAlertOverdue;
        private System.Windows.Forms.Panel pnlAlertCleaning;
        private System.Windows.Forms.Panel pnlAlertMaintenance;
        private System.Windows.Forms.Panel cardShiftNote;
        private System.Windows.Forms.Label lblShiftNoteTitle;
        private System.Windows.Forms.Label lblShiftNoteText;
        private System.Windows.Forms.Button btnUpdateNote;
    }
}
