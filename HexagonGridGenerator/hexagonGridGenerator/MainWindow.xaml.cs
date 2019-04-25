using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hexagonGridGenerator
{
    public partial class MainWindow : Window
    {
        int rows = 0;
        int columns = 0;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //check text boxes have a number inside and not a string then store that number 
            if(checkTextBoxes())
            {
                //clear the canvas first before adding new hexagons
                if (canvasHexagonGrid.Children.Count > 0)
                {
                    canvasHexagonGrid.Children.Clear();
                }

                //add the hexagon grid to the canvas 
                generateHexagonGrid();
            }
        }

        //produce the hexagon grid based on the values of rows and columns 
        private void generateHexagonGrid()
        {
            //position of each hexagon on the canvas 
            double left = 0;
            double top = 0;

            //resizes the canvas based on the number of hexagons that were added 
            if (!(rows == 0) || !(columns == 0))
            {
                if(columns == 1)
                {
                    canvasHexagonGrid.Height = rows*50;
                    canvasHexagonGrid.Width = 50;
                }
                else
                {
                    canvasHexagonGrid.Height = (rows * 50) + 25; //offset of 25 added
                    //50 is the height of the hexagon

                    canvasHexagonGrid.Width = (columns * 37.5) + 12.5; //offset of 12.5 added
                    //37.5 is how much the left value of the hexagon is increased by when a new column of hexagons is introducted to the canvas
                }
            }

            //loop for adding hexagons to the canvas  
            for (int x=0; x < columns; x++)
            {
                for(int y = 0; y < rows; y++)
                {
                    addHexagon(left, top);
                    top = top + 50;
                    //adds hexagons going down the column
                    //adds hexagons without changing the x coordinate but the y coordinate does change
                }
                //sets the left value
                left = left + 37.5;
                if(x%2 == 0)
                {
                    //with a hexagon grid if the x coordinate is odd or even then a offset needs to be added to the top
                    //this makes sure all hexagons are aligned correctly 
                    top = 25;
                }
                else
                {
                    top = 0;
                }
                //the x coordinate has been changed and the hexagons will be added again going down the column
            }


        }

        //adds a new hexagon to the canvas
        //it takes in a left value and top value which are used to set the postion of the hexagon
        public void addHexagon(double left, double top)
        {
            Polygon hexagon = new Polygon();
            Canvas.SetLeft(hexagon, left);
            Canvas.SetTop(hexagon, top);
            
            hexagon.Stroke = Brushes.Black;
            hexagon.Width = 50;
            hexagon.Height = 50;
            hexagon.Fill = Brushes.Red;
            hexagon.StrokeThickness = 1;
            PointCollection myPointCollection = new PointCollection();
            myPointCollection.Add(new System.Windows.Point(50, 25));
            myPointCollection.Add(new System.Windows.Point(37.5, 50));
            myPointCollection.Add(new System.Windows.Point(12.5, 50));
            myPointCollection.Add(new System.Windows.Point(0, 25));
            myPointCollection.Add(new System.Windows.Point(12.5, 0));
            myPointCollection.Add(new System.Windows.Point(37.5, 0));
            hexagon.Points = myPointCollection;

            canvasHexagonGrid.Children.Add(hexagon);
        }


        //simple check just to see if both rows and columns text boxes contain numbers and not text
        //it will also set the rows and columns values whilst checking if they are numbers 
        private bool checkTextBoxes()
        {
            if(!int.TryParse(rowsTxt.Text, out rows) && !int.TryParse(columnTxt.Text, out columns))
            {
                errorTxt.Text = "type a number for rows and columns";
                return false;
            }
            else
            {
                if (!int.TryParse(rowsTxt.Text, out rows))
                {
                    errorTxt.Text = "type a number for rows";
                    return false;
                }
                else if (!int.TryParse(columnTxt.Text, out columns))
                {
                    errorTxt.Text = "type a number for columns";
                    return false;
                }
                else
                {
                    return true;
                }
            }           
        }
    }
}
