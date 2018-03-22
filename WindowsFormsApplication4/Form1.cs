using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Management.Instrumentation;
using System.IO;
using Microsoft.VisualBasic;
using System.Diagnostics;


namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");
            ManagementObjectSearcher searcher4 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

            ManagementScope scope = new ManagementScope("root\\cimv2");
            scope.Connect();
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
            ManagementObjectSearcher searcher3 = new ManagementObjectSearcher(scope, query);
            int Capacity = 0;
            ManagementObjectCollection queryCollection = searcher3.Get();
            foreach (ManagementObject m in queryCollection)
            {
                Capacity = Capacity + Convert.ToInt32(Math.Round(System.Convert.ToDouble(m["Capacity"]) / 1024 / 1024));
                string test = Convert.ToString(m["Manufacturer"]);
                label10.Text = "Об'єм: " + (Convert.ToString(Capacity) + " MБ");
                //MessageBox.Show(test);
            }
            //CPU
            foreach (ManagementObject queryObj in searcher1.Get())
            {
                label1.Text = string.Format("Назва: {0}", queryObj["Name"]);
                label2.Text = string.Format("Кількість ядер: {0}", queryObj["NumberOfCores"]);
                label9.Text = string.Format("Опис: {0}", queryObj["Caption"]);
            }
            //RAM
            foreach (ManagementObject queryObj in searcher3.Get())
            {
                label5.Text = string.Format("Назва: {0}", queryObj["MemoryType"]);//speed 677 + 800 //Manufacturer Kingston + Uknknow
            }
            //GPU
            foreach (ManagementObject queryObj in searcher2.Get())
            {
              //  label12.Text = string.Format("Назва: {0}", queryObj["Name"]);
                label3.Text = string.Format("Назва: {0}", queryObj["Caption"]);
                label4.Text = string.Format("Сімейство: {0}", queryObj["VideoProcessor"]);
                label11.Text = string.Format("Об'єм: {0}", Convert.ToInt32(queryObj["AdapterRAM"] + " Мегабайт") );
             //   label11.Text = string.Format("Об'єм: {0}", Convert.ToInt32(queryObj["AdapterRAM"] + " Мегабайт"));
               // label11.Text = Convert.ToInt32(Math.Round(System.Convert.ToDouble(m["Capacity"]) / 1024 / 1024));
            }

            //label10.Text = ("Об'єм: "+Capacity.ToString()+" Мегабайт"); // ответ в Мб 
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            PerformanceCounter ramFree = new PerformanceCounter("Memory", "Available MBytes");
            label7.Text = "Незадіяне ОЗП: " + string.Format(ramFree.NextValue() + " MБ");
            label6.Text = "Процент задіяння ЦП: " + performanceCounter1.NextValue().ToString("0") + "%";
            label8.Text = "Процент задіяння ОЗП: " + performanceCounter2.NextValue().ToString("0") + "%";
        }
    }
}



//осталось название 1 и 2 озу, их скорость
//~тип
//Обьем видеокарты