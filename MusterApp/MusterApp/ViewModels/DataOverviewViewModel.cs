using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusterApp.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    using MusterApp.Annotations;
    using MusterApp.Models;

    using ServiceReportWizard.Utility;

    /// <summary>
    /// The data overview view model.
    /// </summary>
    public class DataOverviewViewModel : ViewModelBase
    {
        /// <summary>
        /// The logging.
        /// </summary>
        private ObservableCollection<logging> logging;

        /// <summary>
        /// The overview button.
        /// </summary>
        private ICommand overviewButton;

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The is overview visible.
        /// </summary>
        private bool isOverviewVisible = true;

        /// <summary>
        /// The is config visible.
        /// </summary>
        private bool isConfigVisible;

        /// <summary>
        /// The is invoice visible.
        /// </summary>
        private bool isInvoiceVisible;

        /// <summary>
        /// The context.
        /// </summary>
        private MusterDbContext context;

        /// <summary>
        /// The pods.
        /// </summary>
        private ObservableCollection<pod> pods;

        /// <summary>
        /// The invoice command.
        /// </summary>
        private ICommand invoiceCommand;

        /// <summary>
        /// The generate config command.
        /// </summary>
        private ICommand generateConfigCommand;

        /// <summary>
        /// The config.
        /// </summary>
        private string config;

        private ICommand configButton;

        private ICommand invoiceButton;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataOverviewViewModel"/> class.
        /// </summary>
        public DataOverviewViewModel()
        {
            this.context = new MusterDbContext();
        }

        /// <summary>
        /// Gets or sets a value indicating whether is overview visible.
        /// </summary>
        public bool IsOverviewVisible
        {
            get
            {
                return this.isOverviewVisible;
            }

            set
            {
                this.SetProperty(ref this.isOverviewVisible, value, () => this.IsOverviewVisible);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is config visible.
        /// </summary>
        public bool IsConfigVisible
        {
            get
            {
                return this.isConfigVisible;
            }

            set
            {
                this.SetProperty(ref this.isConfigVisible, value, () => this.IsConfigVisible);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is invoice visible.
        /// </summary>
        public bool IsInvoiceVisible
        {
            get
            {
                return this.isInvoiceVisible;
            }

            set
            {
                this.SetProperty(ref this.isInvoiceVisible, value, () => this.IsInvoiceVisible);
            }
        }

        /// <summary>
        /// Gets the overview button.
        /// </summary>
        public ICommand OverviewButton
        {
            get
            {
                return this.overviewButton ?? (this.overviewButton = new RelayCommand(this.ExecuteOverview));
            }
        }

        public ICommand ConfigButton
        {
            get
            {
                return this.configButton ?? (this.configButton = new RelayCommand(this.ExecuteConfig));
            }
        }



        public ICommand InvoiceButton
        {
            get
            {
                return this.invoiceButton ?? (this.invoiceButton = new RelayCommand(this.ExecuteInvoiceView));
            }
        }

 

        /// <summary>
        /// Gets the overview button.
        /// </summary>
        public ICommand InvoiceCommand
        {
            get
            {
                return this.invoiceCommand ?? (this.invoiceCommand = new RelayCommand(this.ExecuteInvoice));
            }
        }

        /// <summary>
        /// Gets the generate config command.
        /// </summary>
        public ICommand GenerateConfigCommand
        {
            get
            {
                return this.generateConfigCommand ?? (this.generateConfigCommand = new RelayCommand(this.ExecuteGenerateConfig));
            }
        }

        /// <summary>
        /// The execute generate config.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        private void ExecuteGenerateConfig(object obj)
        {
            var device = obj as device;
            if (device == null)
            {
                return;
            }

            var netzwerkInterface = device.netzwerkinterface.ToList();
            var vlansList = device.netzwerkinterface.Select(v => v.vlan);
            var vlans = new List<vlan>();
            foreach (var vilan in vlansList)
            {
                foreach(var vlan in vilan)
                {
                    vlans.Add(vlan);
                }
            }

            string vlanConfig = string.Empty;
            foreach (var vlan in vlans)
            {
                vlanConfig += "vlan " + vlan.id_vlan;
                vlanConfig += "\n\t";
                vlanConfig += "name " + vlan.bezeichnung;
                vlanConfig += "\n\n";
            }

            var nwifsConfig =string.Empty;
            foreach (var netzwerkif in netzwerkInterface)
            {
                var vlan = netzwerkif.vlan;
                foreach(var tempVlan in vlan)
                {
                    nwifsConfig += "interface vlan " + tempVlan.bezeichnung;
                    nwifsConfig += "\n\t";
                    nwifsConfig +=  ""+ netzwerkif.name;
                }
            }

            var credentials = device.administrativ_credentials_snmp_comunity.Select(a => a.administrativ_credentials);
            var userName = credentials.Select(c => c.benutzer);
            var pwd = credentials.Select(c => c.passwort);

            this.Config = "test \n test";
        }

        /// <summary>
        /// The execute invoice.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        private void ExecuteInvoice(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The execute invoice view.
        /// </summary>
        private void ExecuteInvoiceView()
        {
            this.IsConfigVisible = false;
            this.IsInvoiceVisible = true;
            this.IsOverviewVisible = false;
        }

        /// <summary>
        /// The execute config.
        /// </summary>
        private void ExecuteConfig()
        {
            this.IsConfigVisible = true;
            this.IsInvoiceVisible = false;
            this.IsOverviewVisible = false;
        }

        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        public string Config
        {
            get
            {
                if(this.config == null)
                {
                    return string.Empty;
                }
                return this.config;
            }

            set
            {
                if (value != null)
                {
                    this.config = value;
                    var onPropertyChanged = this.PropertyChanged;
                    if (onPropertyChanged != null)
                    {
                        onPropertyChanged(this, new PropertyChangedEventArgs("Config"));
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the logging.
        /// </summary>
        public ObservableCollection<logging> Logging
        {
            get
            {
                //using (var db = new MusterDbContext())
                //{
                //    db.logging.Load();
                //    var query = db.logging.ToList();
                //    //var observablecollection = new ObservableCollection<logging>();
                //    //query.ForEach(q => observablecollection.Add(q));
                //    this.logging = new ObservableCollection<logging>(query);
                //    return this.logging;
                //}
                var query = this.context.logging.ToList();
                this.logging = new ObservableCollection<logging>(query);
                return this.logging;
            }

            set
            {
                if (value != null)
                {
                    this.logging = value;
                    var onPropertyChanged = this.PropertyChanged;
                    if (onPropertyChanged != null)
                    {
                        onPropertyChanged(this, new PropertyChangedEventArgs("Logging"));
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the logging.
        /// </summary>
        public ObservableCollection<pod> Pods
        {
            get
            {
                //using (var db = new MusterDbContext())
                //{
                //    db.logging.Load();
                //    var query = db.logging.ToList();
                //    //var observablecollection = new ObservableCollection<logging>();
                //    //query.ForEach(q => observablecollection.Add(q));
                //    this.logging = new ObservableCollection<logging>(query);
                //    return this.logging;
                //}
                var query = this.context.pod.ToList();
                this.pods = new ObservableCollection<pod>(query);
                return this.pods;
            }

            set
            {
                if (value != null)
                {
                    this.pods = value;
                    var onPropertyChanged = this.PropertyChanged;
                    if (onPropertyChanged != null)
                    {
                        onPropertyChanged(this, new PropertyChangedEventArgs("Pods"));
                    }
                }
            }
        }

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// The execute overview.
        /// </summary>
        private void ExecuteOverview()
        {
            this.IsConfigVisible = false;
            this.IsInvoiceVisible = false;
            this.IsOverviewVisible = true;
        }
    }
}
