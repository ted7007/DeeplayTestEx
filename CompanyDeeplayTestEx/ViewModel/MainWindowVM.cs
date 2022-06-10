using Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CompanyDeeplayTestEx.View;
using Repository.Repositories;
using Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace CompanyDeeplayTestEx.ViewModel
{
    class MainWindowVM
    {
        #region fields

        private ButtonCommand addCommand;

        private ButtonCommand removeCommand;

        private ButtonCommand editCommand;

        private BindingList<Worker> workers;

        private CompanyRepository db;

        private ButtonCommand sortCommand;

        #endregion

        #region properties

        public ButtonCommand AddCommand { get { return addCommand ?? (addCommand = new ButtonCommand(o => Add())); } }

        public Worker SelectedWorker { get; set; } 
        public ButtonCommand EditCommand { get { return editCommand ?? (editCommand = new ButtonCommand(o => Edit())); } }

        public ButtonCommand RemoveCommand { get { return removeCommand ?? (removeCommand = new ButtonCommand(sender => Remove())); } }

        public ButtonCommand SortCommand { get { return sortCommand ?? (sortCommand = new ButtonCommand(sender => Sort())); } }

        public BindingList<Worker> Workers { get { return workers; }}

        public ComboBoxItem SortCompany { get; set; }

        public ComboBoxItem SortPost { get; set; }
        

        #endregion

        public MainWindowVM(CompanyContext context)
        {


            db = new CompanyRepository(context);
            workers = db.GetAllWorkers();
        }

        /// <summary>
        /// Метод добавления сотрудника 
        /// </summary>
        public void Add()
        {
            AddWindow window = new AddWindow();
            AddWindowVM context = new AddWindowVM(db.GetAllPosts().ToList(), db.GetAllCompanies().ToList());
            window.DataContext = context;
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                var data = window.DataContext as AddWindowVM;
                if (data.Name == string.Empty)
                {
                    MessageBox.Show("name is empty", "Error");
                    return;
                }
                Worker worker = new Worker { Name = data.Name, Birthday = data.Birthday, PostId = data.Post.Id, CompanyId = data.Company.Id };
                //int? postId = db.FirstPost(p => p.Name == data.Post).Id;
               // int? companyId = db.GetCompany(c => c.Name == (string)data.CompanyName.DataContext).Id;
                Sex sex = (string)data.Sex.Content == "Male" ? Sex.Male : Sex.Female;
              //  worker.CompanyId = companyId;
               // worker.PostId = postId;
                worker.Sex = sex;
                
                var result = db.AddWorker(worker);
                setUniqueInfo(ref result);
                db.UpdateWorker(result);
            }
        }

        public void Edit()
        {
            if (SelectedWorker == null)
                return;
            AddWindow window = new AddWindow();
            window.DataContext = new EditWindowVM(db.GetAllPosts().ToList(), db.GetAllCompanies().ToList(), SelectedWorker);
            window.ShowDialog();
            if(window.DialogResult == true)
            {
                EditWindowVM data = window.DataContext as EditWindowVM;
                SelectedWorker.Name = data.Name;
                SelectedWorker.PostId = data.Post.Id;
                SelectedWorker.Sex = (string)data.Sex.Content == "Male" ? Sex.Male : Sex.Female;
                SelectedWorker.CompanyId = data.Company.Id;
                var res = SelectedWorker;
                setUniqueInfo(ref res);
                SelectedWorker = res;
                Workers[Workers.IndexOf(Workers.First(w => w.Id == SelectedWorker.Id))] = SelectedWorker;
            }
        }

        public void Remove()
        {
            if (SelectedWorker == null)
                return;
            db.RemoveWorker(SelectedWorker);
        }

        private void setUniqueInfo(ref Worker worker)
        {
            if (worker.UIId != null)
                db.RemoveUI(worker.UI);
            switch (worker.PostId)
            {
                case 2:
                    var companyId = worker.CompanyId;
                    var directorName = db.GetPost(p => p.Id == 1).Workers.First(w => w.CompanyId == companyId).Name;
                    var info = new UniqueInfo() { Text = directorName, WorkerId = worker.Id };
                    worker.UIId = info.Id;
                    db.AddUniqueInfo(info);

                    break;
                case 1:
                    var companyId2 = worker.CompanyId;
                    var companyName = db.GetCompany(c => c.Id == companyId2).Name;
                    var info2 = new UniqueInfo() { Text = companyName, WorkerId = worker.Id };
                    worker.UIId = info2.Id;
                    db.AddUniqueInfo(info2);
                    break;
            }
        }

        public void Sort()
        {

        }
       
        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}
