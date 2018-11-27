using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleForms
{
    //Enum for statuses of grid.
    public enum SF_GridStatus
    {
        Empty,
        Clickable,
        OnClick,
        Unclickable
    }

    public partial class SF_GridSelection : Form
    {
        //Defining private properties.
        private object[] arguments;
        public string Title;
        public string TitleText;
        public int GridHeight;
        public int GridWidth;
        public string onClickImage = "";
        private static Dictionary<int, SF_GridItem> gridDict = new Dictionary<int, SF_GridItem>();
        public List<List<int>> gridIndex;

        //Parameters:
        //Window Title - args[0]
        //Title Text - args[1]
        //Grid Height (int) - args[2]
        //Grid Width (int) - args[3]
        //Grid integer array (string, separated by "," characters) - args[4]
        public SF_GridSelection(params object[] args)
        {
            //Checking argument length.
            if (args.Count() < 3) { throw new Exception("Missing parameters."); }

            foreach (var i in args)
            {
                Console.WriteLine(i);
            }

            //Getting title and title text.
            Title = (string)args[0];
            TitleText = (string)args[1];

            //Getting grid size by width and height.
            GridHeight = SF.Int(args[2]);
            GridWidth = SF.Int(args[3]);

            //Setting the grid integer array.
            gridIndex = getGrid(args[4], GridHeight, GridWidth);

            //Setting the argument array.
            arguments = args;
            InitializeComponent();
        }

        private void SF_GridSelection_Load(object sender, EventArgs e)
        {
            //Creating the PictureBox grid (32x32).
            int cellSize = 128;
            int rowHeight = 10;
            int columnWidth = 10;
            for (int i = 0; i < GridHeight; i++)
            {
                for (int j = 0; j < GridWidth; j++)
                {
                    //Getting cell value for the grid.
                    Console.WriteLine("i: " + i);
                    Console.WriteLine("j: "+j);
                    int cell = gridIndex[i][j];
                    //Attempting to get SF_GridItem from dictionary.
                    SF_GridItem currentGridItem;
                    if (!gridDict.TryGetValue(cell, out currentGridItem))
                    {
                        throw new Exception("Error getting key, have integer keys been set using SetKey?");
                    }

                    //Creating individual picture box.
                    PictureBox currentPicBox = new PictureBox();
                    currentPicBox.Location = new Point(columnWidth, rowHeight);
                    currentPicBox.Size = new Size(cellSize, cellSize);
                    currentPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    currentPicBox.Name = "pic" + i + "|" + j;
                    this.Controls.Add(currentPicBox);

                    //Iterating column width.
                    columnWidth += (cellSize+10);

                    //Switching through statuses.
                    switch (currentGridItem.status)
                    {
                        case SF_GridStatus.Empty:
                            break;
                        case SF_GridStatus.Clickable:
                            //Loading image.
                            currentPicBox.ImageLocation = currentGridItem.contents;
                            currentPicBox.Click += handleGridBoxClick;
                            break;
                        case SF_GridStatus.OnClick:
                            //Setting onclick image.
                            onClickImage = currentGridItem.contents;
                            break;
                        case SF_GridStatus.Unclickable:
                            break;
                        default:
                            break;
                    }
                }

                //Iterating height.
                rowHeight += (cellSize + 10);
                //Resetting width.
                columnWidth = 10;
            }
        }

        private void handleGridBoxClick(object sender, EventArgs e)
        {
            //Setting picture box image to the OnClick image, if it's been set.
            PictureBox send = (PictureBox)sender;
            if (onClickImage != "")
            {
                send.ImageLocation = onClickImage;
            }

            //Setting the grid item's status.
            for (int k = 0; k < gridIndex.Count; k++)
            {
                //Getting indexes.
                int index1 = int.Parse(send.Name.Substring(3, 1));
                int index2 = int.Parse(send.Name.Substring(5, 1));
                Console.WriteLine(index1 + "|" + index2);

                //Setting item based on OnClick key.
                gridIndex[index1][index2] = gridDict.First(x => x.Value.status == SF_GridStatus.OnClick).Key;
            }
        }

        //To set the dictionary for the datagrid.
        public static void SetKey(int index, SF_GridStatus status, string path = "")
        {
            gridDict.Add(index, new SF_GridItem(status, path));
        }

        //Public method to get grid list (integer).
        public static List<List<int>> getGrid(object o, int height, int width)
        {
            List<int> non2DGrid = ((string)o).Split(',').Select(Int32.Parse).ToList();

            //Converting to 2D grid.
            List<int> tempRow = new List<int>();
            List<List<int>> grid2D = new List<List<int>>();
            for (int i=0; i<non2DGrid.Count; i++)
            {
                if ((i+1)%width==0)
                {
                    //End of row, add and push.
                    tempRow.Add(non2DGrid[i]);
                    grid2D.Add(tempRow);
                    tempRow = new List<int>();
                } else
                {
                    //Adding to row.
                    tempRow.Add(non2DGrid[i]);
                }
            }
            //Adding final row.
            grid2D.Add(tempRow);

            //Returning.
            return grid2D;
        }
    }

    //Basic grid item container class.
    public class SF_GridItem
    {
        //Constructor setting public properties.
        public SF_GridItem(SF_GridStatus status_, string contents_ = "")
        {
            status = status_;
            contents = contents_;
        }

        public SF_GridStatus status;
        public string contents;
    }
}
