using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ticketlibrary.Models;
using paymentterminal.Context;

namespace paymentterminal.ViewModels
{
    public class VMSelectHospitalSubscription : ObservableObject
    {
        private readonly BestTicketContext _context;

        public ObservableCollection<Hospital> Hospitals { get; set; } = new ObservableCollection<Hospital>();

        public ICommand SelectHospitalCommand { get; }
        public ICommand RetourCommand { get; }

        public VMSelectHospitalSubscription()
        {
            _context = new BestTicketContext();

            // Load all hospitals from the database
            var hospitals = _context.Hospital.ToList();
            foreach (var hospital in hospitals)
            {
                Hospitals.Add(hospital);
            }

            SelectHospitalCommand = new RelayCommand<Hospital>(SelectHospital);
            RetourCommand = new RelayCommand(Retour);
        }

        private void SelectHospital(Hospital selectedHospital)
        {
            // Navigate to the subscription tier selection page, passing the selected hospital as a parameter.
            ((MainWindow)App.Current.MainWindow)
                .NavigateTo("Views/ViewSubscriptionTierSelection.xaml", selectedHospital);
        }

        private void Retour()
        {
            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewHome.xaml");
        }
    }
}
