namespace PrimeNumbers
{
    public partial class PrimeNumbersApp : Form
    {
        PrimeNumbers primeNumbers = new PrimeNumbers();
        public PrimeNumbersApp()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            primeNumbers.Start();
            StartButton.Enabled = false;
            StopButton.Enabled = true;
            GetButton.Enabled = false;
        }
        private void StopButton_Click(object sender, EventArgs e)
        {
            primeNumbers.Stop();
            StopButton.Enabled = false;
            StartButton.Enabled = true;
            GetButton.Enabled = true;
        }

        private void GetButton_Click(object sender, EventArgs e)
        {
            if (primeNumbers.GetValues() != null)
                foreach (string value in primeNumbers.GetValues())
                    PrimeList.Items.Add(value);
        }
    }
}