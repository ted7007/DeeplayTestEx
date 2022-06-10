using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CompanyDeeplayTestEx.ViewModel
{
    class AddWindowVM
    {
        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public ComboBoxItem Sex { get; set; }

        public Post Post { get; set; }

        public Company Company { get; set; }

        public List<Post> Posts { get; set; }

        public List<Company> Companies { get; set; }


        private ButtonCommand addCommand;

        public ButtonCommand OkCommand { get { return  addCommand ?? (addCommand = new ButtonCommand(o => Ok(o))); } }

        public AddWindowVM(List<Post> posts, List<Company> companies)
        {
            Posts = posts;
            Companies = companies;
        }

        public void Ok(object sender)
        {

            byte result;
            if (string.IsNullOrEmpty(Name))
            {
                MessageBox.Show("Введите имя!");
                return;
            }
            if (Birthday == default(DateTime))
            {
                MessageBox.Show("Выберите дату!");
                return;
            }
            if (Sex is null)
            {
                MessageBox.Show("Выберите Пол!");
                return;
            }
            if (string.IsNullOrEmpty(Post.Name))
            {
                MessageBox.Show("Выберите должность!");
                return;
            }
            if(string.IsNullOrEmpty(Company.Name))
            {
                MessageBox.Show("Выберите компанию!");
                return;
            }
            Window window = sender as Window;
            window.DialogResult = true;
        }
    }
}
