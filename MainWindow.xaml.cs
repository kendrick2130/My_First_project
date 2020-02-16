
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        private Marktype[] mResults;


        /// <summary>
        /// True if it is player one turn (X) or player 2 turn (O)
        /// </summary>
        private bool mPlayerOneTurn;


        /// <summary>
        /// True if the game has ended 
        /// </summary>
        private bool mGameEnded;

        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }


        #endregion
        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>

        private void NewGame()
        {
            // Create a new blank array of free cells
            mResults = new Marktype[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = Marktype.Free;

            // Make sure Player One starts the game
            mPlayerOneTurn = true;

            //Iterate every buttin in the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Black;
            });
            // Make sure the game hasn't finished
            mGameEnded = false;




        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Start a new game on the click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button
            var button = (Button)sender;

            // Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);


            // Don't do anything if the cell already has a value in it
            if (mResults[index] != Marktype.Free)
                return;

            // set the cell value based on the player's turn
            if (mPlayerOneTurn)
                mResults[index] = Marktype.Cross;
            else
                mResults[index] = Marktype.Nought;

            // Set button text to the result
            button.Content = mPlayerOneTurn ? "x" : "o";

            //Change noughts to red
            if (mPlayerOneTurn)
                button.Foreground = Brushes.Red;


            //Toggles the players turns
            if (mPlayerOneTurn)
                mPlayerOneTurn = false;
            else
                mPlayerOneTurn = true;

           //Check for a Winner
           CheckForWinner();

          
        }
        /// <summary>
        /// Check if there is a Winner of a 3 line straight
        /// </summary>
        private void CheckForWinner()
        {
            //check for horizontal wins
            //
            // Row 0
            //            
            if (mResults[0] != Marktype.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Purple;

            }
            //check for horizontal wins
            //
            // Row 1
            //            
            if (mResults[3] != Marktype.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Purple;
            }

            //check for horizontal wins
            //
            // Row 2
            //            
            if (mResults[6] != Marktype.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Purple;
            }

            //check for vertical wins
            //
            // Column 0
            //            
            if (mResults[0] != Marktype.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Purple;
            }
            //
            // Column 1
            //            
            if (mResults[1] != Marktype.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Purple;
            }

            //
            // Column 2
            //            
            if (mResults[2] != Marktype.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Purple;
            }
            //check for diagonal wins
            //
            // TOP left Bottom right
            //            
            if (mResults[0] != Marktype.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Purple;
            }

            //
            // TOP right Bottom left
            //            
            if (mResults[2] != Marktype.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Purple;
            }

            // Check for no winner and full board

            if (mGameEnded == false && !mResults.Any(result => result == Marktype.Free))
            {
                // Game Ends
                mGameEnded = true;

                // Turn All cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {

                    button.Foreground = Brushes.Orange;
                });
            }

            
        }
    }
}
