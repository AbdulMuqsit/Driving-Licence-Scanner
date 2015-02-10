using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicenceScanner.Entities;
using DrivingLicenceScanner.Infrastructure;

namespace DrivingLicenceScanner.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private ObservableCollection<LegalAge> _legalAges;
        private LegalAge _legalAge;

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

        public RelayCommand AddLegalAgeCommand { get; set; }
        public RelayCommand RemoveLegalAgeCommand { get; set; }

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
        public RelayCommand LoadLegalAgesCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }


        public SettingsViewModel()
        {
            AddLegalAgeCommand = new RelayCommand(AddLegalAge, () => LegalAge != null && !String.IsNullOrWhiteSpace(LegalAge.Name) && LegalAge.Age > 0);
            RemoveLegalAgeCommand = new RelayCommand(RemoveLegalAge, () => LegalAge != null && LegalAge.Id != 0);
            LoadLegalAgesCommand = new RelayCommand(LoadLegalAges);
            ClearCommand = new RelayCommand(() => LegalAge = new LegalAge() { Age = 18 });
            LoadLegalAgesCommand.Execute(null);
        }

        private async void RemoveLegalAge()
        {
            using (var context = Context)
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
        }

        private async void AddLegalAge()
        {
            using (var context = Context)
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
                LegalAge = new LegalAge() { Age = 18 };

            }
        }

        private async void LoadLegalAges()
        {
            await Task.Run(async () =>
            {

                LegalAges = new ObservableCollection<LegalAge>(await Context.LegalAges.ToListAsync());
                using (var context = Context)
                {
                    if (LegalAges.Count == 0)
                    {
                        var legalAges = new List<LegalAge>();

                        legalAges.Add(new LegalAge() { Age = 18, Name = "Cigarettes" });
                        legalAges.Add(new LegalAge() { Age = 21, Name = "Alcohol" });
                        legalAges.Add(new LegalAge() { Age = 17, Name = "Lottery" });

                        context.LegalAges.AddRange(legalAges);
                        await context.SaveChangesAsync();

                        //using the Context (capital C) object because local context gets destroyed
                        LegalAges = new ObservableCollection<LegalAge>(await Context.LegalAges.ToListAsync());

                    }
                }

            });
        }


    }
}