// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataOverviewViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The data overview view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MusterApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    using MusterApp.Annotations;

    using ServiceReportWizard.Utility;

    /// <summary>
    /// The data overview view model.
    /// </summary>
    public class DataOverviewViewModel : ViewModelBase
    {

        /// <summary>
        /// The new ln.
        /// </summary>
        private const string NewLn = "\n";

        /// <summary>
        /// The tab.
        /// </summary>
        private const string Tab = "\t";

        /// <summary>
        /// The pod bill command.
        /// </summary>
        private ICommand podBillCommand;

        /// <summary>
        /// The logging.
        /// </summary>
        private ObservableCollection<logging> logging;

        /// <summary>
        /// The selected pod.
        /// </summary>
        private pod selectedPod;

        /// <summary>
        /// The is busy.
        /// </summary>
        private bool isBusy;

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
        private MusterContext context;

        /// <summary>
        /// The pods.
        /// </summary>
        private ObservableCollection<pod> pods;

        /// <summary>
        /// The abrechnungen.
        /// </summary>
        private ObservableCollection<abrechnung> abrechnungen;

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

        /// <summary>
        /// The config button.
        /// </summary>
        private ICommand configButton;

        /// <summary>
        /// The invoice button.
        /// </summary>
        private ICommand invoiceButton;

        /// <summary>
        /// The overview button.
        /// </summary>
        private ICommand overviewButton;


        /// <summary>
        /// Initializes a new instance of the <see cref="DataOverviewViewModel"/> class.
        /// </summary>
        public DataOverviewViewModel()
        {
            this.LoadEntities();
        }

        /// <summary>
        /// The load entities.
        /// </summary>
        private void LoadEntities()
        {
            if (this.context != null)
            {
                this.context.Dispose();
            }

            this.context = new MusterContext();
            var query = this.context.pod.ToList();
            if (this.Pods == null)
            {
                this.Pods = new ObservableCollection<pod>(query);
            }

        }

        /// <summary>
        /// The obsolete value.
        /// </summary>
        private string obsoleteValue;

        /// <summary>
        /// Gets or sets the obsolete value.
        /// </summary>
        public string ObsoleteValue
        {
            get
            {
                if (this.obsoleteValue != null && this.obsoleteValue.Equals("0"))
                {
                    return "Momentan kein offener Betrag";
                }
                else if (this.obsoleteValue == null)
                {
                    return "Bitte POD auswählen";
                }

                return this.obsoleteValue + " CHF";
            }

            set
            {
                this.SetProperty(ref this.obsoleteValue, value, () => this.ObsoleteValue);
            }
        }


        /// <summary>
        /// Gets or sets the selected pod.
        /// </summary>
        public pod SelectedPod
        {
            get
            {
                return this.selectedPod;
            }

            set
            {
                if (value == null)
                {
                    this.SetProperty(ref this.selectedPod, null, () => this.SelectedPod);
                    return;
                }

                if (this.context != null)
                {
                    this.context.Dispose();
                }

                this.context = new MusterContext();
                var query = this.context.pod.FirstOrDefault(p => p.id_pod == value.id_pod);
                if (query == null)
                {
                    return;
                }

                this.SetProperty(ref this.selectedPod, query, () => this.SelectedPod);
                this.Abrechnungen = new ObservableCollection<abrechnung>(this.context.abrechnung.ToList());
                Task.Run(() => this.CalculateObsoleteValue(this.selectedPod));

            }

        }

        /// <summary>
        /// Gets or sets the abrechnungen.
        /// </summary>
        public ObservableCollection<abrechnung> Abrechnungen
        {
            get
            {
                return this.abrechnungen;
            }

            set
            {
                this.SetProperty(ref this.abrechnungen, value, () => this.Abrechnungen);
            }
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

        /// <summary>
        /// Gets the config button.
        /// </summary>
        public ICommand ConfigButton
        {
            get
            {
                return this.configButton ?? (this.configButton = new RelayCommand(this.ExecuteConfig));
            }
        }

        /// <summary>
        /// Gets the invoice button.
        /// </summary>
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
        /// Gets the generate config command.
        /// </summary>
        public ICommand ExecutePodBillCommand
        {
            get
            {
                return this.podBillCommand ?? (this.podBillCommand = new RelayCommand(this.ExecutePodBill));
            }
        }


        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        public string Config
        {
            get
            {
                if (this.config == null)
                {
                    return string.Empty;
                }

                return this.config;
            }

            set
            {
                this.SetProperty(ref this.config, value, () => this.Config);
            }
        }

        /// <summary>
        /// Gets or sets the logging.
        /// </summary>
        public ObservableCollection<logging> Logging
        {
            get
            {
                // using (var db = new MusterDbContext())
                // {
                // db.logging.Load();
                // var query = db.logging.ToList();
                // //var observablecollection = new ObservableCollection<logging>();
                // //query.ForEach(q => observablecollection.Add(q));
                // this.logging = new ObservableCollection<logging>(query);
                // return this.logging;
                // }
                var query = this.context.logging.ToList();
                this.logging = new ObservableCollection<logging>(query);
                return this.logging;
            }

            set
            {
                this.SetProperty(ref this.logging, value, () => this.Logging);

            }

        }



        /// <summary>
        /// Gets or sets the logging.
        /// </summary>
        public ObservableCollection<pod> Pods
        {
            get
            {
                return this.pods;
            }

            set
            {
                this.SetProperty(ref this.pods, value, () => this.Pods);

            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is busy.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                this.SetProperty(ref this.isBusy, value, () => this.IsBusy);
            }
        }

        /// <summary>
        /// The calculate obsolete value.
        /// </summary>
        /// <param name="choosedPod">
        /// The choosed pod.
        /// </param>
        public void CalculateObsoleteValue(pod choosedPod)
        {
            if (choosedPod == null)
            {
                return;
            }

            using (var db = new MusterContext())
            {
                var query = db.pod.FirstOrDefault(p => p.id_pod == choosedPod.id_pod);
                if (query == null)
                {
                    return;

                }

                var obsolete = query.location;

                var value = string.Empty + (from location in obsolete
                                            from position in location.position_abrechnung
                                            where position.abrechnung == null
                                            select (int)position.preis).Sum();
                this.ObsoleteValue = value;
            }
        }

        /// <summary>
        /// The refresh all.
        /// </summary>
        public void RefreshAll()
        {
            foreach (var location in this.SelectedPod.location)
            {
                this.context.Entry(location).Reload();
            }

            foreach (var abrechnung in this.SelectedPod.abrechnung)
            {
                this.context.Entry(abrechnung).Reload();
            }
        }

        /// <summary>
        /// The execute overview.
        /// </summary>
        private void ExecuteOverview()
        {
            this.LoadEntities();
            this.IsConfigVisible = false;
            this.IsInvoiceVisible = false;
            this.IsOverviewVisible = true;
        }

        /// <summary>
        /// The execute generate config.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        private void ExecuteGenerateConfig(object obj)
        {
            this.IsBusy = true;
            Task.Run(() => this.GenerateConfigAsync(obj));
        }

        /// <summary>
        /// The execute generate config.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<bool> GenerateConfigAsync(object obj)
        {
            this.IsBusy = true;
            var device = obj as device;
            if (device == null)
            {
                return false;
            }

            var netzwerkInterface = device.netzwerkinterface.ToList();
            var vlansList = device.netzwerkinterface.Select(v => v.vlan);
            var vlans = vlansList.SelectMany(vilan => vilan).ToList();

            var vlanConfig = string.Empty;
            foreach (var vlan in vlans)
            {
                vlanConfig += "vlan " + vlan.id_vlan;
                vlanConfig += NewLn + Tab;
                vlanConfig += "name " + vlan.bezeichnung;
                vlanConfig += NewLn + NewLn;
            }

            var nwifsConfig = string.Empty;
            foreach (var netzwerkif in netzwerkInterface)
            {
                var vlan = netzwerkif.vlan;
                foreach (var tempVlan in vlan)
                {
                    nwifsConfig += "interface vlan " + tempVlan.bezeichnung;
                    nwifsConfig += NewLn + Tab;
                    nwifsConfig += "nameif " + netzwerkif.name;
                    nwifsConfig += NewLn + Tab;
                    nwifsConfig += "ip adress " + tempVlan.net_adress;
                    nwifsConfig += "  " + tempVlan.subnetmask;
                    nwifsConfig += NewLn + Tab;
                    nwifsConfig += "ip default-gateway " + tempVlan.standart_gateway;
                    nwifsConfig += NewLn + NewLn;
                }
            }

            var credentials = device.administrativ_credentials_snmp_comunity.Select(a => a.administrativ_credentials);
            var userConfig = string.Empty;
            foreach (var credential in credentials)
            {
                userConfig += "user name " + credential.benutzer;
                userConfig += NewLn + Tab;
                userConfig += "privilege 10";
                userConfig += NewLn + Tab;
                userConfig += "password " + credential.passwort;
                userConfig += NewLn + NewLn;
            }

            var location = device.location;
            var pod = location.pod;

            var podConfig = "ip domain-name " + pod.domain;
            podConfig += NewLn;
            podConfig += "ip name-server" + pod.nameserver;
            podConfig += NewLn;
            podConfig += "clock timezone " + pod.zeitzone;
            podConfig += NewLn;
            podConfig += "sntp server " + pod.SNTP_ADDRESS;

            this.Config = vlanConfig + nwifsConfig + userConfig + podConfig;
            this.IsBusy = false;
            return true;
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
            if (this.SelectedPod == null)
            {
                MessageBox.Show("Please select a POD first");
                return;
            }

            if (this.context != null)
            {
                this.context.Dispose();
            }

            this.IsBusy = true;

            this.context = new MusterContext();
            var query = this.context.pod.FirstOrDefault(p => p.id_pod == this.SelectedPod.id_pod);
            if (query == null)
            {
                return;
            }

            var list = new ObservableCollection<abrechnung>(query.abrechnung.ToList());
            this.Abrechnungen = list;
            this.IsBusy = false;

            this.IsConfigVisible = false;
            this.IsInvoiceVisible = true;
            this.IsOverviewVisible = false;
        }

        /// <summary>
        /// The execute invoice view.
        /// </summary>
        private void ExecutePodBill()
        {
            if (this.SelectedPod == null)
            {
                return;
            }

            var result = this.context.Database.ExecuteSqlCommand("CALL PodBill({0})", this.SelectedPod.id_pod);
            Task.Run(() => this.CalculateObsoleteValue(this.SelectedPod));
        }

        /// <summary>
        /// The execute config.
        /// </summary>
        private void ExecuteConfig()
        {
            if (this.SelectedPod == null)
            {
                MessageBox.Show("Please select a pod first");
                return;
            }

            if (this.context != null)
            {
                this.context.Dispose();
            }

            this.context = new MusterContext();
            if (this.SelectedPod == null)
            {
                this.SelectedPod = this.context.pod.FirstOrDefault(p => p.id_pod == this.SelectedPod.id_pod);
            }

            this.IsConfigVisible = true;
            this.IsInvoiceVisible = false;
            this.IsOverviewVisible = false;
        }
    }
}
