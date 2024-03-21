using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    abstract class Vehicle
    {
        protected double price;
        protected double speed;
        protected int year;

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return price;
                    case 1:
                        return speed;
                    case 2:
                        return year;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        price = value;
                        break;
                    case 1:
                        speed = value;
                        break;
                    case 2:
                        year = (int)value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public abstract void DisplayInfo();
    }

    class Plane : Vehicle
    {
        private double altitude;

        public double Altitude
        {
            get { return altitude; }
            set { altitude = value; }
        }

        public override void DisplayInfo()
        {
            MessageBox.Show($"Plane - Price: {price}, Speed: {speed}, Year: {year}, Altitude: {altitude}");
        }
    }

    class Car : Vehicle
    {
        public override void DisplayInfo()
        {
            MessageBox.Show($"Car - Price: {price}, Speed: {speed}, Year: {year}");
        }
    }

    class Ship : Vehicle
    {
        private int numberOfPassengers;
        private string portOfRegistry;

        public int NumberOfPassengers
        {
            get { return numberOfPassengers; }
            set { numberOfPassengers = value; }
        }

        public string PortOfRegistry
        {
            get { return portOfRegistry; }
            set { portOfRegistry = value; }
        }

        public override void DisplayInfo()
        {
            MessageBox.Show($"Ship - Price: {price}, Speed: {speed}, Year: {year}, Passengers: {numberOfPassengers}, Port: {portOfRegistry}");
        }
    }

    public partial class Form1 : Form
    {
        private TextBox txtPrice;
        private TextBox txtSpeed;
        private TextBox txtYear;
        private TextBox txtAltitude;
        private TextBox txtPassengers;
        private TextBox txtPort;
        private RadioButton rbPlane;
        private RadioButton rbCar;
        private RadioButton rbShip;
        private Button btnShowInfo;

        public Form1()
        {
            InitializeComponent();

            txtPrice = new TextBox();
            txtPrice.Location = new System.Drawing.Point(120, 20);
            txtPrice.Size = new System.Drawing.Size(100, 20);
            this.Controls.Add(txtPrice);

            txtSpeed = new TextBox();
            txtSpeed.Location = new System.Drawing.Point(120, 50);
            txtSpeed.Size = new System.Drawing.Size(100, 20);
            this.Controls.Add(txtSpeed);

            txtYear = new TextBox();
            txtYear.Location = new System.Drawing.Point(120, 80);
            txtYear.Size = new System.Drawing.Size(100, 20);
            this.Controls.Add(txtYear);

            txtAltitude = new TextBox();
            txtAltitude.Location = new System.Drawing.Point(120, 110);
            txtAltitude.Size = new System.Drawing.Size(100, 20);
            this.Controls.Add(txtAltitude);

            txtPassengers = new TextBox();
            txtPassengers.Location = new System.Drawing.Point(120, 140);
            txtPassengers.Size = new System.Drawing.Size(100, 20);
            this.Controls.Add(txtPassengers);

            txtPort = new TextBox();
            txtPort.Location = new System.Drawing.Point(120, 170);
            txtPort.Size = new System.Drawing.Size(100, 20);
            this.Controls.Add(txtPort);

            rbPlane = new RadioButton();
            rbPlane.Text = "Plane";
            rbPlane.Location = new System.Drawing.Point(20, 200);
            rbPlane.CheckedChanged += rb_CheckedChanged;
            this.Controls.Add(rbPlane);

            rbCar = new RadioButton();
            rbCar.Text = "Car";
            rbCar.Location = new System.Drawing.Point(90, 200);
            rbCar.CheckedChanged += rb_CheckedChanged;
            this.Controls.Add(rbCar);

            rbShip = new RadioButton();
            rbShip.Text = "Ship";
            rbShip.Location = new System.Drawing.Point(160, 200);
            rbShip.CheckedChanged += rb_CheckedChanged;
            this.Controls.Add(rbShip);

            btnShowInfo = new Button();
            btnShowInfo.Text = "Show Info";
            btnShowInfo.Location = new System.Drawing.Point(90, 230);
            btnShowInfo.Click += btnShowInfo_Click;
            this.Controls.Add(btnShowInfo);
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                if (rb == rbPlane)
                {
                    txtAltitude.Enabled = true;
                    txtPassengers.Enabled = false;
                    txtPort.Enabled = false;
                }
                else if (rb == rbCar)
                {
                    txtAltitude.Enabled = false;
                    txtPassengers.Enabled = false;
                    txtPort.Enabled = false;
                }
                else if (rb == rbShip)
                {
                    txtAltitude.Enabled = false;
                    txtPassengers.Enabled = true;
                    txtPort.Enabled = true;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnShowInfo_Click(object sender, EventArgs e)
        {
            Vehicle vehicle;
            if (rbPlane.Checked)
            {
                vehicle = new Plane();
                (vehicle as Plane).Altitude = Convert.ToDouble(txtAltitude.Text);
            }
            else if (rbCar.Checked)
            {
                vehicle = new Car();
            }
            else
            {
                vehicle = new Ship();
                (vehicle as Ship).NumberOfPassengers = Convert.ToInt32(txtPassengers.Text);
                (vehicle as Ship).PortOfRegistry = txtPort.Text;
            }

            vehicle[0] = Convert.ToDouble(txtPrice.Text);
            vehicle[1] = Convert.ToDouble(txtSpeed.Text);
            vehicle[2] = Convert.ToInt32(txtYear.Text);

            vehicle.DisplayInfo();
        }
    }
}
