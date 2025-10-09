using Trivial.Repository;

namespace Trivial.Views;

public partial class CapitalsPage : ContentPage
{
	private Random rn = new Random();
	private string[] currentAnswers;
	private int correctAnswers = 0;
	private int incorrectAnswers = 0;

	public CapitalsPage()
	{
		InitializeComponent();
		SetAnswers();
	}

	/// <summary>
	/// Get the button of the answer clicked and chek if is was the correct answer or not.
	/// Then notify the user.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private async void OnClickAnswer(object sender, EventArgs e)
	{
		Button btn = (Button)sender;
		if (btn.Text == currentAnswers[1])
		{
			await DisplayAlert("¡Correcto!", $"La capital de {currentAnswers[0]} es {currentAnswers[1]}", "Continuar");
			correctAnswers++;
		}
		else
		{
			await DisplayAlert("¡Incorrecto!", $"La capital de {currentAnswers[0]} es {currentAnswers[1]}", "Continuar");
			incorrectAnswers++;
		}

		if (correctAnswers + incorrectAnswers == 10)
		{
			await DisplayAlert("Partida Finalizada",
				$"Has tenido un total de {correctAnswers} aciertos y de {incorrectAnswers} fallos", "Terminar");
			await Shell.Current.GoToAsync(".."); //When finish the game, return to the main
        }
		else SetAnswers(); //Update the question and answers
	}

	/// <summary>
	/// Generate randoms answers where one of them is correct with the showed country.
	/// </summary>
	/// <returns>string[] of answers</returns>
	private string[] LoadAnswers()
	{
		string[] answers = new string[5]; //The first element contains the country, the rest contains the capitals. 
		int[] answersIndex = new int[4]; //Contains random index of the array of capitals.
		int newAnswer;
		answersIndex[0] = rn.Next(0, CapitalsAndCountrysRepository.CapitalsAndCountrysLength + 1);
		//The first element contains the index of the capital and its country.

		for(int i = 1; i < answersIndex.Length; i++) //Start at 1, because the index 0 is the correct answer
		{
			newAnswer = 0;
			do
			{
				newAnswer = rn.Next(0, CapitalsAndCountrysRepository.CapitalsAndCountrysLength + 1);
			} while (answersIndex.Contains(newAnswer)); //Prevent to have the same capital more times.
			answersIndex[i] = newAnswer;
        }

		int index = 1; //Index to add the capitals to the final answers 
		answers[0] = CapitalsAndCountrysRepository.Countrys[answersIndex[0]]; //The first element is the country

		foreach(int i in answersIndex)
		{
			answers[index] = CapitalsAndCountrysRepository.Capitals[i]; //Get the capital of the random index generated
			index++;
		}
		return answers;
	}

    /// <summary>
    /// Set the answers randomly for the buttons and the question. Then update the answers into the global array.
    /// </summary>
    private void SetAnswers()
	{
		string[] answers = LoadAnswers();
		currentAnswers = answers; //Update the global array of answers
		int[] seatedAnswers = new int[4];
		int randomIndex; //Random index to set randomly the answers into the buttons
        txlQuestion.Text = $"¿Cual es la capital de {answers[0]}?";
		for(int i = 0; i < seatedAnswers.Length; i++)
		{
			randomIndex = 0;
			do
			{
				randomIndex = rn.Next(1, answers.Length);
			} while (seatedAnswers.Contains(randomIndex)); //Prevents to hace the same country more times
            seatedAnswers[i] = randomIndex;
		}
		btnOne.Text = answers[seatedAnswers[0]];
		btnTwo.Text = answers[seatedAnswers[1]];
		btnThree.Text = answers[seatedAnswers[2]];
		btnFour.Text = answers[seatedAnswers[3]];
	}
}