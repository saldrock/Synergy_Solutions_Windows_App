﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;

namespace Synergy_Solutions_App
{
    public partial class StartScreen : Form
    {
        //all displayed text with a lanuage select varible
        int lanSelect = 0;
        String readyFReady = "Ready";
        String readyFGo = "Go";
        String loadingText = "Loading";

        //getting size of the screen (changes depending on screen used)
        int getScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
        int getScreenHight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

        double getScreenHightInPixels = Screen.PrimaryScreen.Bounds.Height;
        double getScreenWidthInPixels = Screen.PrimaryScreen.Bounds.Width;

        //fade for the image
        public byte alpha = 255;
        bool inOut = true;

        //thread to move into UI mode
        Thread thUI;

        //changes the alpha value of each pixel in a screen 
        private static Bitmap changeTransparacy(Image image, Byte alpha)
        {
            Bitmap inputImage = new Bitmap(image);
            Bitmap outputImage = new Bitmap(image.Width, image.Height);
            System.Drawing.Color orignalPixel = System.Drawing.Color.Black;
            System.Drawing.Color newPixel = System.Drawing.Color.Black;


            for (int w = 0; w < image.Width; w++)
            {
                for (int h = 0; h < image.Height; h++)
                {
                    orignalPixel = inputImage.GetPixel(w, h);
                    if (orignalPixel == Color.FromArgb(0, orignalPixel.R, orignalPixel.B, orignalPixel.G))
                    {
                        continue;
                    }
                    else
                    {
                        newPixel = Color.FromArgb(alpha, orignalPixel.R, orignalPixel.B, orignalPixel.G);

                        outputImage.SetPixel(w, h, newPixel);
                    }
                }

            }


            return outputImage;
        }

        //getting serial ports (used from Bernard's code, needs to be modified later)
        public void getSerialPorts()
        {
            string[] ports;
            ports = SerialPort.GetPortNames();
            
           

            try {
                gameSerial.PortName = ports[0];
                gameSerial.Open();
                gameDebugWindow.AppendText("connected to:" + gameSerial.PortName + Environment.NewLine);
            }
            catch {
                gameDebugWindow.AppendText("can't connect to serial bus" + Environment.NewLine);
            }
           
        }


        public StartScreen()
        {
            InitializeComponent();
            getSerialPorts();
        }

        private void StartScreen_Load(object sender, EventArgs e)
        {


            //set UI to screen size and put it in top corner of screen and to size of the screen
            this.ClientSize = new System.Drawing.Size(getScreenWidth, getScreenHight);
            this.Location = new Point(0, 0);

            //update UI
            manualUIUpdate();
            
        }

        //----------Starts manually moving UI elements (called in form_load)---

        //Center x-y positiins of a element (next two functions)
        private int centerElementXcor(int Xcor, int elementWidth)
        {
            Xcor = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width / 2) - (elementWidth / 2);
            return Xcor;
        }

        private int centerElementYcor(int Ycor, int elementHight)
        {
            Ycor = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height / 2) - (elementHight / 2);
            return Ycor;
        }

        //Updating the user interfance
        public void manualUIUpdate() {

            //edit to *0 to change position relative to center
            //startGame - button to start game
            int startGameMoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0.35));
            int startGameMoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_action - an image to show user what to do (used for loading image)
            int img_actionMoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_actionMoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_arrow - the fading in/out arrow
            int img_arrowMoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0.28));
            int img_arrowMoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_button - the odd looking white button under the fading arrow the goes once the startGame button is pressed
            int img_UFOMoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_UFOMoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //instruction - used for loading/ready? labels
            int instructionMoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int instructionMoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //action - place holder should be replaced with images of the instruction
            int actionMoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int actionMoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_planet01_1
            int img_planet01_1MoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_planet01_1MoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_planet01_2
            int img_planet01_2MoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_planet01_2MoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_planet02_1
            int img_planet02_1MoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_planet02_1MoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_planet02_2
            int img_planet02_2MoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_planet02_2MoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_planet02_3
            int img_planet02_3MoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_planet02_3MoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_planet03_1
            int img_planet03_1MoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_planet03_1MoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_planet03_2
            int img_planet03_2MoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_planet03_2MoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_planet04_1
            int img_planet04_1MoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_planet04_1MoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_planet04F_1
            int img_planet04F_1MoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_planet04F_1MoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_star01_1
            int img_star01_1MoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_star01_1MoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_star01_2
            int img_star01_2MoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_star01_2MoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_star02_1
            int img_star02_1MoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_star02_1MoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            //img_star02_2
            int img_star02_2MoveY = Convert.ToInt32(Math.Floor(getScreenHightInPixels * 0));
            int img_star02_2MoveX = Convert.ToInt32(Math.Floor(getScreenWidthInPixels * 0));

            ///////////////////////////////////////////////////////////////////////////////////////

            //startGame
            startGame.Location = new Point(
                centerElementXcor(startGame.Location.X, startGame.Size.Width)+ startGameMoveX,
                centerElementYcor(startGame.Location.Y, startGame.Size.Height)+startGameMoveY);
            startGame.Refresh();

            //img_action
            img_action.Location = new Point(
                centerElementXcor(img_action.Location.X, img_action.Size.Width) + img_actionMoveX,
                centerElementYcor(img_action.Location.Y, img_action.Size.Height) + img_actionMoveY);
            img_action.Refresh();

            //img_arrow
            img_arrow.Location = new Point(
                centerElementXcor(img_arrow.Location.X, img_arrow.Size.Width)+img_arrowMoveX,
                centerElementYcor(img_arrow.Location.Y, img_arrow.Size.Height)+img_arrowMoveY);
                    img_arrow.Refresh();

            //img_UFO
            img_UFO.Location = new Point(
                centerElementXcor(img_UFO.Location.X, img_UFO.Size.Width)+img_UFOMoveX,
                centerElementYcor(img_UFO.Location.Y, img_UFO.Size.Height)+img_UFOMoveY);
            img_UFO.Refresh();

            //instruction
            instruction.Location = new Point(
                centerElementXcor(instruction.Location.X, instruction.Size.Width)+instructionMoveX,
                centerElementYcor(instruction.Location.Y, instruction.Size.Height)+instructionMoveY);
            instruction.Refresh();

            //action
            action.Location = new Point(
                centerElementXcor(action.Location.X, action.Size.Width)+actionMoveY,
                centerElementYcor(action.Location.Y, action.Size.Height)+actionMoveX);
            action.Refresh();

            //img_planet01_1
            img_planet01_1.Location = new Point(
               centerElementXcor(startGame.Location.X, startGame.Size.Width) + startGameMoveX,
               centerElementYcor(startGame.Location.Y, startGame.Size.Height) + startGameMoveY);
            startGame.Refresh();

            //img_planet01_2

            //img_planet02_1

            //img_planet02_2

            //img_planet02_3

            //img_planet03_1

            //img_planet03_2

            //img_planet04_1

            //img_planet04F_1

            //img_star01_1

            //img_star01_2

            //img_star02_1

            //img_star02_2

            int bottomCW = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - lanuage.Size.Width);
            int bottomCH = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - lanuage.Size.Height);

            lanuage.Location = new Point(bottomCW,bottomCH);
            lanuage.Refresh();
        }

        //----------Ends UI update----


        //Timer to fade arrow in and out on start screen
        private void timer1_Tick(object sender, EventArgs e)
        {
            img_arrow.Image = changeTransparacy(img_arrow.Image, alpha);

            if (inOut == true) {
                alpha -= 10;
            }
            if (inOut == false) {
                alpha += 10;
            }

            if (alpha < 10) {
                inOut = false;
            }
            if (alpha > 240) {
                inOut = true;
            }

        }


        private void startGame_Click(object sender, EventArgs e)
        {
            img_arrow.Visible = false;
            img_UFO.Visible = false;
            startGame.Visible = false;
            Thread.Sleep(300);
            runGame();
        }


        private void runGame()
        {
            
            String[] instructions = loadActions();
            int numInstructions = instructions.Length;

            ready321();
            instruction.Visible = true;
            instruction.Refresh();
            for (int dis = 0; dis < numInstructions; dis++) {

                instruction.Text = instructions[dis];
                instruction.Refresh();
                Thread.Sleep(1000);

            }

            openUI();
            this.Close();


        }

        //dummy method to load data from MBED
        private string[] loadActions() {
            Stopwatch timingP = new Stopwatch();
            action.Text = loadingText;
            action.Visible = true;
            //img_action.Image = Synergy_Solutions_App.Properties.Resources.ufo;
            img_action.Visible = true;
            img_action.Refresh();
            action.Refresh();

            timingP.Start();
            String[] instructionsFromMBED = getActionsFromSerial();
            timingP.Stop();

            Console.WriteLine(timingP.ElapsedMilliseconds);

            int timeoutTime = 1000;
            timeoutTime -= unchecked((int)timingP.ElapsedMilliseconds);
            Thread.Sleep(timeoutTime);
            Console.WriteLine(timeoutTime);

            action.Visible = false;
            img_action.Visible = false;
            action.Refresh();
            img_action.Refresh();

            return instructionsFromMBED;
        }

        private string[] getActionsFromSerial() {
            int numOfCommands = 4;
            string[] MBEDcommands = new string[numOfCommands];

            /*
            for (int c = 0; c < numOfCommands;) { 
                
                MBEDcommands[c] = 
            
            }
            */
            MBEDcommands[0] = "button1";
            MBEDcommands[1] = "sliderUp";
            MBEDcommands[2] = "sliderDown";
            MBEDcommands[3] = "button1";

            return MBEDcommands;
        }


        private void ready321(){

            action.Text = readyFReady;
            action.Visible = true;
            action.Refresh();


            for (int t = 3; t > -1; t--) {

                if (t != 0)
                {
                    instruction.Text = t.ToString();
                }
                else
                {
                    instruction.Text = readyFGo;
                }

                instruction.Visible = true;
                instruction.Refresh();
                Thread.Sleep(1000);
            }

            instruction.Visible = false;
            action.Visible = false;
            action.Refresh();
            instruction.Refresh();


        }

        //----Debug button----

        //debug button to move straight to user mode skipping game
        private void button1_Click(object sender, EventArgs e)
        {
            //this.Close();
            openUI();
        }

        private void openUI() {
            gameSerial.Close();
            thUI = new Thread(opennewform);
            thUI.SetApartmentState(ApartmentState.STA);
            thUI.Start();

        }

        private void opennewform()
        {
            Application.Run(new UserMode());
        }

        //----End of Debug button code----

        private void lanuage_Click(object sender, EventArgs e)
        {
            Console.WriteLine("mee");
            lanSelect++;
            if (lanSelect == 0) {
                startGame.Text = "Start Game";
                readyFGo = "Go";
                readyFReady = "Ready";
                loadingText = "Loading";
            
            }
            if (lanSelect == 1) {
                startGame.Text = "Start Game in Spanish";
                readyFReady = "Ready in Spanish";
                readyFGo = "Go in Spanish";
                loadingText = "Loading in Spanish";

                lanSelect = 0;
            
            }
            
        }







        //Basically end of start screen code
        private void img_arrow_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void planet01_2_Click(object sender, EventArgs e)
        {

        }

        private void planet04_1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_2(object sender, EventArgs e)
        {

        }

        private void img_planet04F_1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
