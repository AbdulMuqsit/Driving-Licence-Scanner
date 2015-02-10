using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using DrivingLicenceScanner.Entities;
using DrivingLicenceScanner.EntityFramework;
using DrivingLicenceScanner.Infrastructure;
using DrivingLicenceScanner.Model;

namespace DrivingLicenceScanner.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Fields

        private LegalAge _legalAge;
        private ObservableCollection<LegalAge> _legalAges;

        #endregion

        #region Properties

        public LegalAge LegalAge
        {
            get { return _legalAge; }
            set
            {
                if (Equals(value, _legalAge)) return;
                _legalAge = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LegalAge> LegalAges
        {
            get { return _legalAges; }
            set
            {
                if (Equals(value, _legalAges)) return;
                _legalAges = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public RelayCommand AddLegalAgeCommand { get; set; }
        public RelayCommand RemoveLegalAgeCommand { get; set; }
        public RelayCommand LoadLegalAgesCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }

        #endregion

        #endregion

        #region Methods

        public SettingsViewModel()
        {
            AddLegalAgeCommand = new RelayCommand(AddLegalAge,
                () => LegalAge != null && !String.IsNullOrWhiteSpace(LegalAge.Name) && LegalAge.Age > 0);
            RemoveLegalAgeCommand = new RelayCommand(RemoveLegalAge, () => LegalAge != null && LegalAge.Id != 0);
            LoadLegalAgesCommand = new RelayCommand(LoadLegalAges);
            ClearCommand = new RelayCommand(() => LegalAge = new LegalAge {Age = 18});
            LoadLegalAges();
        }

        private async void RemoveLegalAge()
        {
            BusyMessage = "Saving Changes...";
            BusyState = BusyState.Busy;
            await Task.Run(async () =>
            {
                using (DrivingLicenceScannerDbContext context = Context)
                {
                    DbEntityEntry entry = context.Entry(LegalAge);
                    if (entry.State == EntityState.Detached)
                    {
                        context.LegalAges.Attach(LegalAge);
                    }
                    context.LegalAges.Remove(LegalAge);

                    await context.SaveChangesAsync();
                    LoadLegalAges();
                }
            });
            BusyState = BusyState.Free;
        }

        private async void AddLegalAge()
        {
            BusyMessage = "Saving Changes...";
            BusyState = BusyState.Busy;
            using (DrivingLicenceScannerDbContext context = Context)
            {
                if (LegalAge.Id == 0)
                {
                    context.LegalAges.Add(LegalAge);
                }
                else
                {
                    DbEntityEntry entry = context.Entry(LegalAge);
                    if (entry.State == EntityState.Detached)
                    {
                        context.LegalAges.Attach(LegalAge);
                        entry.State = EntityState.Modified;
                    }
                }
                await context.SaveChangesAsync();

                LoadLegalAges();
                LegalAge = new LegalAge {Age = 18};
            }

            BusyState = BusyState.Free;
        }

        private async void LoadLegalAges()
        {
            BusyMessage = "Loading Legal Ages...";
            BusyState = BusyState.Busy;
            await Task.Run(async () =>
            {
                LegalAges = new ObservableCollection<LegalAge>(await Context.LegalAges.ToListAsync());
                using (DrivingLicenceScannerDbContext context = Context)
                {
                    if (LegalAges.Count == 0)
                    {
                        var legalAges = new List<LegalAge>();

                        legalAges.Add(new LegalAge {Age = 18, Name = "Cigarettes"});
                        legalAges.Add(new LegalAge {Age = 21, Name = "Alcohol"});
                        legalAges.Add(new LegalAge {Age = 17, Name = "Lottery"});

                        context.LegalAges.AddRange(legalAges);
                        await context.SaveChangesAsync();

                        //using the Context (capital C) object because local context gets destroyed
                        LegalAges = new ObservableCollection<LegalAge>(await Context.LegalAges.ToListAsync());
                    }
                }
            });

            BusyState = BusyState.Free;
        }

        #endregion
    }
}