using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RSCXNALib.Data;
using RSCXNALib.Game;

namespace RSCXNALib
{
    public class GameApplet// : java.applet.Applet
    {
        public GameApplet()
        {
        }

        public virtual void loadGame()
        {
        }

        public virtual void checkInputs()
        {
        }

        public virtual void close()
        {
        }

        public void createWindow(int width, int height, String title, bool resizable)
        {
            Console.WriteLine("Started application");
            appletWidth = width;
            appletHeight = height;
            gameFrame = new GameFrame(this, width, height, title, resizable, false);
            gameLoadingScreen = 1;

            InitGameApplet();

            // gameWindowThread = new Thread(this);
            //  gameWindowThread.start();
            //  gameWindowThread.setPriority(1);
        }


        public void setRefreshRate(int i)
        {
            refreshRate = 1000 / i;
        }

        public void resetTimings()
        {
            for (int i = 0; i < 10; i++)
                timeArray[i] = 0L;

        }

        public void keyTyped(EventArgs e)
        {
            //ignore
        }

        public void mouseClicked(EventArgs e)
        {
            //ignore
        }

        public void keyPressed(Keys evt)
        {
            var c = Encoding.UTF8.GetChars(new[] { (byte)evt });
            this.keyDown(evt, c[0]);
        }

        public void keyReleased(Keys evt)
        {
            var c = Encoding.UTF8.GetChars(new[] { (byte)evt });
            this.keyUp(evt, c[0]);
        }

        public void mouseEntered(MouseState evt)
        {
            this.mouseMove(evt.X, evt.Y);
        }

        public void mouseExited(MouseState evt)
        {
            this.mouseMove(evt.X, evt.Y);
        }

        public void mousePressed(MouseState evt)
        {
            this.mouseDown(evt.X, evt.Y, evt.RightButton == ButtonState.Pressed);
        }

        public void mouseReleased(MouseState evt)
        {
            this.mouseUp(evt.X, evt.Y);
        }

        public void mouseDragged(MouseState evt)
        {
            this.mouseDrag(evt.Y, evt.X, evt.RightButton == ButtonState.Pressed);
        }

        public void mouseMoved(MouseState evt)
        {
            this.mouseMove(evt.X, evt.Y);
        }

        public void keyDown(Keys key, char c)
        {
            handleKeyDown(key, c);
            if (key == Keys.Left)
                keyLeftDown = true;
            if (key == Keys.Right)
                keyRightDown = true;
            if (key == Keys.Up)
                keyUpDown = true;
            if (key == Keys.Down)
                keyDownDown = true;
            if (key == Keys.Space)
                keySpaceDown = true;
            if (key == Keys.N || key == Keys.M)
                keyNMDown = true;
            if (key == Keys.F1)
                keyF1Toggle = !keyF1Toggle;
            bool flag = false;
            for (int i = 0; i < allowedChars.Length; i++)
            {
                if (c != allowedChars[i] && key != Keys.Left && key != Keys.Right && key != Keys.Up && key != Keys.Down)
                    continue;
                flag = true;
                break;
            }
            if (flag && inputText.Length < 20)
                inputText += c;
            if (flag && pmText.Length < 80)
                pmText += c;
            if (key == Keys.Back && inputText.Length > 0)
                inputText = inputText.Substring(0, inputText.Length - 1);
            if (key == Keys.Back && pmText.Length > 0)
                pmText = pmText.Substring(0, pmText.Length - 1);
            if (key == Keys.Enter)
            {
                enteredInputText = inputText;
                enteredPMText = pmText;
            }
        }

        public virtual void handleKeyDown(Keys key, char c)
        {
        }

        public void keyUp(Keys key, char c)
        {
            if (key == Keys.Left)
                keyLeftDown = false;
            if (key == Keys.Right)
                keyRightDown = false;
            if (key == Keys.Up)
                keyUpDown = false;
            if (key == Keys.Down)
                keyDownDown = false;
            if (key == Keys.Space)
                keySpaceDown = false;
            if (key == Keys.N || key == Keys.M)
                keyNMDown = false;
        }

        public bool mouseMove(int x, int y)
        {
            mouseX = x;
            mouseY = y - mouseYOffset;
            mouseButton = 0;
            return true;
        }

        public bool mouseUp(int x, int y)
        {
            mouseX = x;
            mouseY = y - mouseYOffset;
            mouseButton = 0;
            return true;
        }

        public bool mouseDown(int x, int y, bool metaDown)
        {
            mouseX = x;
            mouseY = y - mouseYOffset;
            mouseButton = metaDown ? 2 : 1;
            lastMouseButton = mouseButton;
            handleMouseDown(mouseButton, x, y);
            return true;
        }

        public virtual void handleMouseDown(int i, int k, int l)
        {
        }

        public bool mouseDrag(int x, int y, bool metaDown)
        {
            mouseX = x;
            mouseY = y - mouseYOffset;
            mouseButton = metaDown ? 2 : 1;
            return true;
        }

        public void init()
        {
            Console.WriteLine("Started applet");
            appletWidth = 512;
            appletHeight = 344;
            gameLoadingScreen = 1;
            DataOperations.codeBase = getCodeBase();
            //startThread(this);
        }

        public void start()
        {
            if (runStatus >= 0)
                runStatus = 0;
        }

        public void stop()
        {
            if (runStatus >= 0)
                runStatus = 4000 / refreshRate;
        }


        public void destroy()
        {
            runStatus = -1;
            try
            {
                Thread.Sleep(2000);
            }
            catch (Exception _ex) { }
            if (runStatus == -1)
            {
                Console.WriteLine("2 seconds expired, forcing kill");
                closeProgram();
                if (gameWindowThread != null)
                {
                    gameWindowThread.Abort();
                    gameWindowThread = null;
                }
            }
        }



        public void closeProgram()
        {
            runStatus = -2;
            Console.WriteLine("Closing program");
            close();
            try
            {
                Thread.Sleep(1000);
            }
            catch (Exception _ex) { }
            if (gameFrame != null)
            {
                //gameFrame.dispose();
                //System.Exit(0);

            }
        }

        //Component getGameComponent() {
        //    if(gameFrame != null)
        //        return gameFrame;
        //    else
        //        return this;
        //}

        public void LoadApp()
        {

        }


        public void run()
        {
            //getGameComponent().addKeyListener(this);
            //getGameComponent().addMouseListener(this);
            //getGameComponent().addMouseMotionListener(this);


            if (gameLoadingScreen == 1)
            {
                gameLoadingScreen = 2;
                loadLoadingScreen();
                drawLoadingScreen(0, "Loading...");
                loadGame();
                gameLoadingScreen = 0;
            }

            for (int k1 = 0; k1 < 10; k1++)
                timeArray[k1] = CurrentTimeMillis();




            while (runStatus >= 0)
            {
                UpdateGame(gameVar_i, gameVar_k, gameVar_sleepTime, gameVar_j1);
                OnDrawDone();
            }
            if (runStatus == -1)
            {
                closeProgram();
                gameWindowThread = null;
            }




        }
        public bool DrawIsNecessary = false;
        public void OnDrawDone()
        {
            DrawIsNecessary = true;
        }

        public int gameVar_i = 0;
        public int gameVar_k = 256;
        public int gameVar_sleepTime = 1;
        public int gameVar_j1 = 0;

        public void UpdateGame(int i, int k, int sleepTime, int j1)
        {
            if (runStatus > 0)
            {
                runStatus--;
                if (runStatus == 0)
                {
                    closeProgram();
                    gameWindowThread = null;
                    return;
                }
            }
            int i2 = k;
            int j2 = sleepTime;
            k = 300;
            sleepTime = 1;
            long l1 = CurrentTimeMillis();//System.currentTimeMillis();
            if (timeArray[i] == 0L)
            {
                k = i2;
                sleepTime = j2;
            }
            else if (l1 > timeArray[i])
                k = (int)((long)(2560 * refreshRate) / (l1 - timeArray[i]));
            if (k < 25)
                k = 25;
            if (k > 256)
            {
                k = 256;
                sleepTime = (int)((long)refreshRate - (l1 - timeArray[i]) / 10L);
                if (sleepTime < gameMinThreadSleepTime)
                    sleepTime = gameMinThreadSleepTime;
            }
            try
            {
                Thread.Sleep(sleepTime);
            }
            catch (Exception _ex) { }
            timeArray[i] = l1;
            i = (i + 1) % 10;
            if (sleepTime > 1)
            {
                for (int k2 = 0; k2 < 10; k2++)
                    if (timeArray[k2] != 0L)
                        timeArray[k2] += sleepTime;

            }
            int l2 = 0;
            while (j1 < 256)
            {
                var start = DateTime.Now;
                checkInputs();
                j1 += k;
                if (++l2 > fie)
                {
                    j1 = 0;
                    fij += 6;
                    if (fij > 25)
                    {
                        fij = 0;
                        keyF1Toggle = true;
                    }
                    break;
                }
                var end = DateTime.Now - start;
            }
            fij--;
            j1 &= 0xff;
            //drawWindow();
            // paint(graphics);
        }

        public virtual void drawWindow()
        {

        }

        public void paint(GraphicsDevice g1)
        {
            if (gameLoadingScreen == 2)
            {
                drawLoadingScreen(gameLoadingPercentage, gameLoadingFileTitle);
                return;
            }
        }

        private void loadLoadingScreen()
        {
            //base.loadLoadingScreen();
            //graphics.Clear(Color.Black);

            //graphics.fillRect(0, 0, appletWidth, appletHeight);
            sbyte[] bytes = unpackData("fonts.jag", "Game fonts", 0);
            GameImage.addFont(DataOperations.loadData("h11p.jf", 0, bytes));
            GameImage.addFont(DataOperations.loadData("h12b.jf", 0, bytes));
            GameImage.addFont(DataOperations.loadData("h12p.jf", 0, bytes));
            GameImage.addFont(DataOperations.loadData("h13b.jf", 0, bytes));
            GameImage.addFont(DataOperations.loadData("h14b.jf", 0, bytes));
            GameImage.addFont(DataOperations.loadData("h16b.jf", 0, bytes));
            GameImage.addFont(DataOperations.loadData("h20b.jf", 0, bytes));
            GameImage.addFont(DataOperations.loadData("h24b.jf", 0, bytes));
        }

        private void drawLoadingScreen(int percentage, String fileTitle)
        {
            try
            {
                int i = (appletWidth - 281) / 2;
                int k = (appletHeight - 148) / 2;
                //graphics.setColor(Color.Black);
                //graphics.fillRect(0, 0, appletWidth, appletHeight);
                //graphics.Clear(Color.Black);

                i += 2;
                k += 90;



                //if (bgImage != null)
                //{
                //    // spriteBatch.BeginSafe();
                //    spriteBatch.Draw(bgImage, Vector2.Zero, Color.White);
                //    // spriteBatch.EndSafe();
                //}
                //  graphics.drawImage(bgImage, 0, 0, null);
                gameLoadingPercentage = percentage;
                gameLoadingFileTitle = fileTitle;
                //graphics.setColor();

                //spriteBatch.drawRect(new Rectangle(i - 2, k - 2, 280, 23), new Color(132, 132, 132));
                //spriteBatch.fillRect(new Rectangle(i, k, (277 * percentage) / 100, 20), new Color(132, 132, 132));


                //graphics.setColor(new Color(198, 198, 198));
                //drawString(fileTitle/*, gameLoadingFont*/, i + 138, k + 10, new Color(198, 198, 198));

            }
            catch (Exception _ex) { }
        }

        public void drawLoadingBarText(int i, String s)
        {
            try
            {

                int k = (appletWidth - 281) / 2;
                int l = (appletHeight - 148) / 2;
                k += 2;
                l += 90;
                gameLoadingPercentage = i;
                gameLoadingFileTitle = s;
                int i1 = (277 * i) / 100;
                // spriteBatch.fillRect(new Rectangle(k, l, i1, 20), new Color(132, 132, 132));
                //  graphics.setColor(new Color(132, 132, 132));
                //  graphics.fillRect(k, l, i1, 20);
                //  graphics.setColor(Color.black);
                //  spriteBatch.fillRect(new Rectangle(k + i1, l, 277 - i1, 20), Color.Black);
                //graphics.fillRect(k + i1, l, 277 - i1, 20);
                //graphics.setColor(new Color(198, 198, 198));

                //drawString(graphics, s, gameLoadingFont, k + 138, l + 10, new Color(198, 198, 198));
                return;
            }
            catch (Exception _ex)
            {
                return;
            }
        }

        //public void drawString(String arg1, int arg3, int arg4, Color color)
        //{
        //    //Object obj;
        //    //if (gameFrame == null)
        //    //    obj = this;
        //    //else
        //    //    obj = gameFrame;
        //    //var fontmetrics = arg2.MeasureString(arg1);//((Component)(obj)).getFontMetrics(arg2);
        //    //fontmetrics.stringWidth(arg1);
        //    //arg0.setFont(arg2);
        //    //arg0.drawString(arg1, arg3 - fontmetrics.stringWidth(arg1) / 2, arg4 + fontmetrics.getHeight() / 4);

        //    //GameImage.stringsToDraw.Add(new stringDrawDef
        //    //{
        //    //    font = arg2,
        //    //    text = arg1,
        //    //    pos = new Vector2(arg3 - fontmetrics.X / 2, arg4 + fontmetrics.Y / 4),
        //    //    forecolor = color
        //    //});

        //    //spriteBatch.BeginSafe();
        //    //spriteBatch.DrawString(arg2, arg1, new Vector2(fontmetrics.X / 2, arg4 + fontmetrics.Y / 4), color);
        //    //spriteBatch.EndSafe();
        //}

        public virtual sbyte[] unpackData(String filename, String fileTitle, int startPercentage)
        {

            Console.WriteLine("Using default load");
            int i = 0;
            int k = 0;
            sbyte[] abyte0 = link.getFile(filename);
            if (abyte0 == null)
            {
                try
                {
                    Console.WriteLine("Loading " + fileTitle + " - 0%");
                    drawLoadingBarText(startPercentage, "Loading " + fileTitle + " - 0%");
                    var inputstream = new BinaryReader(DataOperations.openInputStream(filename));
                    //DataInputStream datainputstream = new DataInputStream(inputstream);
                    sbyte[] abyte2 = new sbyte[6] {
                        inputstream.ReadSByte(),inputstream.ReadSByte(),inputstream.ReadSByte(),
                        inputstream.ReadSByte(),inputstream.ReadSByte(),inputstream.ReadSByte()
                    };

                    //inputstream.Read(abyte2, 0, 6);
                    i = ((abyte2[0] & 0xff) << 16) + ((abyte2[1] & 0xff) << 8) + (abyte2[2] & 0xff);
                    k = ((abyte2[3] & 0xff) << 16) + ((abyte2[4] & 0xff) << 8) + (abyte2[5] & 0xff);

					

                    Console.WriteLine("Loading " + fileTitle + " - 5%");
                    drawLoadingBarText(startPercentage, "Loading " + fileTitle + " - 5%");
#warning this could break stuff
					// int l = 0;
					int l = 6;
                    abyte0 = new sbyte[k];
                    while (l < k)
                    {
                        int i1 = k - l;
                        if (i1 > 1000)
                            i1 = 1000;

                        for (int t = 0; t < i1; t++)
                            abyte0[l + t] = inputstream.ReadSByte();

                        // inputstream.Read(abyte0, l, i1);

                        l += i1;
                        Console.WriteLine("Loading " + fileTitle + " - " + (5 + (l * 95) / k) + "%");
                        drawLoadingBarText(startPercentage, "Loading " + fileTitle + " - " + (5 + (l * 95) / k) + "%");
                    }

                    inputstream.Close();
                }
                catch (IOException _ex) { }
            }
            Console.WriteLine("Unpacking " + fileTitle);
            drawLoadingBarText(startPercentage, "Unpacking " + fileTitle);
            if (k != i)
            {
                sbyte[] abyte1 = new sbyte[i];
                DataFileDecrypter.unpackData(abyte1, i, abyte0, k, 0);
                return abyte1;
            }
            else
            {
                //  return unpackData(filename, fileTitle, startPercentage); // abyte0;
                return abyte0;
            }
        }

        //public Texture2D createImage(int i, int k)
        //{
        //    //if (gameFrame != null)
        //    //    return gameFrame.createImage(i, k);
        //    //else
        //    //    return super.createImage(i, k);

        //    return new Texture2D(this.graphics, i, k);
        //}

        public Uri getCodeBase()
        {
            return default(Uri);//super.getCodeBase();
        }

        public Uri getDocumentBase()
        {
            return default(Uri);//super.getDocumentBase();
        }

        public String getParameter(String s)
        {
            return ""; //super.getParameter(s);
        }

        public TcpClient makeSocket(String address, int port)
        {
            var socket = new TcpClient(address, port);
            socket.SendTimeout = 30000;
            socket.NoDelay = true;
            return socket;
        }

        public void mouseScroll(bool begin, int arg)
        {
            Console.WriteLine("mouseWheel(" + begin + ", " + arg + ")");
        }

        public void InitGameApplet()
        {
            appletWidth = 512;
            appletHeight = 384;
            refreshRate = 60;
            fie = 1000;
            timeArray = new long[10];
            gameLoadingScreen = 1;
            gameLoadingFileTitle = "Loading";
            //gameLoadingFont = loadingFont;//new Font("TimesRoman", 0, 15);
            keyLeftDown = false;
            keyRightDown = false;
            keyUpDown = false;
            keyDownDown = false;
            keySpaceDown = false;
            keyNMDown = false;
            gameMinThreadSleepTime = 1;
            keyF1Toggle = false;
            inputText = "";
            enteredInputText = "";
            pmText = "";
            enteredPMText = "";
        }

        public GameApplet(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {

            // baseApplet = new org.moparscape.msc.client.GameApplet();

            InitGameApplet();
        }

        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        //GameApplet baseApplet;
        private int appletWidth;
        private int appletHeight;
        public Thread gameWindowThread;
        private int refreshRate;
        private int fie;
        private long[] timeArray;
        public static GameFrame gameFrame = null;
        public int runStatus;
        public int fij;
        public int mouseYOffset = 0;
        public int gameLoadingScreen;
        public int gameLoadingPercentage;
        public String gameLoadingFileTitle;
        public static String allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖabcdefghijklmnopqrstuvwxyzåäö0123456789!\"!$%^&*()-_=+[{]};:'@#~,<.>/?\\| ";
        public bool keyLeftDown;
        public bool keyRightDown;
        public bool keyUpDown;
        public bool keyDownDown;
        public bool keySpaceDown;
        public bool keyNMDown;
        public int gameMinThreadSleepTime;
        public int mouseX;
        public int mouseY;
        public int mouseButton;
        public int lastMouseButton;
        public bool keyF1Toggle;
        public String inputText;
        public String enteredInputText;
        public String pmText;
        public String enteredPMText;

        public static int[][] bgPixels = null;
        public static Texture2D bgImage = null;

    }
}
