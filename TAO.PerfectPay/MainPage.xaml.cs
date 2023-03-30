using System.Threading.Tasks;

namespace TAO.PerfectPay;

public partial class MainPage : ContentPage
{
    decimal bill;
    int tip;
    int noPersons = 1;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void txtBill_Completed(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtBill.Text)) 
        {
            await DisplayAlert("Alert", "Please enter a bill.", "OK");
        }
        if (Char.IsDigit(Convert.ToChar(txtBill.Text)))
        {
            await DisplayAlert("Alert", "Please enter a digit.", "OK");
        }
        
        bill = decimal.Parse(txtBill.Text);
        CalculateTotal();
    }

    private void CalculateTotal()
    {
        var totalTip = (bill * tip) / 100;
        var tipByPerson = (totalTip / noPersons);
        lblTipByPerson.Text = $"{tipByPerson:C}";
        var subTotal = (bill / noPersons);
        lblSubTotal.Text = $"{subTotal:C}";
        var totalByPerson = (bill + totalTip) / noPersons;
        lblTotal.Text = $"{totalByPerson:C}";

    }

    private void sldTip_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        tip = (int)sldTip.Value;
        lblTip.Text = $"Tip:{tip}%";
        CalculateTotal();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (sender is Button)
        {
            var btn = (Button)sender;
            var percentage = int.Parse(btn.Text.Replace("%", ""));
            sldTip.Value = percentage;
        }
    }

    private void btnMinus_Clicked(object sender, EventArgs e)
    {
        if (noPersons > 1)
        {
            noPersons--;
        }
        lblNoPerons.Text = noPersons.ToString();
        CalculateTotal();
    }

    private void btnPlus_Clicked(object sender, EventArgs e)
    {
        noPersons++;
        lblNoPerons.Text = noPersons.ToString();
        CalculateTotal();
    }
}

